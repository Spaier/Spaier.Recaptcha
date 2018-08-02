using Microsoft.Extensions.DependencyInjection;

namespace Spaier.Recaptcha.DependencyInjection
{
    /// <summary>
    /// Used for configuring recaptcha services.
    /// </summary>
    public interface IRecaptchaBuilder
    {
        /// <summary>
        /// Gets the <see cref="IServiceCollection"/> where recaptcha services are configured.
        /// </summary>
        IServiceCollection Services { get; }
    }
}