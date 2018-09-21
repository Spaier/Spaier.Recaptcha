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
        [ValidateRecaptcha(
            Configurations = new[] { "Sitekey1" },
            AllowedAction = "background",
            MinimumScore = 0.3,
            UseModelErrors = false)]
        [HttpPost]
        public Task<ActionResult<RecaptchaResponse>> DataApi(SampleData data, [FromRecaptchaResponse] RecaptchaResponse recaptchaResponse)
        {
            Console.WriteLine(recaptchaResponse);
            return Task.FromResult(new ActionResult<RecaptchaResponse>(recaptchaResponse));
        }

        [ValidateRecaptcha(
            Configurations = new[] { "Sitekey1" },
            AllowedAction = "background",
            MinimumScore = 0.2)]
        [HttpPost]
        public ActionResult EmptyApi([FromRecaptchaResponse] RecaptchaResponse recaptchaResponse)
        {
            return NoContent();
        }

        [ValidateRecaptcha(
            Configurations = new[] { "Sitekey1", "Sitekey2" },
            AllowedAction = "background")]
        [HttpPost]
        public ActionResult MultiConfigApi([FromRecaptchaResponse] RecaptchaResponse recaptchaResponse)
        {
            return NoContent();
        }
    }

    public class SampleData
    {
        public string Field1 { get; set; }

        public int Field2 { get; set; }
    }
}
