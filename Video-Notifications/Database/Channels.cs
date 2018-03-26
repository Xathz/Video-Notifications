using System;
using System.Collections.Generic;
using LiteDB;
using VideoNotifications.Database.CollectionType;

namespace VideoNotifications.Database {

    /// <summary>
    /// Query, insert, update, delete, and other options for the 'channel' <see cref="LiteDB.LiteCollection{YouTubeChannel}"/>.
    /// </summary>
    internal abstract class Channels : DatabaseBase {

        /// <summary>
        /// Get all the channels in the database.
        /// </summary>
        public static IEnumerable<YouTubeChannel> GetAll() => _Channels.FindAll();

        /// <summary>
        /// Get a channel by ID.
        /// </summary>
        /// <param name="channelID">Channel ID to lookup.</param>
        public static YouTubeChannel GetByID(string channelID) => _Channels.FindById(channelID);

        /// <summary>
        /// Get all videos by a channel.
        /// </summary>
        /// <param name="channelID">Channel to get videos for.</param>
        public static IEnumerable<YouTubeVideo> GetAllVideos(string channelID) => _Videos.Find(c => c.ChannelID == channelID);

        /// <summary>
        /// Check if a channel exists.
        /// </summary>
        /// <param name="channelID">Channel ID to check.</param>
        public static bool Exists(string channelID) => _Channels.Exists(Query.EQ("$._id", channelID));

        /// <summary>
        /// Insert a channel.
        /// </summary>
        /// <param name="channel">Channel to insert.</param>
        public static void Insert(YouTubeChannel channel) {
            try {
                if (!_Channels.Exists(Query.EQ("$._id", channel.ChannelID))) {
                    _Channels.Insert(channel);
                }

                LoggingManager.Log.Info($"Channel '{channel.Title}' ({channel.ChannelID}) was inserted.");
            } catch (Exception ex) {
                LoggingManager.Log.Error(ex, $"Failed to insert channel: {channel.Title} ({channel.ChannelID}).");
            }
        }

        /// <summary>
        /// Insert a collection of channels.
        /// </summary>
        /// <param name="channels">Collection of channels to insert.</param>
        public static void Insert(IEnumerable<YouTubeChannel> channels) {
            foreach (YouTubeChannel channel in channels) {
                Insert(channel);
            }
        }

        /// <summary>
        /// Update or insert a channel.
        /// </summary>
        /// <param name="channel">Channel to update or insert.</param>
        public static void Upsert(YouTubeChannel channel) {
            try {
                _Channels.Upsert(channel);

                LoggingManager.Log.Info($"Channel '{channel.Title}' ({channel.ChannelID}) was upsert'd.");
            } catch (Exception ex) {
                LoggingManager.Log.Error(ex, $"Failed to upsert a channel: {channel.Title} ({channel.ChannelID}).");
            }
        }

        /// <summary>
        /// Update or insert a collection of channels.
        /// </summary>
        /// <param name="channels">Collection of channels to update or insert.</param>
        public static void Upsert(IEnumerable<YouTubeChannel> channels) {
            foreach (YouTubeChannel channel in channels) {
                Upsert(channel);
            }
        }

        /// <summary>
        /// Delete a channel.
        /// </summary>
        /// <param name="channel">Channel to delete.</param>
        public static void Delete(YouTubeChannel channel) {
            try {
                _Channels.Delete(channel.ChannelID);

                LoggingManager.Log.Info($"Channel '{channel.Title}' ({channel.ChannelID}) was deleted.");
            } catch (Exception ex) {
                LoggingManager.Log.Error(ex, $"Failed to delete channel: {channel.Title} ({channel.ChannelID}).");
            }
        }

        /// <summary>
        /// Delete a collection of channels.
        /// </summary>
        /// <param name="channels">Collection of channels to delete.</param>
        public static void Delete(IEnumerable<YouTubeChannel> channels) {
            foreach (YouTubeChannel channel in channels) {
                Delete(channel);
            }
        }

    }

}
