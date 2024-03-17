namespace Web.Api.Middlewares.Models.Response;

public struct ExceptionResponse
{
    public int StatusCode { get; set; }
    public string Message { get; set; }
    public string? StackTrace { get; set; }
}