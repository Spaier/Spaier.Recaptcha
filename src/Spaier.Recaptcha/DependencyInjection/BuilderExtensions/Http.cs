using System;
using System.Net.Http;
using Spaier.Recaptcha.DependencyInjection;
using Spaier.Recaptcha.Http;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class HttpRecaptchaBuilderExtensions
    {
        public static IRecaptchaBuilder UseGoogleUrl(this IRecaptchaBuilder builder)
        {
            builder.Services.AddSingleton<IVerifyUrlProvider, GoogleVerifyUrlProvider>();
            return builder;
        }

        public static IRecaptchaBuilder UseGlobalUrl(this IRecaptchaBuilder builder)
        {
            builder.Services.AddSingleton<IVerifyUrlProvider, GlobalVerifyUrlProvider>();
            return builder;
        }

        public static IRecaptchaBuilder AddRecaptchaHttpClient(this IRecaptchaBuilder builder,
            string name = "", Action<HttpClient> configureClient = null)
        {
            return builder.AddRecaptchaHttpClient<RecaptchaHttpClient>(name, configureClient);
        }

        public static IRecaptchaBuilder AddRecaptchaHttpClient<T>(this IRecaptchaBuilder builder,
            string name = "", Action<HttpClient> configureClient = null)
            where T : class, IRecaptchaHttpClient
        {
            if (configureClient == null)
            {
                builder.Services.AddHttpClient<IRecaptchaHttpClient, T>(name);
            }
            else
            {
                builder.Services.AddHttpClient<IRecaptchaHttpClient, T>(name, configureClient);
            }
            return builder;
        }
    }
}
