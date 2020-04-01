using System;
using System.Collections.Generic;
using System.Globalization;

namespace NetCoreBackPack
{
    public static class SystemTime
    {

#if DEBUG
        private static DateTimeOffset? dateTimeForTesting;


        public static void SetTimeForTesting(DateTimeOffset dateTime)
        {
            dateTimeForTesting = dateTime;
        }
#endif
        public static DateTimeOffset Now
        {
            get
            {
#if DEBUG
                if (dateTimeForTesting.HasValue)
                    return dateTimeForTesting.Value;
#endif
                return DateTimeOffset.UtcNow;
            }
        }

        public static DateTimeOffset InTimeZone(this DateTime input, string timezone)
        {
            input = DateTime.SpecifyKind(input, DateTimeKind.Unspecified);
            TimeZoneInfo tzi = TimeZoneInfo.FindSystemTimeZoneById(timezone);
            TimeSpan offset = tzi.GetUtcOffset(input);
            return new DateTimeOffset(input, offset);
        }

        public static DateTime StartOfDay(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, 0, 0, 0, 0);
        }

        public static DateTime EndOfDay(this DateTime input)
        {
            return input.Date.AddDays(1).AddTicks(-1);
        }

        public static DateTime LastMonth(this DateTime input)
        {
            return input.Date.AddMonths(-1);
        }

        public static DateTimeOffset StartOfMonth(this DateTimeOffset date)
        {
            return new DateTimeOffset(new DateTime(date.Year, date.Month, 1)).StartOfDay();
        }

        public static DateTimeOffset EndOfMonth(this DateTimeOffset input)
        {
            return input.StartOfMonth().AddMonths(1).AddDays(-1).EndOfDay();
        }

        public static DateTimeOffset AddWeeks(this DateTimeOffset date, int weeks)
        {
            return date.AddDays(weeks * 7);
        }

        public static DateTime AddWeeks(this DateTime date, int weeks)
        {
            return date.AddDays(weeks * 7);
        }

        public static DateTimeOffset StartOfDay(this DateTimeOffset input)
        {
            return new DateTimeOffset(input.Date, input.Offset);
        }

        public static DateTimeOffset EndOfDay(this DateTimeOffset input)
        {
            return new DateTimeOffset(input.Date.AddDays(1).AddTicks(-1), input.Offset);
        }

        public static DateTime UtcToLocal(this DateTimeOffset input, string timeZoneId = "Eastern Standard Time")
        {
            return TimeZoneInfo.ConvertTime(input, TimeZoneInfo.FindSystemTimeZoneById(timeZoneId)).DateTime;
        }

        public static string GetMonthByName(int month)
        {
            return CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month);
        }
    }
}