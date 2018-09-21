using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Spaier.Recaptcha.Http;
using Spaier.Recaptcha.Mvc;
using Spaier.Recaptcha.Responses;
using Spaier.Recaptcha.Services;

namespace Spaier.Recaptcha
{
    public class ValidateRecaptchaAttribute : FilterFactoryAttribute, IOrderedFilter
    {
        public static class ErrorCodes
        {
            /// <summary>
            /// The reCAPTCHA wasn't successful and no other error occured.
            /// </summary>
            public const string NoSuccessError = "recaptcha-no-success";
            /// <summary>
            /// The reCAPTCHA score is too low. 
            /// </summary>
            public const string LowScoreError = "recaptcha-low-score";
            /// <summary>
            /// The specified reCAPTCHA action isn't allowed.
            /// </summary>
            public const string UnallowedActionError = "recaptcha-unallowed-action";
            /// <summary>
            /// The specified configuration isn't allowed.
            /// </summary>
            public const string UnallowedConfigurationError = "recaptcha-unallowed-configuration";
            /// <summary>
            /// Configuration wasn't specified.
            /// </summary>
            public const string UnspecifiedConfigurationError = "recaptcha-unspecified-configuration";
            /// <summary>
            /// The specified configuration doesn't exist in the store.
            /// </summary>
            public const string MissingConfigurationError = "recaptcha-missing-configuration";
        }

        /// <summary>
        /// Allowed configurations.
        /// If only one is specified you can omit configuration.
        /// If none is specified you can use any configuration.
        /// <see cref="ErrorCodes.MissingConfigurationError"/>
        /// <see cref="ErrorCodes.UnspecifiedConfigurationError"/>
        /// <see cref="ErrorCodes.UnallowedConfigurationError"/>
        /// </summary>
        public string[] Configurations { get; set; }

        /// <summary>
        /// <see cref="ErrorCodes.LowScoreError"/>.
        /// </summary>
        public double MinimumScore { get; set; } = 0.5;

        /// <summary>
        /// <see cref="ErrorCodes.UnallowedActionError"/>.
        /// </summary>
        public string AllowedAction { get; set; }

        /// <summary>
        /// If true adds response's ErrorCodes to MVC ModelState.
        /// </summary>
        public bool UseModelErrors { get; set; } = true;

        /// <summary>
        /// ModelStateInvalidFilter has order -2000.
        /// We have to set order lower than that in order for model errors to work.
        /// </summary>
        public int Order => -3000;

        public override IFilterMetadata CreateInstance(IServiceProvider serviceProvider)
        {
            var service = serviceProvider.GetRequiredService<InnerAttribute>();
            service.Configurations = Configurations;
            service.AllowedAction = AllowedAction;
            service.UseModelErrors = UseModelErrors;
            if (MinimumScore < 0 || MinimumScore > 1)
            {
                throw new ArgumentOutOfRangeException(nameof(MinimumScore));
            }
            service.MinimumScore = MinimumScore;
            return service;
        }

        internal class InnerAttribute : ActionFilterAttribute
        {
            private readonly IRecaptchaTokenProvider tokenProvider;
            private readonly IRecaptchaConfigurationProvider configurationProvider;
            private readonly IRecaptchaHttpClient recaptchaHttpClient;
            private readonly IRecaptchaConfigurationStore configurationStore;

            public string[] Configurations { get; set; }

            public double MinimumScore { get; set; }

            public string AllowedAction { get; set; }

            public bool UseModelErrors { get; set; }

            public InnerAttribute(IRecaptchaTokenProvider tokenProvider, IRecaptchaConfigurationProvider configurationProvider,
                IRecaptchaHttpClient recaptchaHttpClient, IRecaptchaConfigurationStore configurationStore)
            {
                this.tokenProvider = tokenProvider ?? throw new ArgumentNullException(nameof(tokenProvider));
                this.configurationProvider = configurationProvider ?? throw new ArgumentNullException(nameof(configurationProvider));
                this.recaptchaHttpClient = recaptchaHttpClient ?? throw new ArgumentNullException(nameof(recaptchaHttpClient));
                this.configurationStore = configurationStore ?? throw new ArgumentNullException(nameof(configurationStore));
            }

            public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
            {
                bool noResponse = false;
                var customErrors = new List<string>();
                RecaptchaConfiguration configuration = null;
                RecaptchaResponse response;

                if (Configurations is null)
                {
                    var configurations = await configurationStore.GetRecaptchaConfigurations();
                    if (configurations.Count() == 1)
                    {
                        configuration = configurations.Values.First();
                    }
                    else
                    {
                        customErrors.Add(ErrorCodes.UnspecifiedConfigurationError);
                        noResponse = true;
                    }
                }
                else
                {
                    var key = configurationProvider.GetRecaptchaConfigurationKey(context.HttpContext.Request);
                    if ((key == null && Configurations.Length == 1) || Configurations.Contains(key))
                    {
                        bool isFound;
                        (isFound, configuration) = await configurationStore.TryGetRecaptchaConfiguration(Configurations[0]);
                        if (!isFound)
                        {
                            customErrors.Add(ErrorCodes.MissingConfigurationError);
                            noResponse = true;
                        }
                    }
                    else
                    {
                        var errorCode = key == null ? ErrorCodes.UnspecifiedConfigurationError : ErrorCodes.UnallowedConfigurationError;
                        customErrors.Add(errorCode);
                        noResponse = true;
                    }
                }

                if (noResponse)
                {
                    response = new RecaptchaResponse()
                    {
                        IsSuccess = false,
                        ErrorCodes = customErrors.ToArray()
                    };
                }
                else
                {
                    response = await recaptchaHttpClient
                        .VerifyRecaptchaAsync<RecaptchaResponse>(configuration, context.HttpContext.Request, tokenProvider);

                    if (response.IsSuccess)
                    {
                        if (response.Score.HasValue && response.Score < MinimumScore)
                        {
                            customErrors.Add(ErrorCodes.LowScoreError);
                        }

                        if (!(AllowedAction is null) && AllowedAction != response.Action)
                        {
                            customErrors.Add(ErrorCodes.UnallowedActionError);
                        }
                    }
                    else if (customErrors.Count == 0 && (response.ErrorCodes is null || response.ErrorCodes.Length == 0))
                    {
                        customErrors.Add(ErrorCodes.NoSuccessError);
                    }

                    if (customErrors.Count > 0)
                    {
                        response.ErrorCodes = (response.ErrorCodes is null
                            ? customErrors : customErrors.Union(response.ErrorCodes)).ToArray();
                    }
                }

                if (!(context?.ActionDescriptor?.Parameters is null))
                {
                    foreach (var parameter in context.ActionDescriptor.Parameters)
                    {
                        if (parameter?.BindingInfo?.BindingSource == FromRecaptchaResponseAttribute.Source)
                        {
                            context.ActionArguments[parameter.Name] = response;
                        }
                    }
                }

                if (UseModelErrors && !(response?.ErrorCodes is null))
                {
                    foreach (var error in response.ErrorCodes)
                    {
                        context.ModelState.AddModelError(error, string.Empty);
                    }
                }

                await base.OnActionExecutionAsync(context, next);
            }
        }
    }
}
