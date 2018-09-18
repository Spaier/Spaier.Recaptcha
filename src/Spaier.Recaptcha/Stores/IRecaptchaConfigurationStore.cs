using System.Collections.Generic;
using System.Threading.Tasks;

namespace Spaier.Recaptcha
{
    public interface IRecaptchaConfigurationStore
    {
        /// <summary>
        /// Tries to return configuration by key.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<(bool IsFound, RecaptchaConfiguration Configuration)> TryGetRecaptchaConfiguration(string key);

        /// <summary>
        /// Returns all configurations.
        /// </summary>
        /// <returns></returns>
        Task<IReadOnlyDictionary<string, RecaptchaConfiguration>> GetRecaptchaConfigurations();
    }
}
