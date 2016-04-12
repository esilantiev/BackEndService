using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Ises.Core.Common
{
    public static class Extensions
    {
        public static T ToEnum<T>(this string value)
        {
            if (!typeof(T).IsEnum)
            {
                throw new NotSupportedException("T must be an Enum");
            }
            return (T)Enum.Parse(typeof(T), value, true);
        }

        public static T ToEnum<T>(this GroupCollection docsGroups)
        {
            return (from item in Enum.GetNames(typeof(T))
                    where docsGroups[item].Success
                    select ToEnum<T>(item)).FirstOrDefault();
        }

        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            var seenKeys = new HashSet<TKey>();
            return source.Where(element => seenKeys.Add(keySelector(element)));
        }

        public static bool In<T>(this T source, params T[] list)
        {
            return (list as IList<T>).Contains(source);
        }

        public static bool IsNullOrEmpty<T>(this IEnumerable<T> data)
        {
            return data == null || !data.Any();
        }

        public static DateTime TrimSeconds(this DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, 0);
        }

        public static DateTime TrimMilliSeconds(this DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, dateTime.Second, 0);
        }

        public static DateTime ToEndOfDay(this DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 23, 59, 59);
        }

        public static DateTime UtcToTimeZone(this DateTime dateTime, TimeZoneInfo tzi)
        {
            return TimeZoneInfo.ConvertTime(dateTime, TimeZoneInfo.Utc, tzi);
        }

        public static DateTime TimeZoneToUtc(this DateTime dateTime, TimeZoneInfo tzi)
        {
            return TimeZoneInfo.ConvertTime(dateTime, tzi, TimeZoneInfo.Utc);
        }

        public static TimeSpan UtcToTimeSpan(this string dateTime)
        {
            DateTime time;
            if (DateTime.TryParse(dateTime, out time))
            {
                return time.TimeOfDay;
            }
            return new TimeSpan(0);
        }

        public static bool EqualsIgnoreCase(this string s1, string s2)
        {
            return String.Equals(s1, s2, StringComparison.InvariantCultureIgnoreCase);
        }

        public static bool IsNullOrWhiteSpace(this string s1)
        {
            return String.IsNullOrWhiteSpace(s1);
        }

        public static bool IsNotNullOrWhiteSpace(this string s1)
        {
            return !String.IsNullOrWhiteSpace(s1);
        }

        public static string SafeTrim(this string value)
        {
            return value.IsNullOrWhiteSpace() ? String.Empty : value.Trim();
        }

        public static string DefaultIfEmpty(this string value, string defaultValue)
        {
            return value.IsNullOrWhiteSpace() ? defaultValue : value;
        }

        public static DateTime? DefaultIfEmpty(this DateTime? value, DateTime? defaultValue)
        {
            return value ?? defaultValue;
        }

        public static string ToCommaSeparated(this string value)
        {
            var commaSeparated = value.Aggregate(String.Empty, (current, t) => current + String.Format("{0},", t));
            commaSeparated.Remove(commaSeparated.LastIndexOf(','));
            return commaSeparated;
        }

        public static bool Contains(this string source, string subString, StringComparison comparision)
        {
            return source.IndexOf(subString, comparision) >= 0;
        }

        public static string GetExceptionMessages(this Exception e, string msgs = "")
        {
            if (e == null) return String.Empty;
            if (msgs == "") msgs = e.Message;
            if (e.InnerException != null)
                msgs += "\r\nInnerException: " + GetExceptionMessages(e.InnerException);
            return msgs;
        }

    }
}
