using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using static Spaier.Recaptcha.RecaptchaResponses;

namespace Spaier.Recaptcha
{
    /// <summary>
    /// Service for validating reCAPTCHA.
    /// </summary>
    public class RecaptchaService : IDisposable
    {
        private readonly RecaptchaOptions _recaptchaOptions;
        private readonly HttpClient _httpClient;

        /// <summary>
        /// Current header key.
        /// </summary>
        public string RecaptchaHeaderKey => _recaptchaOptions?.RecaptchaHeaderKey;

        public RecaptchaService(IOptions<RecaptchaOptions> options, HttpClient httpClient)
        {
            _recaptchaOptions = options.Value;
            _httpClient = _httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        /// <summary>
        /// Validate reCAPTCHA's <paramref name="clientResponse"/> using <see cref="RecaptchaOptions.VerifyUrl"/>.
        /// </summary>
        /// <param name="clientResponse"></param>
        /// <param name="remoteIp"></param>
        /// <param name="verifiesV2"></param>
        /// <param name="verifiesV2Invisible"></param>
        /// <param name="verifiesV2Android"></param>
        /// <param name="verifiesV3"></param>
        /// <exception cref="RecaptchaNoSecretException"></exception>
        /// <returns></returns>
        public async Task<(bool IsSuccess, IEnumerable<RecaptchaResponseBase> Responses)> ValidateRecaptcha
            (string clientResponse, string remoteIp, VerificationState verifiesV2,
            VerificationState verifiesV2Invisible, VerificationState verifiesV2Android, VerificationState verifiesV3)
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

            async Task<TResponse> Check<TResponse>(string secret)
                where TResponse : class
            {
                parameters[secretKey] = secret;
                var content = new FormUrlEncodedContent(parameters);

                var googleResponse = await _httpClient
                    .PostAsync(_recaptchaOptions.VerifyUrl, content)
                    .ConfigureAwait(false);

                googleResponse.EnsureSuccessStatusCode();

                return await googleResponse.Content
                    .ReadAsAsync<TResponse>()
                    .ConfigureAwait(false);
            }

            List<Task> tasks = new List<Task>();

            void AddCheckTask<TResponse>(VerificationState currentVerification, VerificationState defaultVerification, string secret)
                where TResponse : class
            {
                var isNoSecret = string.IsNullOrWhiteSpace(secret);
                void SwitchOnState(bool isEnabled = true)
                {
                    if (isEnabled && isNoSecret)
                    {
                        throw new RecaptchaNoSecretException();
                    }
                    else if (!isNoSecret)
                    {
                        tasks.Add(Check<TResponse>(secret));
                    }
                }
                switch (currentVerification)
                {
                    case VerificationState.Enabled:
                        SwitchOnState();
                        break;
                    case VerificationState.Default:
                        switch (defaultVerification)
                        {
                            case VerificationState.Enabled:
                                SwitchOnState();
                                break;
                            case VerificationState.Default:
                                SwitchOnState(false);
                                break;
                        }
                        break;
                }
            }

            AddCheckTask<RecaptchaV2Response>(verifiesV2, _recaptchaOptions.V2Verification, _recaptchaOptions.V2Secret);
            AddCheckTask<RecaptchaV2AndroidResponse>(verifiesV2Android, _recaptchaOptions.AndroidVerification, _recaptchaOptions.AndroidSecret);
            AddCheckTask<RecaptchaV2Response>(verifiesV2Invisible, _recaptchaOptions.InvisibleVerification, _recaptchaOptions.InvisibleSecret);
            AddCheckTask<RecaptchaV3Response>(verifiesV3, _recaptchaOptions.V3Verification, _recaptchaOptions.V3Secret);

            if (tasks.Count == 0) throw new RecaptchaVerificationException();

            await Task.WhenAll(tasks).ConfigureAwait(false);
            List<RecaptchaResponseBase> responses = new List<RecaptchaResponseBase>();
            foreach (Task<RecaptchaResponseBase> task in tasks)
            {
                if (task.Result.IsSuccess)
                {
                    return (true, new[] { task.Result });
                }
                else
                {
                    responses.Add(task.Result);
                }
            }

            return (false, responses);
        }

        public void Dispose()
        {
            _httpClient?.Dispose();
        }
    }
}
