using System.Runtime.Serialization;

namespace DGC.StaffManagement.Shared.Commons;

/// <summary>
/// enum casting
/// </summary>
public static class EnumExtension
{
    /// <summary>
    /// https://stackoverflow.com/questions/10418651/using-enummemberattribute-and-doing-automatic-string-conversions
    /// </summary>
    /// <param name="type"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static string ToEnumString<T>(this T type) where T: Enum
    {
        var enumType = typeof (T);
        var name = Enum.GetName(enumType, type);
        var enumMemberAttribute = ((EnumMemberAttribute[])enumType.GetField(name).GetCustomAttributes(typeof(EnumMemberAttribute), true)).Single();
        return enumMemberAttribute.Value!;
    }

    /// <summary>
    /// https://stackoverflow.com/questions/10418651/using-enummemberattribute-and-doing-automatic-string-conversions
    /// </summary>
    /// <param name="str"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static T? ToEnum<T>(this string str) where T: Enum
    {
        if(string.IsNullOrEmpty(str)){ return default; }
        var enumType = typeof(T);
        foreach (var name in Enum.GetNames(enumType))
        {
            var enumMemberAttribute = ((EnumMemberAttribute[])enumType.GetField(name)!.GetCustomAttributes(typeof(EnumMemberAttribute), true)).Single();
            if (string.Equals(enumMemberAttribute.Value!.Trim(), str.Trim(), StringComparison.CurrentCultureIgnoreCase)) return (T)Enum.Parse(enumType, name);
        }
        //throw exception or whatever handling you want or
        return default;
    }
}