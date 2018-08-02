using Newtonsoft.Json;

namespace Spaier.Recaptcha.Responses
{
    public class RecaptchaResponseV2Android : RecaptchaResponseBase
    {
        /// <summary>
        /// The package name of the app where the reCAPTCHA was solved.
        /// </summary>
        [JsonProperty("apk_package_name")]
        public string ApkPackageName { get; set; }
    }
}
