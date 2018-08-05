namespace Spaier.Recaptcha
{
    /// <summary>
    /// Default constants.
    /// </summary>
    public static class RecaptchaDefaults
    {
        public const string VerifyApiPath = "/recaptcha/api/siteverify";

        public const string GlobalBaseUrl = "https://www.recaptcha.net";

        public const string GoogleBaseUrl = "https://www.google.com";

        /// <summary>
        /// Can be used instead of <see cref="GoogleVerifyUrl"/> if google.com is not accessible.
        /// </summary>
        public const string GlobalVerifyUrl = GlobalBaseUrl + VerifyApiPath;

        /// <summary>
        /// Standard verification url.
        /// </summary>
        public const string GoogleVerifyUrl = GoogleBaseUrl + VerifyApiPath;

        /// <summary>
        /// Default HTTP header key. Used if header wasn't specified in DI setup.
        /// </summary>
        public const string DefaultResponseHeaderKey = "g-recaptcha-response";

        /// <summary>
        /// Default HTTP header key. Used if header wasn't specified in DI setup.
        /// </summary>
        public const string DefaultConfigurationHeaderKey = "g-recaptcha-type";

        /// <summary>
        /// Secret that makes all requests path. Can be used for unit tests.
        /// </summary>
        public const string TestSecretKey = "6LeIxAcTAAAAAGG-vFI1TnRWxMZNFuojJ4WifJWe";

        /// <summary>
        /// Key that makes all requests path. Can be used for unit tests.
        /// </summary>
        public const string TestSiteKey = "6LeIxAcTAAAAAJcZVRqyHh71UMIEGNQ_MXjiZKhI";

        public const string LowScoreError = "recaptcha-low-score";

        public const string UnallowedActionError = "recaptcha-unallowed-action";

        public const string UnallowedConfigurationError = "recaptcha-unallowed-configuration";
    }
}
