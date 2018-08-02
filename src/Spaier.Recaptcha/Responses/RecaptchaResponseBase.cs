using Newtonsoft.Json;

namespace Spaier.Recaptcha.Responses
{
    /// <summary>
    /// Response from reCAPTCHA verify url.
    /// </summary>
    public abstract class RecaptchaResponseBase
    {
        /// <summary>
        /// Whether this request was a valid reCAPTCHA token for your site.
        /// </summary>
        [JsonProperty("success")]
        public bool IsSuccess { get; set; }

        /// <summary>
        /// Timestamp of the challenge load (ISO format yyyy-MM-dd'T'HH:mm:ssZZ).
        /// </summary>
        [JsonProperty("challenge_ts")]
        public string ChallengeTs { get; set; }

        /// <summary>
        /// Optional error codes.
        /// </summary>
        [JsonProperty("error-codes")]
        public string[] ErrorCodes { get; set; }
    }
}
