namespace TourApp.Web.Middlewares;

public class OperationCancelledHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<OperationCancelledHandlerMiddleware> _logger;

    public OperationCancelledHandlerMiddleware(
        RequestDelegate next, ILogger<OperationCancelledHandlerMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }
    
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (OperationCanceledException)
        {
            _logger.LogInformation($"Request with trace id {context.TraceIdentifier} " +
                                   $"with method {context.Request.Method} for {context.Request.Path} was cancelled");
        }
    }
}