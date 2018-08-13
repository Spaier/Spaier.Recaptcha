using System;
using Microsoft.Extensions.Configuration;
using Spaier.Recaptcha.DependencyInjection;
using Spaier.Recaptcha.Services;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class RecaptchaBuilderExtensionsCore
    {
        public static IRecaptchaBuilder AddTokenHeaderProvider(this IRecaptchaBuilder builder,
            Action<RecaptchaTokenHeaderProvider.Options> setupOptions)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (setupOptions is null)
            {
                throw new ArgumentNullException(nameof(setupOptions));
            }

            builder.Services.Configure(setupOptions);
            return builder.AddTokenHeaderProviderInner();
        }

        public static IRecaptchaBuilder AddTokenHeaderProvider(this IRecaptchaBuilder builder, IConfiguration configuration)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (configuration is null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            builder.Services.Configure<RecaptchaTokenHeaderProvider.Options>(configuration);
            return builder.AddTokenHeaderProviderInner();
        }

        public static IRecaptchaBuilder AddTokenHeaderProvider(this IRecaptchaBuilder builder)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return builder.AddTokenHeaderProvider(_ => { });
        }

        internal static IRecaptchaBuilder AddTokenHeaderProviderInner(this IRecaptchaBuilder builder)
        {
            builder.Services.AddSingleton<IRecaptchaTokenProvider, RecaptchaTokenHeaderProvider>();
            return builder;
        }

        public static IRecaptchaBuilder AddConfigurationHeaderProvider(this IRecaptchaBuilder builder,
            Action<RecaptchaConfigurationHeaderProvider.Options> setupOptions)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (setupOptions is null)
            {
                throw new ArgumentNullException(nameof(setupOptions));
            }

            builder.Services.Configure(setupOptions);
            return builder.AddConfigurationHeaderProviderInner();
        }

        public static IRecaptchaBuilder AddConfigurationHeaderProvider(this IRecaptchaBuilder builder, IConfiguration configuration)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (configuration is null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            builder.Services.Configure<RecaptchaConfigurationHeaderProvider.Options>(configuration);
            return builder.AddConfigurationHeaderProviderInner();
        }

        public static IRecaptchaBuilder AddConfigurationHeaderProvider(this IRecaptchaBuilder builder)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return builder.AddConfigurationHeaderProvider(options => { });
        }

        internal static IRecaptchaBuilder AddConfigurationHeaderProviderInner(this IRecaptchaBuilder builder)
        {
            builder.Services.AddSingleton<IRecaptchaConfigurationProvider, RecaptchaConfigurationHeaderProvider>();
            return builder;
        }
    }
}
