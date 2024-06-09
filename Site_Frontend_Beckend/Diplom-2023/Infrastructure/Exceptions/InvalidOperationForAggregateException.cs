namespace Infrastructure.Exceptions
{
    /// <summary>
    /// Occurs when invalid operation for Aggregate current state is applied.
    /// </summary>
    /// <seealso cref="BadRequestException" />
    public sealed class InvalidOperationForAggregateException : BadRequestException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidOperationForAggregateException"/> class.
        /// </summary>
        public InvalidOperationForAggregateException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidOperationForAggregateException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public InvalidOperationForAggregateException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidOperationForAggregateException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="errorKey">The error key.</param>
        public InvalidOperationForAggregateException(string message, string errorKey)
            : base(message, errorKey)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidOperationForAggregateException"/> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception,
        /// or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
        public InvalidOperationForAggregateException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidOperationForAggregateException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="errorKey">The error key.</param>
        /// <param name="innerException">The exception that is the cause of the current exception,
        /// or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
        public InvalidOperationForAggregateException(string message, string errorKey, Exception innerException)
            : base(message, errorKey, innerException)
        {
        }
    }
}
