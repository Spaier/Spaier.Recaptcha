using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Spaier.Recaptcha
{
    public class RecaptchaConfigurationHeaderProvider : IRecaptchaConfigurationProvider
    {
        private readonly Options options;

        public RecaptchaConfigurationHeaderProvider(IOptions<Options> options)
        {
            this.options = options?.Value ?? throw new ArgumentException(nameof(options));
        }

        public RecaptchaConfiguration GetRecaptchaConfiguration(HttpRequest request, out string key)
        {
            key = request.Headers[options.HeaderKey];
            var result = options.Configurations.TryGetValue(key, out var configuration);
            return result ? configuration : throw new RecaptchaConfigurationException("Invalid configuration key");
        }

        public class Options
        {
            public string HeaderKey { get; set; } = RecaptchaDefaults.DefaultConfigurationHeaderKey;

            public Dictionary<string, RecaptchaConfiguration> Configurations { get; set; }
        }
    }
}
