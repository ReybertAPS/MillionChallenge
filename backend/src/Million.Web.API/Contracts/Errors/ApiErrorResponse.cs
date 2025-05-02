namespace Million.Web.API.Contracts.Errors;

public class ApiErrorResponse
{
    public int StatusCode { get; set; }
    public string Message { get; set; } = null!;
    public IEnumerable<string>? Errors { get; set; }

    public ApiErrorResponse(int statusCode, string message)
    {
        StatusCode = statusCode;
        Message = message;
    }

    public ApiErrorResponse(int statusCode, string message, IEnumerable<string> errors)
        : this(statusCode, message)
    {
        Errors = errors;
    }
}

