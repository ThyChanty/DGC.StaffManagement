namespace DGC.StaffManagement.Shared.Exceptions;

public class NotFoundException : ApplicationException
{
    public NotFoundException(string message) : base( string.IsNullOrEmpty(message) ? "Not Found" : message, message)
    {
    }
}