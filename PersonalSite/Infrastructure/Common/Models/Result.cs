
namespace PersonalSite.Infrastructure.Common.Models;

public class Result
{
    private readonly bool _isSuccess;
    public readonly string? ErrorMessage;
    
    public bool IsSuccess => _isSuccess;
    private Result(bool isSuccess, string? errorMessage)
    {
        _isSuccess = isSuccess;
        ErrorMessage = errorMessage;
    }
    public static Result Success()
    {
        return new Result(true, null);
    }

    public static Result Fail(string error)
    {
        return new Result(false, error);
    }
}
public class Result<T>
{
    private readonly bool _isSuccess;
    public readonly string? ErrorMessage;
    public readonly T Value;

    public bool IsSuccess => _isSuccess && Value != null;

    private Result(bool isSuccess, string? errorMessage, T value)
    {
        _isSuccess = isSuccess;
        ErrorMessage = errorMessage;
        Value = value;
    }

    public static Result<T> Success(T value)
    {
        return new Result<T>(true, null, value);
    }

    public static Result<T> Fail(string error)
    {
        return new Result<T>(false, error, default);
    }

    public static Result<T> FromFail<M>(Result<M> parentResult)
    {
        if (!parentResult.IsSuccess)
            return new Result<T>(false, parentResult.ErrorMessage, default);
        throw new InvalidOperationException("Could not convert Result<T> object");
    }
}