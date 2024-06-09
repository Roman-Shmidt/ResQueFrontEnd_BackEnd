using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ResQueDal.Common;

public static class StringExtensionMethods
{
    private const string CharsToEscape = "\\|{[()^$.#? ";

    public static string ToRightClaimName(this string input)
    {
        string text = Regex.Replace(input, "(?<=[a-z])([A-Z])", "_$1");
        return text.ToLower();
    }

    public static string ToAttributeName(this string input)
    {
        string text = Regex.Replace(input, "(?<=[a-z])([A-Z])", "-$1");
        return text.ToLower();
    }

    public static string ToSqlParamForLikeOperator(this string input)
    {
        return "%" + input + "%";
    }

    public static DateTimeOffset? ToDate(this string dateTimeString)
    {
        return dateTimeString.ToDateTime();
    }

    public static DateTime? ToDateTime(this string dateTimeString)
    {
        DateTimeFormatInfo dateTimeFormat = Thread.CurrentThread.CurrentCulture.DateTimeFormat;
        List<string> list = dateTimeFormat.GetAllDateTimePatterns().ToList();
        list.Add("ddd MMM dd yyyy HH:mm:ss 'GMT'K");
        list.Add("yyyy'-'MM'-'dd'T'HH':'mm zzz");
        list.Add("yyyy'-'MM'-'dd'T'HH':'mm zzz");
        list.Add("yyyy'-'MM'-'dd'T'HH':'mm");
        list.Add("yyyy'-'MM'-'dd'T'HH':'mm':'ss'K'");
        list.Add("yyyy'-'MM'-'dd");
        DateTime result;
        return DateTime.TryParseExact(dateTimeString, list.ToArray(), CultureInfo.InvariantCulture, DateTimeStyles.AllowWhiteSpaces, out result) ? new DateTime?(result) : null;
    }
}
