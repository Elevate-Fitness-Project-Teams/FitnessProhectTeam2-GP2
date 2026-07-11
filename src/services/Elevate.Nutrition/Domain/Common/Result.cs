namespace Elevate.Nutrition.Domain.Common;

public enum ErrorType
{
    Failure,
    NotFound
}

public class Result
{
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public string? Error { get; }
    public ErrorType ErrorType { get; }

    protected Result(bool isSuccess, string? error, ErrorType errorType = ErrorType.Failure)
    {
        IsSuccess = isSuccess;
        Error = error;
        ErrorType = errorType;
    }

    public virtual object? GetValue() => null;

    public static Result Success() => new(true, null);
    public static Result<T> Success<T>(T value) => new(value, true, null);
    public static Result Failure(string error, ErrorType errorType = ErrorType.Failure) => new(false, error, errorType);
    public static Result<T> Failure<T>(string error, ErrorType errorType = ErrorType.Failure) => new(default, false, error, errorType);
}

public class Result<T> : Result
{
    public T? Value { get; }

    public Result(T? value, bool isSuccess, string? error, ErrorType errorType = ErrorType.Failure) : base(isSuccess, error, errorType)
    {
        Value = value;
    }

    public override object? GetValue() => Value;
}
