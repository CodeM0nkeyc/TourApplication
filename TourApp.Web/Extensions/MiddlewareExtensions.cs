namespace TourApp.Web.Extensions;

public static class MiddlewareExtensions
{
    public static WebApplication CatchOperationCancellations(this WebApplication app)
    {
        app.UseMiddleware<OperationCancelledHandlerMiddleware>();
        
        return app;
    }
}