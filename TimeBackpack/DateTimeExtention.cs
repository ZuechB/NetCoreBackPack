using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace NetCoreBackPack.TimeBackpack
{
    public enum ISODayOfWeek
    {
        Monday = 1,
        Tuesday = 2,
        Wednesday = 3,
        Thursday = 4,
        Friday = 5,
        Saturday = 6,
        Sunday = 7
    }
    public static class DateTimeExtensions
    {
        //From http://stackoverflow.com/questions/662379/calculate-date-from-week-number/9064954#9064954
        public static DateTime FirstDateOfWeekISO8601(int year, int weekOfYear)
        {
            DateTime jan1 = new DateTime(year, 1, 1);
            int daysOffset = DayOfWeek.Thursday - jan1.DayOfWeek;

            DateTime firstThursday = jan1.AddDays(daysOffset);
            var cal = CultureInfo.CurrentCulture.Calendar;
            int firstWeek = cal.GetWeekOfYear(firstThursday, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);

            var weekNum = weekOfYear;
            if (firstWeek <= 1)
            {
                weekNum -= 1;
            }
            var result = firstThursday.AddDays(weekNum * 7);
            return result.AddDays(-3);
        }

        public static DateTime GetFirstDayOfWeek(this DateTime dt, DayOfWeek startOfWeek = DayOfWeek.Monday)
        {
            int diff = dt.DayOfWeek - startOfWeek;
            if (diff < 0)
            {
                diff += 7;
            }
            return dt.AddDays(-1 * diff).Date;
        }

        public static DateTime GetFirstDayOfMonth(this DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month, 1);
        }

        public static DateTime GetLastDayOfMonth(this DateTime dt)
        {
            if (dt.Month < 12)
            {
                return new DateTime(dt.Year, dt.Month + 1, 1).AddDays(-1);
            }
            else
            {
                return new DateTime(dt.Year, 12, 1);
            }
        }

        public static ISODayOfWeek ISODayOfWeek(this DateTime dt)
        {
            int val = dt.DayOfWeek == 0 ? 7 : (int)dt.DayOfWeek;
            return (ISODayOfWeek)val;
        }

        public static long GetJavascriptTimestamp(this DateTimeOffset input)
        {
            return GetJavascriptTimestamp(input.DateTime);
        }


        public static long GetJavascriptTimestamp(this DateTime input)
        {
            TimeSpan span = new TimeSpan(DateTime.Parse("1/1/1970").Ticks);
            DateTime time = input.Subtract(span);
            return time.Ticks / 10000;
        }

        public static IEnumerable<DateTime> EachDay(DateTimeOffset from, DateTimeOffset thru)
        {
            for (var day = from.Date; day.Date <= thru.Date; day = day.AddDays(1))
                yield return day;
        }

        public static DateTimeOffset ToLocalTime(this DateTimeOffset input, string timeZone)
        {
            return TimeZoneInfo.ConvertTimeBySystemTimeZoneId(input, timeZone);
        }

        public static string ToLocalString(this DateTimeOffset input, string timeZone = null)
        {
            var datetime = input.ToLocalTime(timeZone).DateTime;
            return datetime.ToString();
        }

        public static int TotalMonths(this DateTime start, DateTime end)
        {
            return (start.Year * 12 + start.Month) - (end.Year * 12 + end.Month);
        }

        public static int TotalMonths(this DateTimeOffset start, DateTimeOffset end)
        {
            return (start.Year * 12 + start.Month) - (end.Year * 12 + end.Month);
        }
    }
}
