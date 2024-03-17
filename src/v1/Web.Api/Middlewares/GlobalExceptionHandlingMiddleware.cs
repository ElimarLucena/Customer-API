using System.Net;
using System.Text.Json;
using Web.Api.Middlewares.Models.Response;

namespace Web.Api.Middlewares;

public class GlobalExceptionHandlingMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch(Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        string response = JsonSerializer.Serialize(new ExceptionResponse 
        {
            StatusCode = (int)HttpStatusCode.InternalServerError,
            Message = exception.Message,
            StackTrace = exception.StackTrace
        });

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        await context.Response.WriteAsync(response);
    }
}
