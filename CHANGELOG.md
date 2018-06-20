# Changelog
All notable changes to this project will be documented in this file.

## Unreleased

### Added

- Custom actions based on reCAPTCHA response.

## [1.1.0-rc0] - 20-06-2018

### Added

- V3 reCAPTCHA support.
- Add properties to `ValidateRecaptchaAttribute` to specify which versions of reCAPTCHA to use.
- Add validation attributes for specific versions of recaptcha.
- Add options to specify whether to enable or disable specific version of reCAPTCHA by default.

### Changed

- Use Typed client `HttpClient`. [Docs](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/http-requests?view=aspnetcore-2.1#typed-clients).
- Rewrite requests to google api with `Task.WaitAll`.

### Removed

- Frontend keys.
- recaptcha.js url constant.

## [1.0.3]

### Changed

- use ASP.NET Core 2.1.

## [1.0.2]

### Changed

- Add ConfigureAwait(false) to async calls.

## [1.0.1]

### Changed

- use ASP.NET Core 2.0.2.

## [1.0.0]

### Added

- I'm not a robot, Invisible and Android reCAPTCHA support.