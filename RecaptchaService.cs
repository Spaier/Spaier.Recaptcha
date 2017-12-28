using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Spaier.Recaptcha
{
    /// <summary>
    /// Service for validating reCAPTCHA.
    /// </summary>
    public class RecaptchaService
    {
        private readonly RecaptchaOptions _recaptchaOptions;

        /// <summary>
        /// Current header key.
        /// </summary>
        public string RecaptchaHeaderKey => _recaptchaOptions?.RecaptchaHeaderKey;

        public RecaptchaService(IOptions<RecaptchaOptions> options)
        {
            _recaptchaOptions = options.Value;
        }

        /// <summary>
        /// Validate recaptcha <paramref name="clientResponse"/> using configured <see cref="RecaptchaOptions.VerifyUrl"/>.
        /// </summary>
        /// <param name="clientResponse"></param>
        /// <param name="remoteIp"></param>
        /// <returns>Task returning bool whether recaptcha is valid or not.</returns>
        public async Task<bool> ValidateRecaptcha(string clientResponse, string remoteIp)
        {
            const string secretKey = "secret";
            const string responseKey = "response";
            const string remoteIpKey = "remoteip";

            var request = new HttpRequestMessage();
            var parameters = new Dictionary<string, string>
            {
                [responseKey] = clientResponse,
                [remoteIpKey] = remoteIp
            };

            async Task<RecaptchaResponse?> SendRequest(string secret)
            {
                if (!string.IsNullOrWhiteSpace(secret))
                {
                    parameters[secretKey] = secret;
                    var content = new FormUrlEncodedContent(parameters);
                    using (var httpClient = new HttpClient())
                    {
                        var googleResponse = await httpClient.PostAsync(_recaptchaOptions.VerifyUrl, content);
                        var json = await googleResponse.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<RecaptchaResponse>(json);
                    }
                }
                return null;
            }
            var responses = new[]
            {
                await SendRequest(_recaptchaOptions.V2Secret),
                await SendRequest(_recaptchaOptions.InvisibleSecret),
                await SendRequest(_recaptchaOptions.AndroidSecret)
            };
            return responses.Any(w => w?.IsSuccess == true);
        }
    }
}
