using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Spaier.Recaptcha.Responses;

namespace Spaier.Recaptcha.Http
{
    public class RecaptchaHttpClient : IRecaptchaHttpClient
    {
        private const string SecretKey = "secret";
        private const string ResponseKey = "response";
        private const string RemoteIpKey = "remoteip";

        private readonly HttpClient httpClient;
        private readonly string url;

        public RecaptchaHttpClient(HttpClient httpClient, IVerifyUrlProvider urlProvider)
        {
            this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            url = urlProvider.Url ?? string.Empty;
        }

        public async Task<IRecaptchaResponse> VerifyRecaptchaAsync(Type type, string secret, string clientResponse, string remoteIp = null)
        {
            var parameters = new Dictionary<string, string>
            {
                [ResponseKey] = clientResponse,
                [SecretKey] = secret,
                [RemoteIpKey] = remoteIp,
            };

            var content = new FormUrlEncodedContent(parameters);

            var googleResponse = await httpClient.PostAsync(url, content);

            googleResponse.EnsureSuccessStatusCode();

            return (IRecaptchaResponse)await googleResponse.Content.ReadAsAsync(type);
        }
    }
}
