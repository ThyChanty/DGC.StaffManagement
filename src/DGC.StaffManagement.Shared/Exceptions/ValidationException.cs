

using DGC.StaffManagement.Shared.Commons;

namespace DGC.StaffManagement.Shared.Exceptions;

public sealed class ValidationException : ApplicationException
{
    public ValidationException(List<Error> errors) : base("Validation Failure",
        "One or more validation errors occurred")
        => Errors = errors;
    public List<Error>? Errors { get; } 
}
