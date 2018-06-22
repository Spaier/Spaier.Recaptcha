# Spaier.Recaptcha

[![Build Status](https://travis-ci.org/Spaier/Spaier.Recaptcha.svg?branch=master)](https://travis-ci.org/Spaier/Spaier.Recaptcha)
[![Nuget](https://img.shields.io/nuget/v/Spaier.Recaptcha.svg)](https://www.nuget.org/packages/Spaier.Recaptcha)

## Prerequisites

- ASP.NET Core 2.1
- Nuget
- Recaptcha key ([V3](https://g.co/recaptcha/v3) or [V2](https://www.google.com/recaptcha/admin))

## Table of Contents

* [Installation](#installation)
* [Usage](#usage)
* [License](#license)

## Installation

Nuget

```
Install-Package Spaier.Recaptcha
```

.NET CLI

```
dotnet add package Spaier.Recaptcha
```

## Usage

1. Add recaptcha services in your Startup.cs

```cs
public void ConfigureServices(IServiceCollection services) {
    // Your Code
    services.AddRecaptcha(options =>
    {
        options.AndroidSecret = "AndroidSecretKey";
        options.InvisibleSecret = "InvisibleSecretKey";
        options.V2Secret = "V2SecretKey";
        options.V3Secret = "V3SecretKey";
        // Optionally you can set header name. Default one is "g-recaptcha-response"
        // options.RecaptchaHeaderKey = "some-header";
        // Optionally specify default verification strategy
        options.V3Verification = VerificationState.Disabled;
        options.V2Verification = VerificationState.Enabled;
    });
}
```

2. Add ValidateRecaptcha Attribute to any action you want to be protected by reCAPTCHA.

```cs
        [HttpPost]
        [AllowAnonymous]
        [ValidateRecaptcha]
        public async Task<ActionResult> SomeAction()
        {
            // Your Code
        }
```

If token passed by client-side is invalid model errors will be added to `ModelState`.
See `RecaptchaErrorCodes` and [official docs](https://developers.google.com/recaptcha/docs/verify)

If no token is passed `NoRecaptchaResponseError = "no-recaptcha-response"` error will be added to `ModelState`.

3. Optionally specify reCAPTCHA types to use in verification. By default if secret is provided it will be used.

```cs
        [HttpPost]
        [AllowAnonymous]
        [ValidateV2Recaptcha]
        public async Task<ActionResult> ProtectedByV2()
        {
            // Your Code
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateV2InvisibleRecaptcha]
        public async Task<ActionResult> ProtectedByV2Invisible()
        {
            // Your Code
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateV2AndroidRecaptcha]
        public async Task<ActionResult> ProtectedByV2Android()
        {
            // Your Code
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateV3Recaptcha]
        public async Task<ActionResult> ProtectedByV3()
        {
            // Your Code
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateRecaptcha(
            VerifiesV2 = VerificationState.Enabled,
            VerifiesV3 = VerificationState.Enabled,
            VerifiesV2Invisible = VerificationState.Disabled,
            VerifiesV2Android = VerificationState.Disabled)]
        public async Task<ActionResult> ProtectedByV2AndV3()
        {
            // Your Code
        }
```

reCAPTCHA's response should be passed in HTTP header with specified key or `g-recaptcha-response`.

## License

MIT
