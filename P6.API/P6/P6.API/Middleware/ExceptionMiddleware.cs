using Google.Apis.Auth;
using Microsoft.SqlServer.Server;
using Newtonsoft.Json;
using P6.Core.Exceptions;

namespace P6.API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            context.Response.Headers.Add("Content-Type", "application/json");
            try
            {
                await _next(context);
            }
            catch (BadRequestException ex)
            {
                BaseException serviceResult = new BaseException()
                {
                    ErrorCode = StatusCodes.Status400BadRequest,
                    UserMessage = ex.Message,
                    DevMessage = ex.Message,
                    TraceId = context.TraceIdentifier,
                    MoreInfo = ex.HelpLink,
                };
                var res = JsonConvert.SerializeObject(serviceResult);
                await context.Response.WriteAsync(res);
            }
            catch (NotFoundException ex)
            {
                BaseException serviceResult = new BaseException()
                {
                    ErrorCode = StatusCodes.Status404NotFound,
                    UserMessage = ex.Message,
                    DevMessage = ex.Message,
                    TraceId = context.TraceIdentifier,
                    MoreInfo = ex.HelpLink,
                };
                var res = JsonConvert.SerializeObject(serviceResult);
                await context.Response.WriteAsync(res);
            }
            catch (ConflictException ex)
            {
                BaseException serviceResult = new BaseException()
                {
                    ErrorCode = ex.ErrorCode,
                    UserMessage = ex.Message,
                    DevMessage = ex.Message,
                    TraceId = context.TraceIdentifier,
                    MoreInfo = ex.HelpLink,
                };
                var res = JsonConvert.SerializeObject(serviceResult);
                await context.Response.WriteAsync(res);
            }
            catch (InternalServerException ex)
            {
                BaseException serviceResult = new BaseException()
                {
                    ErrorCode = StatusCodes.Status500InternalServerError,
                    DevMessage = ex.Message,
                    UserMessage = "Internal server error.",
                };
                var res = JsonConvert.SerializeObject(serviceResult);
                await context.Response.WriteAsync(res);
            }
            catch (InvalidJwtException ex)
            {
                BaseException serviceResult = new BaseException()
                {
                    ErrorCode = StatusCodes.Status400BadRequest,
                    DevMessage = ex.Message,
                    UserMessage = "Invalid Google cridential token.",
                };
                var res = JsonConvert.SerializeObject(serviceResult);
                await context.Response.WriteAsync(res);
            }
            catch (Exception ex)
            {
                BaseException serviceResult = new BaseException()
                {
                    ErrorCode = StatusCodes.Status500InternalServerError,
                    UserMessage = "Server error",
                    DevMessage = ex.Message,
                    TraceId = context.TraceIdentifier,
                    MoreInfo = ex.HelpLink,
                };
                var res = JsonConvert.SerializeObject(serviceResult);
                await context.Response.WriteAsync(res);
            }

        }
    }
}
