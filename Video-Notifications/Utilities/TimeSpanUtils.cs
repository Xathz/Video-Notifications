using System;
using Humanizer;

namespace VideoNotifications.Utilities {

    internal static class TimeSpanUtils {

        /// <summary>
        /// Convert an ISO 8601 timestamp to a <see cref="TimeSpan"/>.
        /// </summary>
        /// <param name="duration">The duration to convert.</param>
        public static TimeSpan ConvertDuration(string duration) => System.Xml.XmlConvert.ToTimeSpan(duration);

        /// <summary>
        /// Formats a <see cref="TimeSpan"/> into a human readable <see cref="string"/>.
        /// </summary>
        /// <param name="duration">The duration to format.</param>
        /// <returns>Example: 19 minutes, 15 seconds</returns>
        public static string ConvertDuration(TimeSpan duration) => (duration.Humanize(4, false));

        /// <summary>
        /// Formats a <see cref="TimeSpan"/> into a human readable <see cref="string"/>, but more compact than <see cref="ConvertDuration"/>.
        /// </summary>
        /// <param name="duration">The duration to format.</param>
        /// <returns>Example: 19:15</returns>
        public static string ConvertDurationCompact(TimeSpan duration) => (duration.Hours == 0) ? string.Format("{00:mm\\:ss}", duration) : string.Format("{00:hh\\:mm\\:ss}", duration);

    }

}
