using System;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Spaier.Recaptcha.Services
{
    public class RecaptchaTokenHeaderProvider : IRecaptchaTokenProvider
    {
        private readonly string recaptchaHeaderKey;

        public RecaptchaTokenHeaderProvider(IOptions<Options> options)
        {
            recaptchaHeaderKey = options?.Value?.HeaderKey ?? throw new ArgumentException(nameof(options));
        }

        public string GetToken(HttpRequest request)
        {
            return request?.Headers[recaptchaHeaderKey];
        }

        public class Options
        {
            public string HeaderKey { get; set; } = RecaptchaDefaults.DefaultResponseHeaderKey;
        }
    }
}
