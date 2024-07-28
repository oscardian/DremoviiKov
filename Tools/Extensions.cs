using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Tools;

public static class StringExtensions
{
    public static Boolean IsNullOrWhiteSpace(this String? str)
    {
        return String.IsNullOrWhiteSpace(str);
    }

    public static Boolean IsEmail(this String str)
    {
        String pattern = @"^[\w\.-]+@[\w\.-]+\.\w+$";
        Regex regex = new Regex(pattern);

        return regex.IsMatch(str);
    }

    public static String Hashing(this String srt)
    {
        using SHA256 hash = SHA256.Create();
        return Convert.ToHexString(hash.ComputeHash(Encoding.ASCII.GetBytes(srt)));
    }
}


