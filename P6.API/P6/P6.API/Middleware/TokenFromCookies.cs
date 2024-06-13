using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
namespace P6.API.MiddleWare
{
    public class TokenFromCookies
    {
        private readonly RequestDelegate _next;

        public TokenFromCookies(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Cookies.ContainsKey("Access-Token"))
            {
                var token = context.Request.Cookies["Access-Token"];
                if (!string.IsNullOrEmpty(token))
                {
                    context.Request.Headers.Add("Authorization", $"Bearer {token}");
                }
            }

            await _next(context);
        }
    }

    //public static class RequestCultureMiddlewareExtensions
    //{
    //    public static IApplicationBuilder UseRequestCulture(
    //        this IApplicationBuilder builder)
    //    {
    //        return builder.UseMiddleware<TokenFromCookies>();
    //    }
    //}
}
