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
        // Actions can be used for any reCAPTCHA.
        [ValidateRecaptcha(Configurations = new[] { "Sitekey1" }, AllowedAction = "background", MinimumScore = 0.3)]
        [HttpPost]
        public Task<ActionResult<RecaptchaResponse>> SampleApi(SampleData data, [FromRecaptchaResponse] RecaptchaResponse recaptchaResponse)
        {
            Console.WriteLine(recaptchaResponse);
            return Task.FromResult(new ActionResult<RecaptchaResponse>(recaptchaResponse));
        }
    }

    public class SampleData
    {
        public string Field1 { get; set; }

        public int Field2 { get; set; }
    }
}
