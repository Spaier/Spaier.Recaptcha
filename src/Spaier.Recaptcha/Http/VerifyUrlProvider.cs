using System;

namespace Spaier.Recaptcha.Http
{
    internal class VerifyUrlProvider : IVerifyUrlProvider
    {
        public string Url { get; private set; }

        public VerifyUrlProvider(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                throw new ArgumentException("Bad url", nameof(url));
            }

            Url = url;
        }
    }
}
