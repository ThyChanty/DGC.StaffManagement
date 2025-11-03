namespace DGC.StaffManagement.Shared.Extensions;

public static class StringExtensions
{
    public static bool IsNullOrEmpty(this string? value)
    {
        return string.IsNullOrEmpty(value);
    }
    
    public static bool IsNullOrWhiteSpace(this string? value)
    {
        return string.IsNullOrWhiteSpace(value);
    }

    /// <summary>
    /// Check if string is Unicode
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static bool IsUnicode(this string? value)
    {
        return !string.IsNullOrEmpty(value) && value.Any(x => x > 255);
    }
}