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
        /// <summary>
        /// Allowed configurations. If equals null any configuration can be used.
        /// </summary>
        public string[] Configurations { get; set; }

        public double MinimumScore { get; set; }

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
                if (Configurations == null)
                {
                    IEnumerable<RecaptchaConfiguration> configurations;
                    if ((configurations = await configurationStore.GetRecaptchaConfigurations()).Count() == 1)
                    {
                        configuration = configurations.First();
                    }
                    else
                    {
                        context.ModelState.AddModelError(RecaptchaDefaults.UnallowedConfigurationError, string.Empty);
                        goto end;
                    }
                }
                else if (Configurations.Length == 1)
                {
                    configuration = await configurationStore.GetRecaptchaConfiguration(Configurations[0]);
                }
                else
                {
                    var key = configurationProvider.GetRecaptchaConfigurationKey(context.HttpContext.Request);
                    if (Array.BinarySearch(Configurations, key) < 0)
                    {
                        context.ModelState.AddModelError(RecaptchaDefaults.UnallowedConfigurationError, string.Empty);
                        goto end;
                    }
                    else
                    {
                        configuration = await configurationStore.GetRecaptchaConfiguration(key);
                    }
                }

                var response = await recaptchaHttpClient.VerifyRecaptchaAsync(configuration, context.HttpContext.Request, tokenProvider);

                if (response.IsSuccess)
                {
                    if (response is RecaptchaResponseV3 responseV3)
                    {
                        if (responseV3.Score < MinimumScore)
                        {
                            context.ModelState.AddModelError(RecaptchaDefaults.LowScoreError, string.Empty);
                        }
                        if (AllowedAction != null && responseV3.Action != AllowedAction)
                        {
                            context.ModelState.AddModelError(RecaptchaDefaults.UnallowedActionError, string.Empty);
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
