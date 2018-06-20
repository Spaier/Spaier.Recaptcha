using System;
using Microsoft.Extensions.Configuration;
using Spaier.Recaptcha;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Static class containing extension methods for asp.net core di.
    /// </summary>
    public static class RecaptchaServiceCollectionExtensions
    {
        /// <summary>
        /// Adds reCAPTCHA services to AspNetCore DI.
        /// </summary>
        /// <param name="serviceCollection"></param>
        /// <param name="configuration"></param>
        public static void AddRecaptcha(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.Configure<RecaptchaOptions>(configuration);
            AddRecaptcha(serviceCollection);
        }

        /// <summary>
        /// Adds reCAPTCHA services to AspNetCore DI.
        /// </summary>
        /// <param name="serviceCollection"></param>
        /// <param name="setupOptions"></param>
        public static void AddRecaptcha(this IServiceCollection serviceCollection, Action<RecaptchaOptions> setupOptions)
        {
            serviceCollection.Configure(setupOptions);
            AddRecaptcha(serviceCollection);
        }

        /// <summary>
        /// Adds reCAPTCHA services to AspNetCore DI.
        /// </summary>
        /// <param name="serviceCollection"></param>
        public static void AddRecaptcha(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<RecaptchaService>();
        }
    }
}
