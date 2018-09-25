using System;
using System.Net.Http;
using Spaier.Recaptcha;
using Spaier.Recaptcha.DependencyInjection;
using Spaier.Recaptcha.Http;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class HttpRecaptchaBuilderExtensions
    {
        /// <summary>
        /// Provides custom url to <see cref="IRecaptchaHttpClient"/>.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="url">custom url.</param>
        /// <returns></returns>
        public static IRecaptchaBuilder UseCustomUrl(this IRecaptchaBuilder builder, string url)
        {
            builder.Services.AddSingleton<IVerifyUrlProvider>(sp => new VerifyUrlProvider(url));
            return builder;
        }

        /// <summary>
        /// Provides <see cref="RecaptchaDefaults.GoogleVerifyUrl"/> to <see cref="IRecaptchaHttpClient"/>.
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IRecaptchaBuilder UseGoogleUrl(this IRecaptchaBuilder builder)
        {
            builder.Services.AddSingleton<IVerifyUrlProvider>(sp => new VerifyUrlProvider(RecaptchaDefaults.GoogleVerifyUrl));
            return builder;
        }

        /// <summary>
        /// Provides <see cref="RecaptchaDefaults.GlobalVerifyUrl"/> to <see cref="IRecaptchaHttpClient"/>.
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IRecaptchaBuilder UseGlobalUrl(this IRecaptchaBuilder builder)
        {
            builder.Services.AddSingleton<IVerifyUrlProvider>(sp => new VerifyUrlProvider(RecaptchaDefaults.GlobalVerifyUrl));
            return builder;
        }

        /// <summary>
        /// Adds <see cref="IRecaptchaHttpClient"/> implementation.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="name"></param>
        /// <param name="configureClient"></param>
        /// <param name="configureHttpBuilder">You can configure retry policies here.</param>
        /// <returns></returns>
        public static IRecaptchaBuilder AddRecaptchaHttpClient(this IRecaptchaBuilder builder,
            string name = null, Action<HttpClient> configureClient = null, Action<IHttpClientBuilder> configureHttpBuilder = null)
        {
            return builder.AddRecaptchaHttpClient<RecaptchaHttpClient>(name, configureClient, configureHttpBuilder);
        }

        /// <summary>
        /// Adds <see cref="IRecaptchaHttpClient"/> implementation.
        /// </summary>
        /// <typeparam name="T"><see cref="IRecaptchaHttpClient"/> implementation.</typeparam>
        /// <param name="builder"></param>
        /// <param name="name"></param>
        /// <param name="configureClient"></param>
        /// <param name="configureHttpBuilder">You can configure retry policies here.</param>
        /// <returns></returns>
        public static IRecaptchaBuilder AddRecaptchaHttpClient<T>(this IRecaptchaBuilder builder,
            string name = null, Action<HttpClient> configureClient = null, Action<IHttpClientBuilder> configureHttpBuilder = null)
            where T : class, IRecaptchaHttpClient
        {
            var httpBuilder = name is null
                ? configureClient is null
                    ? builder.Services.AddHttpClient<IRecaptchaHttpClient, T>()
                    : builder.Services.AddHttpClient<IRecaptchaHttpClient, T>(configureClient)
                : configureClient is null
                    ? builder.Services.AddHttpClient<IRecaptchaHttpClient, T>(name)
                    : builder.Services.AddHttpClient<IRecaptchaHttpClient, T>(name, configureClient);

            if (!(configureHttpBuilder is null))
            {
                configureHttpBuilder(httpBuilder);
            }

            return builder;
        }
    }
}
