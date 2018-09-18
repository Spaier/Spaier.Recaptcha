using System;
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
        /// Allowed configurations. If equals null any configuration can be used.
        /// </summary>
        public string[] Configurations { get; set; }

        public double MinimumScore { get; set; } = 0.5;

        public string AllowedAction { get; set; }

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
                RecaptchaConfiguration configuration;

                if (Configurations is null)
                {
                    var configurations = await configurationStore.GetRecaptchaConfigurations();
                    if (configurations.Count() == 1)
                    {
                        configuration = configurations.Values.First();
                    }
                    else
                    {
                        context.ModelState.AddModelError(ErrorCodes.UnspecifiedConfigurationError, string.Empty);
                        goto end;
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
                            context.ModelState.AddModelError(ErrorCodes.MissingConfigurationError, string.Empty);
                            goto end;
                        }
                    }
                    else
                    {
                        var errorCode = key == null ? ErrorCodes.UnspecifiedConfigurationError : ErrorCodes.UnallowedConfigurationError;
                        context.ModelState.AddModelError(errorCode, string.Empty);
                        goto end;
                    }
                }

                var response = await recaptchaHttpClient
                    .VerifyRecaptchaAsync<RecaptchaResponse>(configuration, context.HttpContext.Request, tokenProvider);

                if (response.IsSuccess)
                {
                    if (configuration.SecretType == RecaptchaSecretType.V3)
                    {
                        if (response.Score.HasValue && response.Score < MinimumScore)
                        {
                            context.ModelState.AddModelError(ErrorCodes.LowScoreError, string.Empty);
                        }
                        if (AllowedAction != null && response.Action != AllowedAction)
                        {
                            context.ModelState.AddModelError(ErrorCodes.UnallowedActionError, string.Empty);
                        }
                    }
                    foreach (var parameter in context.ActionDescriptor.Parameters)
                    {
                        if (parameter?.BindingInfo?.BindingSource == FromRecaptchaResponseAttribute.Source)
                        {
                            context.ActionArguments[parameter.Name] = response;
                        }
                    }
                }
                else
                {
                    foreach (var error in response.ErrorCodes)
                    {
                        context.ModelState.AddModelError(error, string.Empty);
                    }
                }

            end:
                await base.OnActionExecutionAsync(context, next);
            }
        }
    }
}
