namespace TourApp.Web.Extensions;

public static class MiddlewareExtensions
{
    public static IApplicationBuilder CatchOperationCancellations(this IApplicationBuilder app)
    {
        return app.UseMiddleware<OperationCancelledHandlerMiddleware>();
    }
}