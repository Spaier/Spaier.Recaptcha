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
            ["Sitekey1"] = new RecaptchaConfiguration(RecaptchaDefaults.TestSecretKey),
            ["Sitekey2"] = new RecaptchaConfiguration(RecaptchaDefaults.TestSecretKey),
            ["Sitekey3"] = new RecaptchaConfiguration(RecaptchaDefaults.TestSecretKey)
        })
        .AddTokenHeaderProvider()
        .AddConfigurationHeaderProvider()
        .AddRecaptchaHttpClient()
        .UseGoogleUrl();
		// UseGlobalUrl(); // will use recaptcha.net mirror. Useful for countries where google.com is blocked.
        // UseCustomUrl("your_url"); // will use custom url for validation.
}
```

2. Apply `ValidateRecaptcha` attribute to an action.

`Configurations` defines allowed configurations for an action.
If none is specified you can use any configuration.
If only one is specified you can omit configuration token.

`AllowedAction` works with V2 or V3.
Don't specify to skip action check.

`MinimumScore` works with V3.
Defaults to `0.5`.

`UseModelErrors` determines whether errors will be added to MVC Model.
True by default.

You can pass a recaptcha response to an action by using the `FromRecaptchaResponseAttribute` with 
any method parameter derived from the `IRecaptchaResponse`.

```cs
        [HttpPost]
        [AllowAnonymous]
        [ValidateRecaptcha(Configurations = new[] { "Sitekey1", "Sitekey2" }, MinimumScore = 0.7, AllowedAction = "register")]
        public async Task<ActionResult> ProtectedByV3AndV2([FromRecaptchaResponse] RecaptchaResponse response)
        {
            // Your Code
        }
```

A reCAPTCHA's response should be passed in a HTTP header with the specified key or `g-recaptcha-response`.

A reCAPTCHA's configuration key should be passed in a HTTP header with the specified key or `g-recaptcha-type`
if there's more than one configuration in store or specified in `Configurations`.

If token passed by client-side is invalid model errors will be added to `ModelState`.
See `RecaptchaErrorCodes`, `ValidateRecaptchaAttribute.ErrorCodes` and [official docs](https://developers.google.com/recaptcha/docs/verify)

## License

MIT
