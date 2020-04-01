using Microsoft.AspNetCore.Http;
using NetCoreBackPack.Models.ErrorTrackingMiddleware;
using Newtonsoft.Json;
using Sentry;
using System;
using System.Net;
using System.Threading.Tasks;

namespace NetCoreBackPack.Policy
{
    public class ErrorTrackingMiddleware
    {
        private readonly RequestDelegate next;
        public ErrorTrackingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context /* other dependencies */)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var code = HttpStatusCode.InternalServerError; // 500 if unexpected

            if (ex is NotFoundException)
            {
                code = HttpStatusCode.NotFound;
            }
            else if (ex is UnauthorizedException)
            {
                code = HttpStatusCode.Unauthorized;
            }
            else if (ex is BadRequestException)
            {
                code = HttpStatusCode.BadRequest;
            }
            else if (ex is ForbiddenException) // BE CAREFUL WHEN YOU USE THIS... READ "ForbiddenException" for more details...
            {
                code = HttpStatusCode.Forbidden;

                if (ex.Message == "User is not logged in")
                {
                    var result2 = JsonConvert.SerializeObject(new
                    {
                        error = ex.Message
                    });

                    context.Response.ContentType = "application/json";
                    context.Response.StatusCode = (int)code;
                    return context.Response.WriteAsync(result2);
                }
            }
            else
            {
                //telemetryClient.TrackException(ex);
                SentrySdk.CaptureException(ex);
            }

            var result = JsonConvert.SerializeObject(new
            {
                error = ex.Message
            });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(result);
        }
    }
}
