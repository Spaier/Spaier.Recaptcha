# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/)
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]

## [2.0.0]

## Fixed

- Null reference exceptions in `RecaptchaTokenHeaderProvider`

## [2.0.0-alpha2]

## Fixed

- Null reference exception in Razor MVC when action has parameters

## [2.0.0-alpha1]

## Added

- Pass recaptcha response with `FromRecaptchaResponseAttribute`
- Add V3 Score and Action integration
- Add configuration store interface
- Add InMemory configuration store

## Changed

- Use model errors instead of exceptions

## Fixed

- Model errors cause to return 400 error in web api

## Removed

- Remove success handler

## [2.0.0-alpha0]

## Added

- Add success handler(Can be used to check response)
- Add test reCAPTCHA secrets
- Add di builder
- Add implementations of `IRecaptchaConfigurationProvider` and `IRecaptchaTokenProvider` that extract info from HTTP headers

## Changed

- Send only one request to google api
- Recaptcha configuration should be determined by `IRecaptchaConfigurationProvider`
- Recaptcha token should be determined by `IRecaptchaTokenProvider`

## Removed

- Remove predefined configurations and attributes

## Fixed

- Fix Http client injection bug 

[Unreleased]: https://github.com/Spaier/Spaier.Recaptcha/compare/2.0.0...HEAD
[2.0.0]: https://github.com/Spaier/Spaier.Recaptcha/compare/2.0.0-alpha2...2.0.0
[2.0.0-alpha2]: https://github.com/Spaier/Spaier.Recaptcha/compare/2.0.0-alpha1...2.0.0-alpha2
[2.0.0-alpha1]: https://github.com/Spaier/Spaier.Recaptcha/compare/2.0.0-alpha0...2.0.0-alpha1