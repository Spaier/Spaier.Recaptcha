using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Spaier.Recaptcha.Http;
using Spaier.Recaptcha.Mvc;

namespace Spaier.Recaptcha
{
    public class ValidateRecaptchaAttribute : FilterFactoryAttribute, IOrderedFilter
    {
        public string[] Configurations { get; set; }

        public int Order { get; set; }

        public override IFilterMetadata CreateInstance(IServiceProvider serviceProvider)
        {
            var service = serviceProvider.GetRequiredService<InnerAttribute>();
            service.Configurations = Configurations;
            service.Order = Order;
            return service;
        }

        internal class InnerAttribute : ActionFilterAttribute
        {
            private readonly IRecaptchaTokenProvider tokenProvider;
            private readonly IRecaptchaConfigurationProvider configurationProvider;
            private readonly IRecaptchaHttpClient recaptchaHttpClient;
            private readonly IRecaptchaSuccessHandler recaptchaSuccessHandler;

            /// <summary>
            /// Allowed configurations. If equals null any configuration can be used.
            /// </summary>
            public string[] Configurations { get; set; }

            public InnerAttribute(IRecaptchaTokenProvider tokenProvider, IRecaptchaConfigurationProvider configurationProvider,
                IRecaptchaHttpClient recaptchaHttpClient, IRecaptchaSuccessHandler recaptchaSuccessHandler = null)
            {
                this.tokenProvider = tokenProvider ?? throw new ArgumentNullException(nameof(tokenProvider));
                this.configurationProvider = configurationProvider ?? throw new ArgumentNullException(nameof(configurationProvider));
                this.recaptchaHttpClient = recaptchaHttpClient ?? throw new ArgumentNullException(nameof(recaptchaHttpClient));
                this.recaptchaSuccessHandler = recaptchaSuccessHandler;
            }

            public async override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
            {
                var configuration = configurationProvider.GetRecaptchaConfiguration(context.HttpContext.Request, out var key);
                if (Configurations != null && Array.BinarySearch(Configurations, key) < 0)
                {
                    throw new RecaptchaConfigurationException("Specified configuration isn't allowed");
                }

                var result = await recaptchaHttpClient.VerifyRecaptchaAsync(configuration, context.HttpContext.Request, tokenProvider);

                if (result.IsSuccess)
                {
                    recaptchaSuccessHandler?.OnSuccess(result, context);
                }
                else
                {
                    foreach (var error in result.ErrorCodes)
                    {
                        context.ModelState.AddModelError(error, string.Empty);
                    }
                }

                await next();
            }
        }
    }
}
