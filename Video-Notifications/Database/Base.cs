using System.Collections.Generic;
using System.Linq;
using LiteDB;
using VideoNotifications.Database.Types;
using VideoNotifications.Settings;

namespace VideoNotifications.Database {

    internal abstract class Base {

        /// <summary>
        /// The LiteDB Database.
        /// </summary>
        protected static LiteDatabase _Database = new LiteDatabase(Constants.YouTubeDatabaseFile);

        /// <summary>
        /// Collection of channels.
        /// </summary>
        protected static LiteCollection<Channel> _Channels = _Database.GetCollection<Channel>("channels");

        /// <summary>
        /// Collection of videos.
        /// </summary>
        protected static LiteCollection<Video> _Videos = _Database.GetCollection<Video>("videos");

        /// <summary>
        /// Log messages from the <see cref="_Database"/>.
        /// </summary>
        /// <param name="message">Message to log.</param>
        protected static void DatabaseLogEvent(string message) => LoggingManager.Log.Error(message);

        /// <summary>
        /// Setup the database with various settings for the engine. This needs to be called first before any other <see cref="Database"/> methods/functions are called.
        /// </summary>
        public static void Initialize() {
            _Database.Engine.UserVersion = 1;
            _Database.Log.Level = Logger.ERROR | Logger.RECOVERY;
            _Database.Log.Logging += DatabaseLogEvent;

            _Channels.EnsureIndex(c => c.ID);
            _Videos.EnsureIndex(v => v.ID);
            _Videos.EnsureIndex(v => v.ChannelID);
            _Videos.EnsureIndex(v => v.Posted);

            PruneVideos();

            LoggingManager.Log.Info("Database initialization finished.");
        }

        /// <summary>
        /// Keep the <see cref="SettingsManager.Configuration.MaximumVideosToKeepPerChannel"/> most recent videos for every channel, delete all else.
        /// </summary>
        protected static void PruneVideos() {
            List<Video> delete = new List<Video>();

            foreach (Channel channel in _Channels.FindAll()) {
                delete.AddRange(Channels.GetAllVideos(channel.ID).OrderByDescending(v => v.Posted).Skip(SettingsManager.Configuration.MaximumVideosToKeepPerChannel));
            }

            foreach (Video video in delete) {
                _Videos.Delete(v => v.ID == video.ID);
                ImageFile.Delete(video.ID, ImageType.VideoThumbnail);
            }
            if (delete.Count > 0) { LoggingManager.Log.Info($"{delete.Count} old videos and thumbnails were deleted."); }

            _Database.Shrink();
        }

    }

}
