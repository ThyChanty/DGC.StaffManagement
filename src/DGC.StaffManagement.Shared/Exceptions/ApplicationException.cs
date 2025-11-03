namespace DGC.StaffManagement.Shared.Exceptions;

public class ApplicationException : Exception
{
    public ApplicationException(string title, string message)
        : base(message) =>
        Title = title;

    public string Title { get; }
}