using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Spaier.Recaptcha
{
    /// <summary>
    /// Response from reCAPTCHA verify url.
    /// </summary>
    public struct RecaptchaResponse
    {
        [JsonProperty("success")]
        public bool IsSuccess { get; set; }

        [JsonProperty("challenge_ts")]
        public string ChallengeTs { get; set; }

        [JsonProperty("error-codes")]
        public string[] ErrorCodes { get; set; }

        [JsonProperty("hostname")]
        public string HostName { get; set; }

        [JsonProperty("apk_package_name")]
        public string ApkPackageName { get; set; }
    }
}
