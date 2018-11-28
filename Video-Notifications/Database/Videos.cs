using System;
using System.Collections.Generic;
using LiteDB;
using VideoNotifications.Database.Types;

namespace VideoNotifications.Database {

    /// <summary>
    /// Query, insert, update, delete, and other options for the 'videos' <see cref="LiteDB.LiteCollection{Video}"/>.
    /// </summary>
    internal abstract class Videos : DatabaseBase {

        /// <summary>
        /// Get all the videos in the database.
        /// </summary>
        public static IEnumerable<Video> GetAll() => _Videos.FindAll();

        /// <summary>
        /// Get a video by ID.
        /// </summary>
        /// <param name="videoID">Video ID to lookup.</param>
        public static Video GetByID(string videoID) => _Videos.FindById(videoID);

        /// <summary>
        /// Search for videos that contain a tag, or part of the tag.
        /// </summary>
        /// <param name="tag">Tag to search for.</param>
        /// <param name="channelID">Only search for videos from this channel. If not defined, <see langword="null"/>, or is a blank/empty string; search all channels.</param>
        /// <remarks>This is no longer used or needed, but it was a pain to figure out, so it's staying.</remarks>
        public static IEnumerable<Video> TagSearch(string tag, string channelID = null) =>
            string.IsNullOrWhiteSpace(channelID)
            ? _Videos.Find(Query.Contains("$.Tags[*]", tag))
            : _Videos.Find(Query.And(Query.Contains("$.Tags[*]", tag), Query.Contains("$.ChannelID", channelID)));

        /// <summary>
        /// Get all unwatched videos.
        /// </summary>
        public static IEnumerable<Video> GetAllUnwatched() => _Videos.Find(v => v.WatchStatus == WatchStatus.Unwatched);

        /// <summary>
        /// Get all dismissed videos.
        /// </summary>
        public static IEnumerable<Video> GetAllDismissed() => _Videos.Find(v => v.WatchStatus == WatchStatus.Dismissed);

        /// <summary>
        /// Get all ignored videos.
        /// </summary>
        public static IEnumerable<Video> GetAllIgnored() => _Videos.Find(v => v.WatchStatus == WatchStatus.Ignored);

        /// <summary>
        /// Check if a video exists.
        /// </summary>
        /// <param name="videoID">Video ID to check.</param>
        public static bool Exists(string videoID) => _Videos.Exists(Query.EQ("$._id", videoID));

        /// <summary>
        /// Insert a video.
        /// </summary>
        /// <param name="video">Video to insert.</param>
        public static void Insert(Video video) {
            try {
                if (!Exists(video.ID)) {
                    _Videos.Insert(video);

                    LoggingManager.Log.Info($"'{video.ID}' was inserted.");
                }
            } catch (Exception ex) {
                LoggingManager.Log.Error(ex, $"Failed to insert '{video.ID}'.");
            }
        }

        /// <summary>
        /// Insert a collection of videos.
        /// </summary>
        /// <param name="videos">Collection of videos to insert.</param>
        public static void Insert(IEnumerable<Video> videos) {
            foreach (Video video in videos) {
                Insert(video);
            }
        }

        /// <summary>
        /// Update a video.
        /// </summary>
        /// <param name="video">Video to update.</param>
        public static void Update(Video video) {
            try {
                _Videos.Update(video);

                LoggingManager.Log.Info($"'{video.ID}' was updated.");
            } catch (Exception ex) {
                LoggingManager.Log.Error(ex, $"Failed to update '{video.ID}'.");
            }
        }

        /// <summary>
        /// Update a collection of videos.
        /// </summary>
        /// <param name="videos">Collection of videos to update</param>
        public static void Update(IEnumerable<Video> videos) {
            foreach (Video video in videos) {
                Update(video);
            }
        }

        /// <summary>
        /// Delete a video.
        /// </summary>
        /// <param name="video">Video to delete.</param>
        public static void Delete(Video video) {
            try {
                _Videos.Delete(video.ID);

                LoggingManager.Log.Info($"'{video.ID}' was deleted.");
            } catch (Exception ex) {
                LoggingManager.Log.Error(ex, $"Failed to delete '{video.ID}'.");
            }
        }

        /// <summary>
        /// Delete a collection of videos.
        /// </summary>
        /// <param name="videos">Collection of videos to delete.</param>
        public static void Delete(IEnumerable<Video> videos) {
            foreach (Video video in videos) {
                Delete(video);
            }
        }

        /// <summary>
        /// Set the videos watched <see cref="WatchStatus"/>.
        /// </summary>
        /// <param name="videoID">Video ID to set status for.</param>
        /// <param name="status">Status to set the video to.</param>
        public static void SetStatus(string videoID, WatchStatus status) {
            try {
                Video video = _Videos.FindById(videoID);
                if (video != null) {
                    video.WatchStatus = status;
                    Update(video);

                    LoggingManager.Log.Info($"Status was changed for '{videoID}'.");
                }
            } catch (Exception ex) {
                LoggingManager.Log.Error(ex, $"Failed to set status on '{videoID}'.");
            }
        }

    }

}
