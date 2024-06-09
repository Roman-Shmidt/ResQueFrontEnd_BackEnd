namespace Infrastructure.Exceptions
{
    /// <summary>
    /// Exception for cases when information someone tries to read/write data into not onw Tenant.
    /// </summary>
    public sealed class TenantBoundaryViolationException : BadRequestException
    {
        private const string ErrorMessage = "Tenant boundary violation";

        /// <summary>
        /// Initializes a new instance of the <see cref="TenantBoundaryViolationException"/> class.
        /// </summary>
        public TenantBoundaryViolationException()
            : base(ErrorMessage)
        {
        }
    }
}
