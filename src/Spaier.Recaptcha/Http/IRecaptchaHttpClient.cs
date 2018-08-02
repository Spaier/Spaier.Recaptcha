using System;
using System.Threading.Tasks;
using Spaier.Recaptcha.Responses;

namespace Spaier.Recaptcha.Http
{
    public interface IRecaptchaHttpClient
    {
        Task<IRecaptchaResponse> VerifyRecaptchaAsync(Type type, string secret, string clientResponse, string remoteIp = null);
    }
}
