using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Spaier.Recaptcha.Stores.InMemory
{
    public class InMemoryRecaptchaConfigurationStore : IRecaptchaConfigurationStore
    {
        private readonly IDictionary<string, RecaptchaConfiguration> configurations;

        public InMemoryRecaptchaConfigurationStore(IDictionary<string, RecaptchaConfiguration> configurations)
        {
            this.configurations = configurations;
        }

        public Task<(bool IsFound, RecaptchaConfiguration Configuration)> TryGetRecaptchaConfiguration(string key)
        {
            return Task.FromResult((configurations.TryGetValue(key, out var configuration), configuration));
        }

        public Task<IReadOnlyDictionary<string, RecaptchaConfiguration>> GetRecaptchaConfigurations()
        {
            return Task.FromResult((IReadOnlyDictionary<string, RecaptchaConfiguration>)
                new ReadOnlyDictionary<string, RecaptchaConfiguration>(configurations));
        }
    }
}
