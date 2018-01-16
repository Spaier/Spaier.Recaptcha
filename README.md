# spaier-ng-recaptcha
[![Build Status](https://travis-ci.org/Spaier/Spaier.Recaptcha.svg?branch=master)](https://travis-ci.org/Spaier/Spaier.Recaptcha)
[![Nuget](https://img.shields.io/nuget/v/Spaier.Recaptcha.svg)](https://www.nuget.org/packages/Spaier.Recaptcha)
## Prerequisites

ASP.NET Core 2

## Table of Contents

* [Installation](#installation)
* [Usage](#usage)
* [License](#license)

## Installation

Nuget
```
Install-Package Spaier.Recaptcha
```

.NET Cli
```
dotnet add package Spaier.Recaptcha
```

## Usage

0. Add recaptcha services in your Startup.cs
```cs
public void ConfigureServices(IServiceCollection services) {
    // Your Code
    services.AddRecaptcha(options =>
    {
        options.AndroidSecret = "AndroidSecretKey";
        options.InvisibleSecret = "InvisibleSecretKey";
        options.V2Secret = "V2SecretKey";
        options.RecaptchaHeaderKey = "some-header";
    });
}
```

1. Add ValidateRecaptcha Attribute to any action you want to be protected by reCAPTCHA.
```cs
        [HttpPost]
        [AllowAnonymous]
        [ValidateRecaptcha]
        public async Task<IActionResult> SomeAction()
        {
            // Your Code
        }
```

Recaptcha response should be passed in HTTP header with specified key(default is g-recaptcha-response).

## License

MIT
