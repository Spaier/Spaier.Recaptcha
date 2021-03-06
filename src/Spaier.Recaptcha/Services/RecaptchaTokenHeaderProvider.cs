﻿using System;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Spaier.Recaptcha.Services
{
    public sealed class RecaptchaTokenHeaderProvider : IRecaptchaTokenProvider
    {
        private readonly string recaptchaResponseHeaderKey;

        public RecaptchaTokenHeaderProvider(IOptions<Options> options)
        {
            recaptchaResponseHeaderKey = options?.Value?.HeaderKey ?? throw new ArgumentException(nameof(options));
        }

        public string GetToken(HttpRequest request)
        {
            return request?.Headers[recaptchaResponseHeaderKey];
        }

        public sealed class Options
        {
            public string HeaderKey { get; set; } = RecaptchaDefaults.DefaultResponseHeaderKey;
        }
    }
}
