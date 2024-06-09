using System;

namespace Infrastructure.Exceptions;

public sealed class WriteOperationInReadOnlyDbContextException : Exception
{
    public WriteOperationInReadOnlyDbContextException()
        : base("Current DbContext is read-only and should not be used for write operations")
    {
    }
}