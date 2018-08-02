using Newtonsoft.Json;

namespace Spaier.Recaptcha.Responses
{
    public class RecaptchaResponseV2 : RecaptchaResponseBase
    {
        /// <summary>
        /// The hostname of the site where the reCAPTCHA was solved.
        /// </summary>
        [JsonProperty("hostname")]
        public string HostName { get; set; }
    }
}
