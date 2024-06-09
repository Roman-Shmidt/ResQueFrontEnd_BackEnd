namespace Infrastructure.Exceptions
{
    /// <summary>
    /// Base type for exception with ErrorKey.
    /// </summary>
    /// <seealso cref="System.Exception" />
    public abstract class BaseErrorKeyException:Exception
    {
        /// <summary>
        /// Gets the error key.
        /// </summary>
        /// <value>The error key.</value>
        public string ErrorKey { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseErrorKeyException"/> class.
        /// </summary>
        protected BaseErrorKeyException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseErrorKeyException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        protected BaseErrorKeyException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseErrorKeyException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="errorKey">The error key.</param>
        protected BaseErrorKeyException(string message, string errorKey)
            : base(message)
        {
            ErrorKey = errorKey;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseErrorKeyException"/> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception,
        /// or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
        protected BaseErrorKeyException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BadRequestException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="errorKey">The error key.</param>
        /// <param name="innerException">The exception that is the cause of the current exception,
        /// or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
        protected BaseErrorKeyException(string message, string errorKey, Exception innerException)
            : base(message, innerException)
        {
            ErrorKey = errorKey;
        }
    }
}
