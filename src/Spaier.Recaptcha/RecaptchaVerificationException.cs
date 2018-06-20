using System;
using System.Runtime.Serialization;

namespace Spaier.Recaptcha
{
    /// <summary>
    /// Thrown when no reCAPTCHA's verification is enabled.
    /// </summary>
    public class RecaptchaVerificationException : Exception
    {
        public RecaptchaVerificationException() { }

        protected RecaptchaVerificationException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }

        public RecaptchaVerificationException(string message) : base(message) { }

        public RecaptchaVerificationException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
