using System;
using Spaier.Recaptcha.Responses;

namespace Spaier.Recaptcha
{
    public static class RecaptchaSecretTypeExtensions
    {
        public static Type GetResponseType(this RecaptchaSecretType secretType)
        {
            switch (secretType)
            {
                case RecaptchaSecretType.V2:
                    return typeof(RecaptchaResponseV2);
                case RecaptchaSecretType.V2Android:
                    return typeof(RecaptchaResponseV2Android);
                case RecaptchaSecretType.V3:
                    return typeof(RecaptchaResponseV3);
                default:
                    throw new RecaptchaConfigurationException();
            }
        }
    }
}
