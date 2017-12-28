namespace Spaier.Recaptcha
{
    /// <summary>
    /// Configuration for reCAPTCHA.
    /// </summary>
    public class RecaptchaOptions
    {
        /// <summary>
        /// Backend secret of Android reCAPTCHA.
        /// </summary>
        public string AndroidSecret { get; set; }

        /// <summary>
        /// Android application secret of Android reCAPTCHA.
        /// </summary>
        public string AndroidKey { get; set; }

        /// <summary>
        /// Backend secret of Invisible reCAPTCHA.
        /// </summary>
        public string InvisibleSecret { get; set; }

        /// <summary>
        /// Frontend key of Invisible reCAPTCHA.
        /// </summary>
        public string InvisibleKey { get; set; }

        /// <summary>
        /// Backend secret of V2 reCAPTCHA.
        /// </summary>
        public string V2Secret { get; set; }

        /// <summary>
        /// Frontend key of V2 reCAPTCHA.
        /// </summary>
        public string V2Key { get; set; }

        /// <summary>
        /// URL for verifying reCAPTCHA.
        /// </summary>
        public string VerifyUrl { get; set; } = RecaptchaDefaults.VerifyUrl;

        /// <summary>
        /// Where reCAPTCHA's response is stored in HTTP header.
        /// </summary>
        public string RecaptchaHeaderKey { get; set; } = RecaptchaDefaults.RecaptchaHeaderKey;
    }
}
