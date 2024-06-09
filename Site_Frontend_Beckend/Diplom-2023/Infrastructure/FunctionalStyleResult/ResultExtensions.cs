namespace Infrastructure.FunctionalStyleResult;

/// <summary> 
/// Extension methods for Result. 
/// More info: https://enterprisecraftsmanship.com/2015/03/20/functional-c-handling-failures-input-errors/ 
/// </summary> 
public static class ResultExtensions
{
    public static readonly string ConvertFailureExceptionOnSuccess =
        $"{nameof(ConvertFailure)} failed because the Result is in a success state.";

    /// <summary> 
    /// Called when [success]. 
    /// </summary> 
    /// <param name="result">The result.</param> 
    /// <param name="func">The function.</param> 
    public static Result OnSuccess(this Result result, Func<Result> func)
    {
        if (result.Failure)
            return result;

        return func();
    }

    /// <summary> 
    /// Called when [success]. 
    /// </summary> 
    /// <param name="result">The result.</param> 
    /// <param name="action">The action.</param> 
    public static Result OnSuccess(this Result result, Action action)
    {
        if (result.Failure)
            return result;

        action();

        return Result.Ok();
    }

    /// <summary> 
    /// Called when [success]. 
    /// </summary> 
    /// <typeparam name="T"></typeparam> 
    /// <param name="result">The result.</param> 
    /// <param name="action">The action.</param> 
    public static Result OnSuccess<T>(this Result<T> result, Action<T> action)
    {
        if (result.Failure)
            return result;

        action(result.Value);

        return Result.Ok();
    }

    /// <summary> 
    /// Called when [success]. 
    /// </summary> 
    /// <typeparam name="T"></typeparam> 
    /// <param name="result">The result.</param> 
    /// <param name="func">The function.</param> 
    public static Result<T> OnSuccess<T>(this Result result, Func<T> func)
    {
        if (result.Failure)
            return Result.Fail<T>(result.ErrorMessage);

        return Result.Ok(func());
    }

    /// <summary> 
    /// Called when [success]. 
    /// </summary> 
    /// <typeparam name="T"></typeparam> 
    /// <param name="result">The result.</param> 
    /// <param name="func">The function.</param> 
    public static Result<T> OnSuccess<T>(this Result result, Func<Result<T>> func)
    {
        if (result.Failure)
            return Result.Fail<T>(result.ErrorMessage);

        return func();
    }

    /// <summary> 
    /// Called when [success]. 
    /// </summary> 
    /// <typeparam name="T"></typeparam> 
    /// <param name="result">The result.</param> 
    /// <param name="func">The function.</param> 
    public static Result OnSuccess<T>(this Result<T> result, Func<T, Result> func)
    {
        if (result.Failure)
            return result;

        return func(result.Value);
    }

    /// <summary> 
    /// Called when [failure]. 
    /// </summary> 
    /// <param name="result">The result.</param> 
    /// <param name="action">The action.</param> 
    public static Result OnFailure(this Result result, Action action)
    {
        if (result.Failure)
        {
            action();
        }

        return result;
    }

    /// <summary> 
    /// Called when [both]. 
    /// </summary> 
    /// <param name="result">The result.</param> 
    /// <param name="action">The action.</param> 
    public static Result OnBoth(this Result result,

Action<Result> action) 
    {
action(result);

return result;
}

/// <summary> 
/// Called when [both]. 
/// </summary> 
/// <typeparam name="T"></typeparam> 
/// <param name="result">The result.</param> 
/// <param name="func">The function.</param> 
public static T OnBoth<T>(this Result result, Func<Result, T> func)
{
return func(result);
}

public static Result<T> ConvertFailure<T>(this Result result)
{
if (result.Success)
    throw new InvalidOperationException(ConvertFailureExceptionOnSuccess);

return Result.Fail<T>(result.ErrorMessage, result.ErrorKey);
}

public static Result<T2> ConvertFailure<T1, T2>(this Result<T1> result)
{
if (result.Success)
    throw new InvalidOperationException(ConvertFailureExceptionOnSuccess);

return Result.Fail<T2>(result.ErrorMessage, result.ErrorKey);
} 
} 