﻿using System.Diagnostics;
using System.IO;

namespace VideoNotifications {

    internal static class Constants {

        /// <summary>
        /// Application name.
        /// </summary>
        public const string ApplicationName = "Video Notifications";

        /// <summary>
        /// Application name with no spaces. 
        /// </summary>
        public const string ApplicationNameFormatted = "VideoNotifications";

        /// <summary>
        /// Current location (including filename and extension) of the running executable.
        /// </summary>
        public static string ExecutablePath => Process.GetCurrentProcess().MainModule.FileName;

        /// <summary>
        /// Current executable name minus the extension.
        /// </summary>
        public static string ExecutableName => Path.GetFileNameWithoutExtension(ExecutablePath);

        /// <summary>
        /// Current directory of the running executable.
        /// </summary>
        public static string ApplicationDirectory => Path.GetDirectoryName(ExecutablePath);

        private static string _WorkingDirectory = Path.Combine(ApplicationDirectory, ExecutableName);
        /// <summary>
        /// Working directory for the application.
        /// </summary>
        public static string WorkingDirectory {
            get => _WorkingDirectory;
            set => _WorkingDirectory = value;
        }

        /// <summary>
        /// Log files for the application.
        /// </summary>
        public static string LogDirectory => Path.Combine(WorkingDirectory, "Logs");

        /// <summary>
        /// Settings file location.
        /// </summary>
        public static string SettingsFile => Path.Combine(WorkingDirectory, $"{ExecutableName}.settings");

        /// <summary>
        /// YouTube video database location.
        /// </summary>
        public static string YouTubeDatabaseFile => Path.Combine(WorkingDirectory, "YouTubeData.db");

    }

}
