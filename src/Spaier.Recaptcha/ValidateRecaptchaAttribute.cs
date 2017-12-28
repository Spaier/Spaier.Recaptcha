using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace Spaier.Recaptcha
{
    /// <summary>
    /// Validates <see cref="ActionExecutedContext"/>.
    /// </summary>
    public class ValidateRecaptchaAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// Create <see cref="ValidateRecaptchaAttribute"/> with <see cref="ActionFilterAttribute.Order"/> -10.
        /// </summary>
        public ValidateRecaptchaAttribute()
        {
            Order = -10;
        }

        /// <summary>
        /// Checks reCAPTCHA in header with key <see cref="RecaptchaOptions.RecaptchaHeaderKey"/>.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        public async override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var recaptchaService = context.HttpContext.RequestServices.GetService<RecaptchaService>();
            string token;
            if (!string.IsNullOrWhiteSpace(token = context.HttpContext.Request.Headers[recaptchaService.RecaptchaHeaderKey]))
            {
                var remoteIp = context.HttpContext.Connection.RemoteIpAddress.ToString();
                var result = await recaptchaService.ValidateRecaptcha(token, remoteIp);
                if (!result)
                {
                    context.ModelState.AddModelError("wrong-recaptcha", "Recaptcha check failed");
                }
            }
            else
            {
                context.ModelState.AddModelError("no-recaptcha", "Recaptcha is missing");
            }
            await base.OnActionExecutionAsync(context, next);
        }
    }
}
