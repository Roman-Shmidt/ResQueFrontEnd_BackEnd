namespace Infrastructure.Exceptions
{
    /// <summary>
    /// Occurs when there is an error during Command execution.
    /// </summary>
    /// <seealso cref="BadRequestException" />
    public sealed class CommandExecutionFailedException : BadRequestException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CommandExecutionFailedException"/> class.
        /// </summary>
        public CommandExecutionFailedException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandExecutionFailedException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public CommandExecutionFailedException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandExecutionFailedException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="errorKey">The error key.</param>
        public CommandExecutionFailedException(string message, string errorKey)
            : base(message, errorKey)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandExecutionFailedException"/> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception,
        /// or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
        public CommandExecutionFailedException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandExecutionFailedException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="errorKey">The error key.</param>
        /// <param name="innerException">The exception that is the cause of the current exception,
        /// or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
        public CommandExecutionFailedException(string message, string errorKey, Exception innerException)
            : base(message, errorKey, innerException)
        {
        }
    }
}
