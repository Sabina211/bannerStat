using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using NoruBanner.Infrastructure.Exceptions;

namespace NoruBanner.Infrastructure.Middlewares
{
    public class CustomExceptionFilter
    {
        private readonly RequestDelegate next;

        public CustomExceptionFilter(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
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

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var response = context.Response;
            response.ContentType = "application/json";
            HttpStatusCode status;
            var stackTrace = string.Empty;

            switch (exception)
            {
                case EntityNotFoundException e:
                    status = HttpStatusCode.NotFound;
                    break;
                case NoruBannerException e:
                    status = HttpStatusCode.BadRequest;
                    break;
                default:
                    status = HttpStatusCode.InternalServerError;
                    stackTrace = exception.StackTrace;
                    break;
            }

            var result = JsonSerializer.Serialize(new
            {
                error = exception?.Message
            }, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

            response.StatusCode = (int)status;
            return response.WriteAsync(result);
        }
    }
}
