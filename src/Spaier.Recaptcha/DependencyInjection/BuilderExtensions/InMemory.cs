using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Spaier.Recaptcha;
using Spaier.Recaptcha.DependencyInjection;
using Spaier.Recaptcha.Stores.InMemory;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class InMemoryRecaptchaBuilderExtensions
    {
        public static IRecaptchaBuilder AddInMemoryConfigurationStore(this IRecaptchaBuilder builder,
            IDictionary<string, RecaptchaConfiguration> configurations)
        {
            builder.Services.AddSingleton<IRecaptchaConfigurationStore, InMemoryRecaptchaConfigurationStore>(services
                => new InMemoryRecaptchaConfigurationStore(configurations));
            return builder;
        }

        public static IRecaptchaBuilder AddInMemoryConfigurationStore(this IRecaptchaBuilder builder,
            IConfiguration configuration)
        {
            var configurations = configuration.Get<Dictionary<string, RecaptchaConfiguration>>();
            return builder.AddInMemoryConfigurationStore(configurations);
        }
    }
}
