namespace Spaier.Recaptcha
{
    public static class RecaptchaErrorCodes
    {
        /// <summary>
        /// The secret parameter is missing.
        /// </summary>
        public const string MissingInputSecret = "missing-input-secret";

        /// <summary>
        /// The secret parameter is invalid or malformed.
        /// </summary>
        public const string InvalidInputSecret = "invalid-input-secret";

        /// <summary>
        /// The response parameter is missing.
        /// </summary>
        public const string MissingInputResponse = "missing-input-response";

        /// <summary>
        /// The response parameter is invalid or malformed.
        /// </summary>
        public const string InvalidInputResponse = "invalid-input-response";

        /// <summary>
        /// The request is invalid or malformed.
        /// </summary>
        public const string BadRequest = "bad-request";

        /// <summary>
        /// The request is a duplicate or it has expired.
        /// </summary>
        public const string TimeoutOrDuplicate = "timeout-or-duplicate";
    }
}
