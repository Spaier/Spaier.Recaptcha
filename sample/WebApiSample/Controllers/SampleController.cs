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
        /// <summary>
        /// Configuration can be omitted.
        /// background action is allowed.
        /// Sitekey1 is allowed.
        /// Errors aren't added to ModelState.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="recaptchaResponse"></param>
        /// <returns></returns>
        [ValidateRecaptcha(
            Configurations = new[] { "Sitekey1" },
            AllowedAction = "background",
            MinimumScore = 0.3)]
        [HttpPost]
        public Task<ActionResult<RecaptchaResponse>> SampleApi(SampleData data, [FromRecaptchaResponse] RecaptchaResponse recaptchaResponse)
        {
            Console.WriteLine(recaptchaResponse);
            return Task.FromResult(new ActionResult<RecaptchaResponse>(recaptchaResponse));
        }

        [ValidateRecaptcha(
            UseModelErrors = false)]
        [HttpPost]
        public ActionResult NoConfigApi([FromRecaptchaResponse] RecaptchaResponse recaptchaResponse)
        {
            return Ok(recaptchaResponse);
        }

        [ValidateRecaptcha(
            Configurations = new[] { "Sitekey1" },
            UseModelErrors = false)]
        [HttpPost]
        public ActionResult OneConfigApi([FromRecaptchaResponse] RecaptchaResponse recaptchaResponse)
        {
            return Ok(recaptchaResponse);
        }

        [ValidateRecaptcha(
            Configurations = new[] { "Sitekey1", "Sitekey2" },
            UseModelErrors = false)]
        [HttpPost]
        public ActionResult MultiConfigApi([FromRecaptchaResponse] RecaptchaResponse recaptchaResponse)
        {
            return Ok(recaptchaResponse);
        }
    }

    public class SampleData
    {
        public string Field1 { get; set; }

        public int Field2 { get; set; }
    }
}
