using Microsoft.AspNetCore.Mvc.Filters;
using Spaier.Recaptcha.Responses;

namespace Spaier.Recaptcha
{
    public interface IRecaptchaSuccessHandler
    {
        void OnSuccess(IRecaptchaResponse response, ActionExecutingContext context);
    }
}
