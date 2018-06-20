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

        public VerificationState AndroidVerification { get; set; }

        /// <summary>
        /// Backend secret of Invisible reCAPTCHA.
        /// </summary>
        public string InvisibleSecret { get; set; }

        public VerificationState InvisibleVerification { get; set; }

        /// <summary>
        /// Backend secret of V2 reCAPTCHA.
        /// </summary>
        public string V2Secret { get; set; }

        public VerificationState V2Verification { get; set; }

        /// <summary>
        /// Backend secret of V3 reCAPTCHA.
        /// </summary>
        public string V3Secret { get; set; }

        public VerificationState V3Verification { get; set; }

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
