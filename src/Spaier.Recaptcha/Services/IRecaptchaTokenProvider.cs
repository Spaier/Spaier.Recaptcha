using Microsoft.AspNetCore.Http;

namespace Spaier.Recaptcha.Services
{
    public interface IRecaptchaTokenProvider
    {
        /// <summary>
        /// Extracts token from http request.
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Recaptcha's client-side token(response from google api).</returns>
        string GetToken(HttpRequest request);
    }
}
