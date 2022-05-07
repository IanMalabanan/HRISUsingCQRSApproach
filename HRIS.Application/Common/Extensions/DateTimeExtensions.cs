using System;
using System.Collections.Generic;
using System.Text;

namespace System
{
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Returns string Date Only for display
        /// </summary>
        /// <param name="dt">PH DateTime</param>
        /// <returns></returns>
        public static string ToDateOnlyDisplayFormat(this DateTime dt)
        {
            var _output = dt.ToString("yyyy-MM-dd");
            return _output;
        }

        /// <summary>
        /// Returns string Date Only for display
        /// </summary>
        /// <param name="dt">PH DateTime</param>
        /// <returns></returns>
        public static string ToDateOnlyDisplayFormat(this DateTime? dt)
        {
            if (!dt.HasValue) return null;
            var _output = dt.Value.ToString("yyyy-MM-dd");
            return _output;
        }

        /// <summary>
        /// Returns string DateTime for display
        /// </summary>
        /// <param name="dt">PH DateTime</param>
        /// <returns></returns>
        public static string ToDateTimeDisplayFormat(this DateTime dt)
        {
            var _output = dt.ToString("yyyy-MM-dd HH:mm");
            return _output;
        }

        /// <summary>
        /// Returns string DateTime for display
        /// </summary>
        /// <param name="dt">PH DateTime</param>
        /// <returns></returns>
        public static string ToDateTimeDisplayFormat(this DateTime? dt)
        {
            if (!dt.HasValue) return null;
            var _output = dt.Value.ToString("yyyy-MM-dd HH:mm");
            return _output;
        }

        /// <summary>
        /// Returns string DateTime for display
        /// </summary>
        /// <param name="dt">PH DateTime</param>
        /// <returns></returns>
        public static DateTime? ToNullableDateTime(this string datetimeValue)
        {
            if (string.IsNullOrEmpty(datetimeValue)) return null;

            DateTime _output;

            if (!DateTime.TryParse(datetimeValue, out _output))
                return null;

            return _output;
        }

        /// <summary>
        /// Returns string DateTime for display
        /// </summary>
        /// <param name="dt">PH DateTime</param>
        /// <returns></returns>
        public static DateTime ToDateTime(this string datetimeValue)
        {
            DateTime _output = DateTime.Parse(datetimeValue);
            return _output;
        }
    }
}
