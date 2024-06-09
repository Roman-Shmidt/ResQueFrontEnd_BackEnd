﻿namespace Infrastructure.Exceptions
{
    /// <summary>
    /// Occurs when users attempt to change the same data at the same time.
    /// </summary>
    /// <seealso cref="BaseErrorKeyException" />
    public sealed class ConcurrencyException : BaseErrorKeyException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConcurrencyException"/> class.
        /// </summary>
        public ConcurrencyException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConcurrencyException" /> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public ConcurrencyException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConcurrencyException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="errorKey">The error key.</param>
        public ConcurrencyException(string message, string errorKey)
            : base(message, errorKey)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConcurrencyException"/> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception,
        /// or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
        public ConcurrencyException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConcurrencyException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="errorKey">The error key.</param>
        /// <param name="innerException">The exception that is the cause of the current exception,
        /// or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
        public ConcurrencyException(string message, string errorKey, Exception innerException)
            : base(message, errorKey, innerException)
        {
        }
    }
}
