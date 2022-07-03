namespace PersonalSite.Infrastructure.Common.Models;

public class Result<T>
{
    public readonly bool IsSuccess;
    public readonly string? ErrorMessage;
    public readonly T Value;
    
    private Result(bool isSuccess, string? errorMessage, T value)
    {
        IsSuccess = isSuccess;
        ErrorMessage = errorMessage;
        Value = value;
    }

    public static Result<T> Success(T value)
    {
        return new Result<T>(true, null, value);
    }

    public static Result<T?> Fail(string error)
    {
        return new Result<T?>(false, error, default);
    }
}