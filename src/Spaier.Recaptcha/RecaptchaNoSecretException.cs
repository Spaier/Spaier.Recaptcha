using System;
using System.Runtime.Serialization;

namespace Spaier.Recaptcha
{
    public class RecaptchaNoSecretException : Exception
    {
        public RecaptchaNoSecretException() { }

        protected RecaptchaNoSecretException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }

        public RecaptchaNoSecretException(string message) : base(message) { }

        public RecaptchaNoSecretException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
