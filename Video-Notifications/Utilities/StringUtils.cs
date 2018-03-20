using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace VideoNotifications.Utilities {

    internal static class StringUtils {

        /// <summary>
        /// Format a video description removing URLs, and attempting to remove other information.
        /// </summary>
        /// <param name="description">Video description to format.</param>
        public static string FormatVideoDescription(string description) {
            if (string.IsNullOrWhiteSpace(description)) { return description; }

            string removedURLs = Regex.Replace(description, @"(.*http[^\s]+.*)", "");

            List<string> individualLines = new List<string>();
            individualLines.AddRange(removedURLs.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries));

            foreach (string line in individualLines.ToList()) {
                if (line.TrimEnd().EndsWith(":")) { individualLines.Remove(line); }
                if (line.TrimEnd().EndsWith(";")) { individualLines.Remove(line); }
                if (line.TrimEnd().EndsWith("-")) { individualLines.Remove(line); }
                if (line.TrimEnd().EndsWith("_")) { individualLines.Remove(line); }
                if (line.TrimEnd().EndsWith("~")) { individualLines.Remove(line); }
                if (line.TrimEnd().EndsWith("+")) { individualLines.Remove(line); }
                if (line.TrimEnd().EndsWith("=")) { individualLines.Remove(line); }
            }

            return string.Join(" ", individualLines);
        }

    }

}
