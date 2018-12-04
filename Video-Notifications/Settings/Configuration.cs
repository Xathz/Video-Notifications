using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using VideoNotifications.Database.Types;

namespace VideoNotifications.Settings {

    public class Configuration {

        /// <summary>
        /// When minimized, hide the window.
        /// </summary>
        public bool MinimizeToTray { get; set; } = true;

        /// <summary>
        /// Pause notifications. The application will still check for new videos,
        /// but you will not be notified of them with a popup.
        /// </summary>
        public bool PauseNotifications { get; set; } = false;

        /// <summary>
        /// Use the light version of the icon in the task tray.
        /// </summary>
        public bool UseLightTrayIcon { get; set; } = false;

        /// <summary>
        /// Use the light version of the icon in all windows titles.
        /// </summary>
        public bool UseLightWindowIcon { get; set; } = false;

        /// <summary>
        /// YouTube API key. Default: <see cref="string.Empty;"/>
        /// </summary>
        public string YouTubeAPIKey { get; set; } = string.Empty;

        /// <summary>
        /// Open the video in the browser when you mark and close the notification.
        /// </summary>
        public bool NotificationOpenVideo { get; set; } = true;

        /// <summary>
        /// When a new video notification is shown, set the video status to this.
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public WatchStatus NotificationDefaultVideoStatus { get; set; } = WatchStatus.Watched;

        /// <summary>
        /// When a new channel is added, set the video status to this.
        /// This only applies to the first batch of videos that gets added to the database, when you add a new channel.
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public WatchStatus NewChannelDefaultVideoStatus { get; set; } = WatchStatus.Unwatched;

        /// <summary>
        /// Check YouTube every # (this) minutes for new videos.
        /// It is not recommended to set this lower than 10 minutes.
        /// </summary>
        public int CheckForNewVideosEvery { get; set; } = 30;

        /// <summary>
        /// Maximum videos to keep per channel, videos over this limit will be removed
        /// from the database starting with the oldest each time the application is started.
        /// </summary>
        public int MaximumVideosToKeepPerChannel { get; set; } = 50;

        /// <summary>
        /// Images are cached in memory when loaded from the disk.
        /// Remove images from cache if unused/not accessed for more than (this) minutes.
        /// </summary>
        public int ImageCacheLifetime { get; set; } = 60;

        /// <summary>
        /// Main window location [X, Y] and size [Width, Height].
        /// </summary>
        public FormState MainWindow { get; set; } = new FormState();

        /// <summary>
        /// Notification window location [X, Y] and size [Width, Height].
        /// </summary>
        public FormState NotificationWindow { get; set; } = new FormState();

        /// <summary>
        /// Notification window location [X, Y] and size [Width, Height].
        /// </summary>
        public FormState AddChannelWindow { get; set; } = new FormState();

        /// <summary>
        /// Delete channel window location [X, Y] and size [Width, Height].
        /// </summary>
        public FormState DeleteChannelWindow { get; set; } = new FormState();

    }

}
