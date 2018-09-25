using System;
using Microsoft.Extensions.Configuration;
using Spaier.Recaptcha.DependencyInjection;
using Spaier.Recaptcha.Services;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class RecaptchaBuilderExtensionsCore
    {
        /// <summary>
        /// Specifies that reCAPTCHA token should be passed in http header.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="setupOptions"></param>
        /// <returns></returns>
        public static IRecaptchaBuilder AddTokenHeaderProvider(this IRecaptchaBuilder builder,
            Action<RecaptchaTokenHeaderProvider.Options> setupOptions = null)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (setupOptions is null)
            {
                setupOptions = _ => { };
            }

            builder.Services.Configure(setupOptions);
            return builder.AddTokenHeaderProviderInner();
        }

        /// <summary>
        /// Specifies that reCAPTCHA token should be passed in http header.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
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

        internal static IRecaptchaBuilder AddTokenHeaderProviderInner(this IRecaptchaBuilder builder)
        {
            builder.Services.AddSingleton<IRecaptchaTokenProvider, RecaptchaTokenHeaderProvider>();
            return builder;
        }

        /// <summary>
        /// Specifies that configuration should be passed in http header.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="setupOptions"></param>
        /// <returns></returns>
        public static IRecaptchaBuilder AddConfigurationHeaderProvider(this IRecaptchaBuilder builder,
            Action<RecaptchaConfigurationHeaderProvider.Options> setupOptions = null)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (setupOptions is null)
            {
                setupOptions = _ => { };
            }

            builder.Services.Configure(setupOptions);
            return builder.AddConfigurationHeaderProviderInner();
        }

        /// <summary>
        /// Specifies that configuration should be passed in http header.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
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

        internal static IRecaptchaBuilder AddConfigurationHeaderProviderInner(this IRecaptchaBuilder builder)
        {
            builder.Services.AddSingleton<IRecaptchaConfigurationProvider, RecaptchaConfigurationHeaderProvider>();
            return builder;
        }
    }
}
