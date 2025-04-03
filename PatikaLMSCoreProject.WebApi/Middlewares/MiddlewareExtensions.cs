namespace PatikaLMSCoreProject.WebApi.Middlewares
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseMaintenenceMode(this IApplicationBuilder app)
        {
            return app.UseMiddleware<MaintenanceMiddleware>();
        }

        public static IApplicationBuilder UseExceptionHandling(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ExceptionHandlingMiddleware>();
        }
    }
}