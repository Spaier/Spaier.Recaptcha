using Newtonsoft.Json;

namespace Spaier.Recaptcha.Responses
{
    public class RecaptchaResponse : IRecaptchaResponse
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

        /// <summary>
        /// The hostname of the site where the reCAPTCHA was solved.
        /// </summary>
        [JsonProperty("hostname")]
        public string HostName { get; set; }

        /// <summary>
        /// The package name of the app where the reCAPTCHA was solved.
        /// </summary>
        [JsonProperty("apk_package_name")]
        public string ApkPackageName { get; set; }

        /// <summary>
        /// The score for this request (0.0 - 1.0).
        /// </summary>
        [JsonProperty("score")]
        public double? Score { get; set; }

        /// <summary>
        /// The action name for this request (important to verify).
        /// </summary>
        [JsonProperty("action")]
        public string Action { get; set; }
    }
}
