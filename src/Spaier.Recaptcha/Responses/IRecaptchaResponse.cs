using Newtonsoft.Json;

namespace Spaier.Recaptcha.Responses
{
    /// <summary>
    /// Response from reCAPTCHA verify url.
    /// </summary>
    public interface IRecaptchaResponse
    {
        /// <summary>
        /// Whether this request was a valid reCAPTCHA token for your site.
        /// </summary>
        [JsonProperty("success")]
        bool IsSuccess { get; set; }

        /// <summary>
        /// Timestamp of the challenge load (ISO format yyyy-MM-dd'T'HH:mm:ssZZ).
        /// </summary>
        [JsonProperty("challenge_ts")]
        string ChallengeTs { get; set; }

        /// <summary>
        /// Optional error codes.
        /// </summary>
        [JsonProperty("error-codes")]
        string[] ErrorCodes { get; set; }

        /// <summary>
        /// The hostname of the site where the reCAPTCHA was solved.
        /// </summary>
        [JsonProperty("hostname")]
        string HostName { get; set; }

        /// <summary>
        /// The package name of the app where the reCAPTCHA was solved.
        /// </summary>
        [JsonProperty("apk_package_name")]
        string ApkPackageName { get; set; }

        /// <summary>
        /// The score for this request (0.0 - 1.0).
        /// </summary>
        [JsonProperty("score")]
        double? Score { get; set; }

        /// <summary>
        /// The action name for this request (important to verify).
        /// </summary>
        [JsonProperty("action")]
        string Action { get; set; }
    }
}
