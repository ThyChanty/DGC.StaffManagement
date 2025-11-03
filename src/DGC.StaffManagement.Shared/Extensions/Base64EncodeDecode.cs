using System.Text;

namespace DGC.StaffManagement.Shared.Extensions;

public static class Base64EncodeDecode
{
    public static string EncodeBase64(this string value)
    {
        var valueBytes = Encoding.UTF8.GetBytes(value);
        return Convert.ToBase64String(valueBytes);
    }

    public static string DecodeBase64(this string value)
    {
        var valueBytes = System.Convert.FromBase64String(value);
        return Encoding.UTF8.GetString(valueBytes);
    }

    public static byte[] GetBytesFromBase64(this string value)
    {
        return Convert.FromBase64String(value);
    }

    public static bool IsBase64(this string base64String)
    {
        if (string.IsNullOrEmpty(base64String) || base64String.Length % 4 != 0
           || base64String.Contains(" ") || base64String.Contains("\t") || base64String.Contains("\r") || base64String.Contains("\n"))
            return false;

        try
        {
            Convert.FromBase64String(base64String);
            return true;
        }
        catch (Exception ex)
        {
            // Handle the exception
        }
        return false;
    }
}