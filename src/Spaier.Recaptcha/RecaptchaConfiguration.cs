using System;

namespace Spaier.Recaptcha
{
    public class RecaptchaConfiguration
    {
        /// <summary>
        /// Back-end reCAPTCHA secret.
        /// </summary>
        public string Secret { get; set; }

        /// <summary>
        /// Type of a reCAPTCHA key.
        /// </summary>
        [Obsolete]
        public RecaptchaSecretType SecretType { get; set; }

        public RecaptchaConfiguration() { }

        public RecaptchaConfiguration(string secret)
        {
            Secret = secret ?? throw new ArgumentNullException(nameof(secret));
        }

        [Obsolete]
        public RecaptchaConfiguration(string secret, RecaptchaSecretType secretType) : this(secret)
        {
            SecretType = Enum.IsDefined(typeof(RecaptchaSecretType), secretType)
                ? secretType : throw new ArgumentException(nameof(secretType));
        }
    }
}
