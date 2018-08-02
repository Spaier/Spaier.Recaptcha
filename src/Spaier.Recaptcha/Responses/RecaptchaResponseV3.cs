using Newtonsoft.Json;

namespace Spaier.Recaptcha.Responses
{
    public class RecaptchaResponseV3 : RecaptchaResponseV2
    {
        /// <summary>
        /// The score for this request (0.0 - 1.0).
        /// </summary>
        [JsonProperty("score")]
        public double Score { get; set; }

        /// <summary>
        /// The action name for this request (important to verify).
        /// </summary>
        [JsonProperty("action")]
        public string Action { get; set; }
    }
}
