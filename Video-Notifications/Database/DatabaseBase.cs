using System.Collections.Generic;
using System.Linq;
using LiteDB;
using VideoNotifications.Database.CollectionType;
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
        protected static LiteCollection<YouTubeChannel> _Channels = _Database.GetCollection<YouTubeChannel>("channels");

        /// <summary>
        /// Collection of videos.
        /// </summary>
        protected static LiteCollection<YouTubeVideo> _Videos = _Database.GetCollection<YouTubeVideo>("videos");

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

            _Channels.EnsureIndex(c => c.ChannelID);
            _Videos.EnsureIndex(v => v.VideoID);
            _Videos.EnsureIndex(v => v.ChannelID);
            _Videos.EnsureIndex(v => v.Posted);

            PruneVideos();

            LoggingManager.Log.Info("Database initialization finished.");
        }

        /// <summary>
        /// Keep the <see cref="SettingsManager.Configuration.MaximumVideosToKeepPerChannel"/> most recent videos for every channel, delete all else.
        /// </summary>
        protected static void PruneVideos() {
            List<YouTubeVideo> videosToDelete = new List<YouTubeVideo>();
            List<LiteFileInfo> filesToDelete = new List<LiteFileInfo>();

            foreach (YouTubeChannel channel in _Channels.FindAll()) {
                videosToDelete.AddRange(Channels.GetAllVideos(channel.ChannelID).OrderByDescending(v => v.Posted).Skip(SettingsManager.Configuration.MaximumVideosToKeepPerChannel));
            }

            foreach (YouTubeVideo video in videosToDelete) {
                filesToDelete.AddRange(_Database.FileStorage.Find(video.VideoID));
                _Videos.Delete(v => v.VideoID == video.VideoID);
            }
            if (videosToDelete.Count > 0) { LoggingManager.Log.Info($"{videosToDelete.Count} old videos were deleted."); }

            foreach (LiteFileInfo file in filesToDelete) {
                _Database.FileStorage.Delete(file.Id);
            }
            if (filesToDelete.Count > 0) { LoggingManager.Log.Info($"{filesToDelete.Count} old files were deleted."); }

            // TODO Investigate: This throws an exception, 'LiteDB.LiteException: Index key must be less than 512 bytes'.
            // Update: Removed the 'EnsureIndex' for '_Videos' (VideoNotifications.Database.DatabaseBase.cs#L40) for the video tags, that seemed to fix the issue.
            // https://github.com/mbdavid/LiteDB/blob/master/LiteDB/Engine/Services/IndexService.cs#L95
            // https://github.com/mbdavid/LiteDB/wiki/Indexes
            _Database.Shrink();
        }

    }

}
