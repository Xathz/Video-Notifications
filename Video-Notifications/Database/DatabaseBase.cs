using System.Collections.Generic;
using System.Linq;
using LiteDB;
using VideoNotifications.Database.Types;
using VideoNotifications.Settings;

namespace VideoNotifications.Database {

    internal abstract class DatabaseBase {

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
            List<Video> videosToDelete = new List<Video>();
            List<LiteFileInfo> filesToDelete = new List<LiteFileInfo>();

            foreach (Channel channel in _Channels.FindAll()) {
                videosToDelete.AddRange(Channels.GetAllVideos(channel.ID).OrderByDescending(v => v.Posted).Skip(SettingsManager.Configuration.MaximumVideosToKeepPerChannel));
            }

            foreach (Video video in videosToDelete) {
                filesToDelete.AddRange(_Database.FileStorage.Find(video.ID));
                _Videos.Delete(v => v.ID == video.ID);
            }
            if (videosToDelete.Count > 0) { LoggingManager.Log.Info($"{videosToDelete.Count} old videos were deleted."); }

            foreach (LiteFileInfo file in filesToDelete) {
                _Database.FileStorage.Delete(file.Id);
            }
            if (filesToDelete.Count > 0) { LoggingManager.Log.Info($"{filesToDelete.Count} old files were deleted."); }

            _Database.Shrink();
        }

    }

}
