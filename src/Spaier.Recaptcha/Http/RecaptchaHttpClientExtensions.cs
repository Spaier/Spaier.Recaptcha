using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Spaier.Recaptcha.Responses;

namespace Spaier.Recaptcha.Http
{
    public static class RecaptchaHttpClientExtensions
    {
        public static async Task<TResponse> VerifyRecaptchaAsync<TResponse>(this IRecaptchaHttpClient httpClient,
            string secret, string clientResponse, string remoteIp = null)
            where TResponse : IRecaptchaResponse
        {
            return (TResponse)await httpClient.VerifyRecaptchaAsync(typeof(TResponse), secret, clientResponse, remoteIp);
        }

        public static Task<IRecaptchaResponse> VerifyRecaptchaAsync(this IRecaptchaHttpClient httpClient,
            Type type, string secret, string clientResponse, IPAddress remoteIp)
        {
            return httpClient.VerifyRecaptchaAsync(type, secret, clientResponse, remoteIp.ToString());
        }

        public static async Task<TResponse> VerifyRecaptchaAsync<TResponse>(this IRecaptchaHttpClient httpClient,
            string secret, string clientResponse, IPAddress remoteIp)
            where TResponse : IRecaptchaResponse
        {
            return (TResponse)await httpClient.VerifyRecaptchaAsync(typeof(TResponse), secret, clientResponse, remoteIp.ToString());
        }

        public static Task<IRecaptchaResponse> VerifyRecaptchaAsync(this IRecaptchaHttpClient httpClient,
            RecaptchaConfiguration configuration, string clientResponse, IPAddress remoteIp)
        {
            return httpClient.VerifyRecaptchaAsync(configuration.SecretType.GetResponseType(), configuration.Secret,
                clientResponse, remoteIp.ToString());
        }

        public static Task<IRecaptchaResponse> VerifyRecaptchaAsync(this IRecaptchaHttpClient httpClient,
            RecaptchaConfiguration configuration, HttpRequest request, IRecaptchaTokenProvider tokenProvider)
        {
            return httpClient.VerifyRecaptchaAsync(configuration.SecretType.GetResponseType(), configuration.Secret,
                tokenProvider.GetToken(request), request.HttpContext.Connection.RemoteIpAddress.ToString());
        }
    }
}
