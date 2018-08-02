using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Spaier.Recaptcha;
using Spaier.Recaptcha.Http;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiSample.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SampleController : Controller
    {
        private readonly IRecaptchaConfigurationProvider configurationProvider;
        private readonly IRecaptchaTokenProvider tokenProvider;
        private readonly IRecaptchaHttpClient httpClient;

        public SampleController(IRecaptchaConfigurationProvider configurationProvider,
            IRecaptchaTokenProvider tokenProvider, IRecaptchaHttpClient httpClient)
        {
            this.configurationProvider = configurationProvider ?? throw new ArgumentNullException(nameof(configurationProvider));
            this.tokenProvider = tokenProvider ?? throw new ArgumentNullException(nameof(tokenProvider));
            this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        // Manual.
        public void Api1()
        {
            httpClient.VerifyRecaptchaAsync(configurationProvider.GetRecaptchaConfiguration(Request, out _), Request, tokenProvider);
        }

        // Any specified configuration
        [ValidateRecaptcha]
        public void Api2()
        {
        }

        // Only V3 reCAPTCHA specified in Startup.cs is allowed
        [ValidateRecaptcha(Configurations = new[] { "V3" })]
        public void Api3()
        {
        }
    }
}
