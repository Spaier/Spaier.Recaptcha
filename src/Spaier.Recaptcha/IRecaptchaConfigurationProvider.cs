using Microsoft.AspNetCore.Http;

namespace Spaier.Recaptcha
{
    public interface IRecaptchaConfigurationProvider
    {
        /// <summary>
        /// Retrieves configuration from <paramref name="request"/>.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        RecaptchaConfiguration GetRecaptchaConfiguration(HttpRequest request, out string key);
    }
}
