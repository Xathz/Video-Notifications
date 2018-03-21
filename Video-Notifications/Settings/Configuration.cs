using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using VideoNotifications.Database.CollectionType;

namespace VideoNotifications.Settings {

    public class Configuration {

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
        /// When a new channel is added, set the video status to this.
        /// This only applies to the first batch of videos that gets added to the database, when you add a new channel.
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public Status NewChannelDefaultVideoStatus { get; set; } = Status.Unwatched;

        /// <summary>
        /// Check YouTube every # (this) minutes for new videos.
        /// It is recommended to not set this lower than 10 minutes.
        /// </summary>
        public int CheckForNewVideosEveryMinutes { get; set; } = 30;

        /// <summary>
        /// Maximum videos to keep per channel, videos over this limit will be removed
        /// from the database starting with the oldest each time the application is started.
        /// </summary>
        public int MaximumVideosToKeepPerChannel { get; set; } = 50;

    }

}
