using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Spaier.Recaptcha.Responses;
using Spaier.Recaptcha.Services;

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
            return (TResponse)await httpClient.VerifyRecaptchaAsync(typeof(TResponse), secret, clientResponse,
                remoteIp.ToString());
        }

        public static Task<TResponse> VerifyRecaptchaAsync<TResponse>(this IRecaptchaHttpClient httpClient,
            RecaptchaConfiguration configuration, HttpRequest request, IRecaptchaTokenProvider tokenProvider)
            where TResponse : IRecaptchaResponse
        {
            return httpClient.VerifyRecaptchaAsync<TResponse>(configuration.Secret, tokenProvider.GetToken(request),
                request.HttpContext.Connection.RemoteIpAddress);
        }
    }
}
