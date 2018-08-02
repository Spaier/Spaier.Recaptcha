using System;
using System.Runtime.Serialization;

namespace Spaier.Recaptcha
{
    public class RecaptchaConfigurationException : Exception
    {
        public RecaptchaConfigurationException()
        {
        }

        protected RecaptchaConfigurationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public RecaptchaConfigurationException(string message) : base(message)
        {
        }

        public RecaptchaConfigurationException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
