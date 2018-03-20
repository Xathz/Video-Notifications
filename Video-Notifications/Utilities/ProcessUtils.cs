using System;
using System.Diagnostics;

namespace VideoNotifications.Utilities {

    internal static class ProcessUtils {

        /// <summary>
        /// Starts a program, file, or opens a URL.
        /// </summary>
        /// <param name="path">Path to the file or URL to start.</param>
        public static void Start(string path) {
            try {
                Process.Start(path);
            } catch (Exception ex) {
                LoggingManager.Log.Error(ex, $"Failed to start: {path}.");
            }
        }

    }

}
