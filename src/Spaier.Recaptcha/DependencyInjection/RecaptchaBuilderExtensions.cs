using System;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using Spaier.Recaptcha;
using Spaier.Recaptcha.DependencyInjection;
using Spaier.Recaptcha.Http;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class RecaptchaBuilderExtensions
    {
        public static IRecaptchaBuilder AddTokenHeaderProvider(this IRecaptchaBuilder builder,
            Action<RecaptchaTokenHeaderProvider.Options> setupOptions)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (setupOptions == null)
            {
                throw new ArgumentNullException(nameof(setupOptions));
            }

            builder.Services.Configure(setupOptions);
            return builder.AddTokenHeaderProviderInner();
        }

        public static IRecaptchaBuilder AddTokenHeaderProvider(this IRecaptchaBuilder builder, IConfiguration configuration)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            builder.Services.Configure<RecaptchaTokenHeaderProvider.Options>(configuration);
            return builder.AddTokenHeaderProviderInner();
        }

        public static IRecaptchaBuilder AddTokenHeaderProvider(this IRecaptchaBuilder builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            builder.Services.Configure<RecaptchaTokenHeaderProvider.Options>(options => { });
            return builder.AddTokenHeaderProviderInner();
        }

        internal static IRecaptchaBuilder AddTokenHeaderProviderInner(this IRecaptchaBuilder builder)
        {
            builder.Services.AddSingleton<IRecaptchaTokenProvider, RecaptchaTokenHeaderProvider>();
            return builder;
        }

        public static IRecaptchaBuilder AddConfigurationHeaderProvider(this IRecaptchaBuilder builder,
            Action<RecaptchaConfigurationHeaderProvider.Options> setupOptions)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (setupOptions == null)
            {
                throw new ArgumentNullException(nameof(setupOptions));
            }

            builder.Services.Configure(setupOptions);
            return builder.AddConfigurationHeaderProviderInner();
        }

        public static IRecaptchaBuilder AddConfigurationHeaderProvider(this IRecaptchaBuilder builder, IConfiguration configuration)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            builder.Services.Configure<RecaptchaConfigurationHeaderProvider.Options>(configuration);
            return builder.AddConfigurationHeaderProviderInner();
        }

        internal static IRecaptchaBuilder AddConfigurationHeaderProviderInner(this IRecaptchaBuilder builder)
        {
            builder.Services.AddSingleton<IRecaptchaConfigurationProvider, RecaptchaConfigurationHeaderProvider>();
            return builder;
        }

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
