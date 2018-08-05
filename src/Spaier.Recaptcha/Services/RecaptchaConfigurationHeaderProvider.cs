using System;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Spaier.Recaptcha.Services
{
    public class RecaptchaConfigurationHeaderProvider : IRecaptchaConfigurationProvider
    {
        private readonly Options options;

        public RecaptchaConfigurationHeaderProvider(IOptions<Options> options)
        {
            this.options = options?.Value ?? throw new ArgumentException(nameof(options));
        }

        public string GetRecaptchaConfigurationKey(HttpRequest request)
        {
            return request.Headers[options.HeaderKey];
        }

        public class Options
        {
            public string HeaderKey { get; set; } = RecaptchaDefaults.DefaultConfigurationHeaderKey;
        }
    }
}
