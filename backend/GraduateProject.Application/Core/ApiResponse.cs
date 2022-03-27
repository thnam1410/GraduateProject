using System.Text.Json.Serialization;

namespace GraduateProject.Extensions;

public class ApiResponse<TResult> : ApiResponse where TResult : class
{
    public TResult Result { get; }

    private ApiResponse(
        bool success,
        string message,
        TResult result,
        object error = null,
        int statusCode = 200)
        : base(success, message, error, statusCode)
    {
        this.Result = result;
    }

    public static ApiResponse<TResult> Ok(TResult result = null, string message = null) => new ApiResponse<TResult>(true, message, result);

    public static ApiResponse<TResult> Fail(
        string message,
        object errors = null,
        int statusCode = 500,
        TResult result = null)
    {
        return new ApiResponse<TResult>(false, message, result, errors, statusCode);
    }
}

public class ApiResponse
{
    public bool Success { get; }

    public string Message { get; }

    public object Error { get; }

    [JsonIgnore] public int StatusCode { get; }

    protected ApiResponse(bool success, string message, object error = null, int statusCode = 200)
    {
        this.Message = message;
        this.Error = error;
        this.StatusCode = statusCode;
        this.Success = success;
    }

    public static ApiResponse Ok(string message = null) => new ApiResponse(true, message);

    public static ApiResponse Fail(string message, object errors = null, int statusCode = 500) => new ApiResponse(false, message, errors);
}