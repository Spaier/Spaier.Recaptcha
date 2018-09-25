using System;
using Microsoft.Extensions.DependencyInjection;

namespace Spaier.Recaptcha.DependencyInjection
{
    internal sealed class RecaptchaBuilder : IRecaptchaBuilder
    {
        public IServiceCollection Services { get; }

        public RecaptchaBuilder(IServiceCollection services)
        {
            Services = services ?? throw new ArgumentNullException(nameof(services));
        }
    }
}