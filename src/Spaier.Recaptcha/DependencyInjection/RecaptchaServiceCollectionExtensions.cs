using System;
using Spaier.Recaptcha;
using Spaier.Recaptcha.DependencyInjection;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Static class containing extension methods for asp.net core di.
    /// </summary>
    public static class RecaptchaServiceCollectionExtensions
    {
        public static IRecaptchaBuilder AddRecaptcha(this IServiceCollection serviceCollection)
        {
            if (serviceCollection is null)
                throw new ArgumentNullException(nameof(serviceCollection));

            serviceCollection.AddTransient<ValidateRecaptchaAttribute.InnerAttribute>();
            return new RecaptchaBuilder(serviceCollection);
        }
    }
}
