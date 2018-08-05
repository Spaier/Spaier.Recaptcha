using Microsoft.AspNetCore.Http;

namespace Spaier.Recaptcha.Services
{
    public interface IRecaptchaConfigurationProvider
    {
        /// <summary>
        /// Retrieves configuration key from <paramref name="request"/>.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        string GetRecaptchaConfigurationKey(HttpRequest request);
    }
}
