using Microsoft.AspNetCore.Http;

namespace Spaier.Recaptcha.Services
{
    public interface IRecaptchaTokenProvider
    {
        /// <summary>
        /// Extracts the reCAPTCHA response from the http request.
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Recaptcha's client-side token(response from google api).</returns>
        string GetToken(HttpRequest request);
    }
}
