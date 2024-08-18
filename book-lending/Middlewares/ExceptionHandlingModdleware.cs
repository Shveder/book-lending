using System.Net;
using System.Security.Authentication;
using book_lending.DTO;
using book_lending.Exceptions;

namespace book_lending.Middlewares;

public class ExceptionHandlingMiddleware
{
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;
    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(
        RequestDelegate next,
        ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (IncorrectDataException ex)
        {
            await HandleExceptionAsync(httpContext,
                ex.Message,
                HttpStatusCode.UnprocessableEntity,
                ex.Message);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(httpContext,
                ex.Message,
                HttpStatusCode.InternalServerError,
                "Internal server error");
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, string exMsg, HttpStatusCode httpStatusCode,
        string message)
    {
        _logger.LogError(exMsg);

        var response = context.Response;

        response.ContentType = "application/json";
        response.StatusCode = (int)httpStatusCode;

        ErrorDto errorDto = new()
        {
            Message = message,
            StatusCode = (int)httpStatusCode
        };

        await response.WriteAsJsonAsync(errorDto);
    }
}