using System.Diagnostics;
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

        /// <summary>
        /// Working directory for the application.
        /// </summary>
        public static string WorkingDirectory { get; set; } = Path.Combine(ApplicationDirectory, ExecutableName);

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

        /// <summary>
        /// Downloaded images directory.
        /// </summary>
        public static string ImagesDirectory => Path.Combine(WorkingDirectory, "Images");

        /// <summary>
        /// Channel icons directory.
        /// </summary>
        public static string ChannelIconDirectory => Path.Combine(ImagesDirectory, "ChannelIcons");

        /// <summary>
        /// Video thumbnails directory.
        /// </summary>
        public static string VideoThumbnailDirectory => Path.Combine(ImagesDirectory, "VideoThumbnails");

    }

}
