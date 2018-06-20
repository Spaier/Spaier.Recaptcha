namespace Spaier.Recaptcha
{
    /// <summary>
    /// Default constants.
    /// </summary>
    public static class RecaptchaDefaults
    {
        /// <summary>
        /// Default URL for verifying reCAPTCHA.
        /// </summary>
        public const string VerifyUrl = "https://www.google.com/recaptcha/api/siteverify";

        /// <summary>
        /// Default HTTP header key for reCAPTCHA response.
        /// </summary>
        public const string RecaptchaHeaderKey = "g-recaptcha-response";
    }
}
