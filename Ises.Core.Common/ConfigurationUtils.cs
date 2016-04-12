using System;
using System.Configuration;

namespace Ises.Core.Common
{
    public class ConfigurationUtils
    {
        public static string GetAsString(string key, string defaultVal)
        {
            var value = ConfigurationManager.AppSettings[key];
            if (String.IsNullOrWhiteSpace(value)) value = defaultVal;
            return value;
        }

        public static bool GetAsBool(string key, bool defaultVal)
        {
            var strValue = ConfigurationManager.AppSettings[key];

            bool value;
            return Boolean.TryParse(strValue, out value)
                ? value
                : defaultVal;
        }

        public static bool? GetAsBool(string key, bool? defaultVal)
        {
            var strValue = ConfigurationManager.AppSettings[key];

            bool value;
            return Boolean.TryParse(strValue, out value)
                ? value
                : defaultVal;
        }

        public static int GetAsInt(string key, int defaultVal)
        {
            var strValue = ConfigurationManager.AppSettings[key];

            int value;
            return Int32.TryParse(strValue, out value)
                ? value
                : defaultVal;
        }

        public static int? GetAsInt(string key, int? defaultVal)
        {
            var strValue = ConfigurationManager.AppSettings[key];

            int value;
            return Int32.TryParse(strValue, out value)
                ? value
                : defaultVal;
        }

        public static long GetAsLong(string key, long defaultVal)
        {
            var strValue = ConfigurationManager.AppSettings[key];

            long value;
            return Int64.TryParse(strValue, out value)
                ? value
                : defaultVal;
        }

        public static long? GetAsLong(string key, long? defaultVal)
        {
            var strValue = ConfigurationManager.AppSettings[key];

            long value;
            return Int64.TryParse(strValue, out value)
                ? value
                : defaultVal;
        }

        public static decimal GetAsDecimal(string key, decimal defaultVal)
        {
            var strValue = ConfigurationManager.AppSettings[key];

            decimal value;
            return Decimal.TryParse(strValue, out value)
                ? value
                : defaultVal;
        }

        public static decimal? GetAsDecimal(string key, decimal? defaultVal)
        {
            var strValue = ConfigurationManager.AppSettings[key];

            decimal value;
            return Decimal.TryParse(strValue, out value)
                ? value
                : defaultVal;
        }

        public static DateTime GetAsDateTime(string key, DateTime defaultVal)
        {
            var strValue = ConfigurationManager.AppSettings[key];

            DateTime value;
            return DateTime.TryParse(strValue, out value)
                ? value
                : defaultVal;
        }

        public static DateTime? GetAsDateTime(string key, DateTime? defaultVal)
        {
            var strValue = ConfigurationManager.AppSettings[key];

            DateTime value;
            return DateTime.TryParse(strValue, out value)
                ? value
                : defaultVal;
        }

        public static TimeSpan GetAsTimeSpan(string key, TimeSpan defaultVal)
        {
            var strValue = ConfigurationManager.AppSettings[key];

            TimeSpan value;
            return TimeSpan.TryParse(strValue, out value)
                ? value
                : defaultVal;
        }

        public static TimeSpan? GetAsTimeSpan(string key, TimeSpan? defaultVal)
        {
            var strValue = ConfigurationManager.AppSettings[key];

            TimeSpan value;
            return TimeSpan.TryParse(strValue, out value)
                ? value
                : defaultVal;
        }

        public static Uri GetAsUri(string key, Uri defaultVal)
        {
            var strValue = ConfigurationManager.AppSettings[key];

            Uri value;
            return Uri.TryCreate(strValue, UriKind.RelativeOrAbsolute, out value)
                ? value
                : defaultVal;
        }
    }
}
