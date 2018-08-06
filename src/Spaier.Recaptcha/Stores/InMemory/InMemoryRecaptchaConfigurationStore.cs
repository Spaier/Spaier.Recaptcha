using System.Collections.Generic;
using System.Linq;
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

        public Task<RecaptchaConfiguration> GetRecaptchaConfiguration(string key)
        {
            return Task.FromResult(configurations[key]);
        }

        public Task<IEnumerable<RecaptchaConfiguration>> GetRecaptchaConfigurations()
        {
            return Task.FromResult(configurations.Values.AsEnumerable());
        }
    }
}
