namespace Spaier.Recaptcha.Http
{
    public class GlobalVerifyUrlProvider : IVerifyUrlProvider
    {
        public string Url => RecaptchaDefaults.GlobalVerifyUrl;
    }
}
