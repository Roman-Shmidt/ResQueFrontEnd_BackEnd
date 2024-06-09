using System.Runtime.Serialization;

namespace Infrastructure.FunctionalStyleResult;

/// <summary> 
/// Contains Result of method execution. It allows us to reason about the code without looking 
/// into the implementation details. More info: https://enterprisecraftsmanship.com/2015/03/20/functional-c-handling-failures-input-errors/ 
/// </summary> 
[DataContract]
public class Result
{
    /// <summary> 
    /// Initializes a new instance of the <see cref="Result"/> class. 
    /// </summary> 
    /// <param name="success">if set to <c>true</c> [success].</param> 
    /// <param name="errorMessage">The error message.</param> 
    /// <param name="errorKey">The error key.</param> 
    protected Result(bool success,
        string errorMessage,
        string errorKey = "")
    {
        Success = success;
        ErrorMessage = errorMessage;
        ErrorKey = errorKey;
    }

    /// <summary> 
    /// Gets a value indicating whether this <see cref="Result"/> is success. 
    /// </summary> 
    /// <value><c>true</c> if success; otherwise, <c>false</c>.</value> 
    [DataMember]
    public bool Success { get; private set; }

    /// <summary> 
    /// The error message. 
    /// </summary> 
    [DataMember]
    public string ErrorMessage { get; private set; }

    /// <summary> 
    /// The error key. 
    /// </summary> 
    [DataMember]
    public string ErrorKey { get; private set; }

    /// <summary> 
    /// Gets a value indicating whether this <see cref="Result"/> is failure. 
    /// </summary> 
    /// <value><c>true</c> if failure; otherwise, <c>false</c>.</value> 
    public bool Failure => !Success;

    /// <summary> 
    /// Creates Failed Result. 
    /// </summary> 
    /// <param name="errorMessage">The error message.</param> 
    /// <param name="errorKey">The error key.</param> 
    /// <returns>Failed Result.</returns> 
    public static Result Fail(string errorMessage,
        string errorKey = "")
    {
        return new Result(false,
            errorMessage,
            errorKey);
    }

    /// <summary> 
    /// Creates Failed Result. 
    /// </summary> 
    /// <typeparam name="T"></typeparam> 
    /// <param name="errorMessage">The error message.</param> 
    /// <param name="errorKey">The error key.</param> 
    /// <returns>Failed Result.</returns> 
    public static Result<T> Fail<T>(string errorMessage,
        string errorKey = "")
    {
#pragma warning disable CS8604 // Possible null reference argument.
        return new Result<T>(default(T),
            false,
            errorMessage,
            errorKey);
#pragma warning restore CS8604 // Possible null reference argument.
    }

    /// <summary> 
    /// Creates Ok Result. 
    /// </summary> 
    /// <returns>Ok Result.</returns> 
    public static Result Ok()
    {
        return new Result(true, string.Empty);
    }

    /// <summary> 
    /// Creates Ok Result. 
    /// </summary> 
    /// <typeparam name="T"></typeparam> 
    /// <param name="value">The value.</param> 
    /// <returns>Ok Result.</returns> 
    public static Result<T> Ok<T>(T value)
    {
        return new Result<T>(value, true, string.Empty);
    }

    /// <summary> 
    /// Combines the Results. 
    /// </summary> 
    /// <param name="results">The Results.</param> 
    /// <returns>Combined Result.</returns> 
    public static Result Combine(params Result[] results)
    {
        const char Separator = ';';

        bool success = true;

        List<string> errorMessages = new List<string>();
        List<string> errorKeys = new List<string>();

        foreach (Result result in results)
        {
            if (result.Failure)
            {
                success = false;
                errorMessages.Add(result.ErrorMessage);
                errorKeys.Add(result.ErrorKey);
            }
        }

        return new Result(success,
            string.Join(Separator, errorMessages),
            string.Join(Separator, errorKeys));
    }
}

/// <summary> 
/// Generic version of Result. More info: https://enterprisecraftsmanship.com/2015/03/20/functional-c-handling-failures-input-errors/ 
/// </summary> 
/// <typeparam name="T"></typeparam> 
[DataContract]
public class Result<T> : Result
{
    /// <value>The value.</value> 
    [DataMember]
    public T Value { get; private set; }

    /// <summary> 
    /// Initializes a new instance of the <see cref="Result{T}"/> class. 
    /// </summary> 
    /// <param name="value">The value.</param> 
    /// <param name="success">if set to <c>true</c> [success].</param> 
    /// <param name="errorMessage">The error message.</param> 
    /// <param name="errorKey">The error key.</param> 
    protected internal Result(T value,
        bool success,
        string errorMessage,
        string errorKey = "")
        : base(success, errorMessage, errorKey)
    {
        Value = value;
    }
}