using System;

namespace Spaier.Recaptcha.Http
{
    public class CustomVerifyUrlProvider : IVerifyUrlProvider
    {
        public string Url { get; private set; }

        public CustomVerifyUrlProvider(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                throw new ArgumentException("Bad url", nameof(url));
            }

            Url = url;
        }
    }
}
