using PatikaLMSCoreProject.Business.Operations.Setting;
using System.Text.Json;

namespace PatikaLMSCoreProject.WebApi.Middlewares
{
    public class MaintenanceMiddleware
    {
        private readonly RequestDelegate _next;

        public MaintenanceMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var settingService = context.RequestServices.GetRequiredService<ISettingService>();
            bool maintenanceMode = settingService.GetMaintenanceState();

            if (context.Request.Path.StartsWithSegments("/api/auth/login") ||
                context.Request.Path.StartsWithSegments("/api/settings"))
            {
                await _next(context);
                return;
            }

            if (maintenanceMode)
            {
                context.Response.StatusCode = StatusCodes.Status503ServiceUnavailable;
                context.Response.ContentType = "application/json";

                var response = new
                {
                    message = "The service is temporarily unavailable due to maintenance."
                };

                await context.Response.WriteAsync(JsonSerializer.Serialize(response));
                return;
            }
            else
            {
                await _next(context);
            }
        }
    }
}