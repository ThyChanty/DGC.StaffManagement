
using DGC.StaffManagement.Shared.Commons;
using DGC.StaffManagement.Shared.Exceptions;
using ApplicationException = DGC.StaffManagement.Shared.Exceptions.ApplicationException;

namespace DGC.StaffManagement.WebApi.Middlewares;

internal sealed class ExceptionHandlingMiddleware : IMiddleware
{
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(
        ILogger<ExceptionHandlingMiddleware> logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);

            await HandleExceptionAsync(context, e);
        }
    }

    private async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
    {
        var statusCode = GetStatusCode(exception);

        var response = await Result.FailAsync(GetTitle(exception));
        response.StatusCode = statusCode;

        if (exception is ValidationException validationException)
        {
            var requestPath = httpContext.Request.Path;

            response.Errors = validationException.Errors;
        }
        httpContext.Response.ContentType = "application/json";
        httpContext.Response.StatusCode = statusCode;
        await httpContext.Response.WriteAsync(response.ToString()!);
    }

    private static int GetStatusCode(Exception exception) =>
        exception switch
        {
            BadRequestException => StatusCodes.Status400BadRequest,
            NotFoundException => StatusCodes.Status404NotFound,
            ValidationException => StatusCodes.Status422UnprocessableEntity,
            _ => StatusCodes.Status500InternalServerError
        };

    private static string GetTitle(Exception exception) =>
        exception switch
        {
            ApplicationException applicationException => applicationException.Title,
            _ => "Server Error"
        };
}