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
    services.AddRecaptcha()
        // Use appsettings.json
        //.AddInMemoryConfigurationStore(Configuration.GetSection("Recaptcha"))
        .AddInMemoryConfigurationStore(new Dictionary<string, RecaptchaConfiguration>
        {
            ["V3"] = new RecaptchaConfiguration(RecaptchaDefaults.TestSecretKey, RecaptchaSecretType.V3),
            ["V2"] = new RecaptchaConfiguration(RecaptchaDefaults.TestSecretKey, RecaptchaSecretType.V2),
            ["Android"] = new RecaptchaConfiguration(RecaptchaDefaults.TestSecretKey, RecaptchaSecretType.V2Android)
        })
        .AddTokenHeaderProvider()
        .AddConfigurationHeaderProvider()
        .AddRecaptchaHttpClient()
        .UseGoogleUrl();
}
```

2. Add ValidateRecaptcha Attribute to any action you want to be protected by reCAPTCHA.

```cs
        // Specify allowed configurations
        [ValidateRecaptcha(Configurations = new[] { "V2", "V3", "V2Android" })]
        public Task<ActionResult> Api1()
        {
            // Your code
        }
```

If token passed by client-side is invalid model errors will be added to `ModelState`.
See `RecaptchaErrorCodes`, `ValidateRecaptchaAttribute.ErrorCodes` and [official docs](https://developers.google.com/recaptcha/docs/verify)

3. Optionally specify allowed V3 action and minimum v3 score(defaults to 0.5).

You can pass recaptcha response to action by using `FromRecaptchaResponseAttribute` with 
any method parameter derived from `IRecaptchaResponse`.

```cs
        [HttpPost]
        [AllowAnonymous]
        [ValidateRecaptcha(Configurations = new[] { "V3", "V2" }, MinimumScore = 0.5, AllowedAction = "register")]
        public async Task<ActionResult> ProtectedByV3AndV2([FromRecaptchaResponse] RecaptchaResponse response)
        {
            // Your Code
        }
```

reCAPTCHA's response should be passed in HTTP header with the specified key or `g-recaptcha-response`.

reCAPTCHA's configuration key should be passed in HTTP header with the specified key or `g-recaptcha-type`
if there's more than one configuration in store or specified in attribute.

## License

MIT
