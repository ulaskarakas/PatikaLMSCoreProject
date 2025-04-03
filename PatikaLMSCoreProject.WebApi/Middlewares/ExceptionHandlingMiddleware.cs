using System.Net;
using System.Text.Json;

namespace PatikaLMSCoreProject.WebApi.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                await context.Response.WriteAsync(JsonSerializer.Serialize(new
                {
                    message = "An unexpected error occurred.",
                    detail = ex.Message
                }));
            }
        }
    }
}