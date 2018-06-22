# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/)
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

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
- Add model errors from responses when reCAPTCHA is invalid.

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

[Unreleased]: https://github.com/Spaier/Spaier.Recaptcha/compare/1.1.0-rc0...HEAD
[1.1.0-rc0]: https://github.com/Spaier/Spaier.Recaptcha/compare/1.0.3...1.1.0-rc0
[1.0.3]: https://github.com/Spaier/Spaier.Recaptcha/compare/1.0.2...1.0.3
[1.0.2]: https://github.com/Spaier/Spaier.Recaptcha/compare/1.0.1...1.0.2
[1.0.1]: https://github.com/Spaier/Spaier.Recaptcha/compare/1.0.0...1.0.1