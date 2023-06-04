using System.Globalization;

namespace Gymby.Application.Utils;

public static class DateHandler
{
    public static string GetNameOfDay(DateTime date)
    {
        string dayOfWeek = date.ToString("dddd", new CultureInfo("en-US"));
        return char.ToUpper(dayOfWeek[0]) + dayOfWeek.Substring(1);
    }
}
