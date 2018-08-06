using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Spaier.Recaptcha;
using Spaier.Recaptcha.Responses;

namespace WebApiSample.Controllers
{
    [Route("api/[controller]/[action]")]
    [Produces("application/json")]
    [ApiController]
    public class SampleController : ControllerBase
    {
        // Any specified configuration
        [ValidateRecaptcha]
        public Task<ActionResult> Api1()
        {
            return Task.FromResult((ActionResult)NoContent());
        }

        // Only V3 reCAPTCHA specified in Startup.cs is allowed
        [ValidateRecaptcha(Configurations = new[] { "V3" }, MinimumScore = 0, AllowedAction = "api2")]
        public Task<ActionResult> Api2([FromRecaptchaResponse] RecaptchaResponseV3 recaptchaResponse)
        {
            Console.WriteLine(recaptchaResponse);
            return Task.FromResult((ActionResult)NoContent());
        }
    }
}
