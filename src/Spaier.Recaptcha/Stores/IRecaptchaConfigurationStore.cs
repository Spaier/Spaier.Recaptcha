using System.Collections.Generic;
using System.Threading.Tasks;

namespace Spaier.Recaptcha
{
    public interface IRecaptchaConfigurationStore
    {
        /// <summary>
        /// Retrieves configuration by key.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<RecaptchaConfiguration> GetRecaptchaConfiguration(string key);

        /// <summary>
        /// Returns all configurations.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<RecaptchaConfiguration>> GetRecaptchaConfigurations();
    }
}
