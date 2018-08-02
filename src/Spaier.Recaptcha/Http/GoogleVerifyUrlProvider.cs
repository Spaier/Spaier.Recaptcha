namespace Spaier.Recaptcha.Http
{
    public class GoogleVerifyUrlProvider : IVerifyUrlProvider
    {
        public string Url => RecaptchaDefaults.GoogleVerifyUrl;
    }
}
