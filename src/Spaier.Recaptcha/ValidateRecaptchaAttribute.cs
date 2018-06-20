using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace Spaier.Recaptcha
{
    public class ValidateRecaptchaAttribute : ActionFilterAttribute
    {
        public VerificationState VerifiesV2 { get; set; }

        public VerificationState VerifiesV2Invisible { get; set; }

        public VerificationState VerifiesV2Android { get; set; }

        public VerificationState VerifiesV3 { get; set; }

        /// <summary>
        /// Checks reCAPTCHA in header with key <see cref="RecaptchaOptions.RecaptchaHeaderKey"/>.
        /// If no token is present adds <see cref="ModelErrorCodes.NoRecaptchaResponseError"/> model error.
        /// If verification is unsuccessful adds model errors from <see cref="RecaptchaOptions.VerifyUrl"/> responses.
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
                var (isSuccess, responses) = await recaptchaService.ValidateRecaptcha(token, remoteIp,
                    VerifiesV2, VerifiesV2Invisible, VerifiesV2Android, VerifiesV3).ConfigureAwait(false);
                if (isSuccess)
                {
                    // TODO: Custom action?s
                    // someService(responses.First());
                }
                else
                {
                    foreach (var response in responses)
                    {
                        foreach (var error in response.ErrorCodes)
                        {
                            context.ModelState.AddModelError(error, null);
                        }
                    }
                }
            }
            else
            {
                context.ModelState.AddModelError(ModelErrorCodes.NoRecaptchaResponseError, "Recaptcha is missing");
            }
            await base.OnActionExecutionAsync(context, next).ConfigureAwait(false);
        }
    }

    public class ValidateRecaptchaV2Attribute : ValidateRecaptchaAttribute
    {
        public ValidateRecaptchaV2Attribute()
        {
            VerifiesV2 = VerificationState.Enabled;
            VerifiesV3 = VerificationState.Disabled;
            VerifiesV2Android = VerificationState.Disabled;
            VerifiesV2Invisible = VerificationState.Disabled;
        }
    }

    public class ValidateRecaptchaV2InvisibleAttribute : ValidateRecaptchaAttribute
    {
        public ValidateRecaptchaV2InvisibleAttribute()
        {
            VerifiesV2Invisible = VerificationState.Enabled;
            VerifiesV2Android = VerificationState.Disabled;
            VerifiesV2 = VerificationState.Disabled;
            VerifiesV3 = VerificationState.Disabled;
        }
    }

    public class ValidateRecaptchaV2AndroidAttribute : ValidateRecaptchaAttribute
    {
        public ValidateRecaptchaV2AndroidAttribute()
        {
            VerifiesV2Android = VerificationState.Enabled;
            VerifiesV2Invisible = VerificationState.Disabled;
            VerifiesV2 = VerificationState.Disabled;
            VerifiesV3 = VerificationState.Disabled;
        }
    }

    public class ValidateRecaptchaV3Attribute : ValidateRecaptchaAttribute
    {
        public ValidateRecaptchaV3Attribute()
        {
            VerifiesV3 = VerificationState.Enabled;
            VerifiesV2 = VerificationState.Disabled;
            VerifiesV2Android = VerificationState.Disabled;
            VerifiesV2Invisible = VerificationState.Disabled;
        }
    }
}
