using System;
using System.Collections.Generic;
using LiteDB;
using VideoNotifications.Database.Types;

namespace VideoNotifications.Database {

    /// <summary>
    /// Query, insert, update, delete, and other options for the 'channel' <see cref="LiteDB.LiteCollection{Channel}"/>.
    /// </summary>
    internal abstract class Channels : Base {

        /// <summary>
        /// Get all the channels in the database.
        /// </summary>
        public static IEnumerable<Channel> GetAll() => _Channels.FindAll();

        /// <summary>
        /// Get a channel by ID.
        /// </summary>
        /// <param name="channelID">Channel ID to lookup.</param>
        public static Channel GetByID(string channelID) => _Channels.FindById(channelID);

        /// <summary>
        /// Get all videos by a channel.
        /// </summary>
        /// <param name="channelID">Channel to get videos for.</param>
        public static IEnumerable<Video> GetAllVideos(string channelID) => _Videos.Find(c => c.ChannelID == channelID);

        /// <summary>
        /// Check if a channel exists.
        /// </summary>
        /// <param name="channelID">Channel ID to check.</param>
        public static bool Exists(string channelID) => _Channels.Exists(Query.EQ("$._id", channelID));

        /// <summary>
        /// Insert a channel.
        /// </summary>
        /// <param name="channel">Channel to insert.</param>
        public static void Insert(Channel channel) {
            try {
                if (!_Channels.Exists(Query.EQ("$._id", channel.ID))) {
                    _Channels.Insert(channel);
                }

                LoggingManager.Log.Info($"'{channel.ID}' was inserted.");
            } catch (Exception ex) {
                LoggingManager.Log.Error(ex, $"Failed to insert '{channel.ID}'.");
            }
        }

        /// <summary>
        /// Insert a collection of channels.
        /// </summary>
        /// <param name="channels">Collection of channels to insert.</param>
        public static void Insert(IEnumerable<Channel> channels) {
            foreach (Channel channel in channels) {
                Insert(channel);
            }
        }

        /// <summary>
        /// Update or insert a channel.
        /// </summary>
        /// <param name="channel">Channel to update or insert.</param>
        public static void Upsert(Channel channel) {
            try {
                _Channels.Upsert(channel);

                LoggingManager.Log.Info($"'{channel.ID}' was upsert'd.");
            } catch (Exception ex) {
                LoggingManager.Log.Error(ex, $"Failed to upsert '{channel.ID}'.");
            }
        }

        /// <summary>
        /// Update or insert a collection of channels.
        /// </summary>
        /// <param name="channels">Collection of channels to update or insert.</param>
        public static void Upsert(IEnumerable<Channel> channels) {
            foreach (Channel channel in channels) {
                Upsert(channel);
            }
        }

        /// <summary>
        /// Delete a channel.
        /// </summary>
        /// <param name="channel">Channel to delete.</param>
        public static void Delete(Channel channel) {
            try {
                _Channels.Delete(channel.ID);

                LoggingManager.Log.Info($"'{channel.ID}' was deleted.");
            } catch (Exception ex) {
                LoggingManager.Log.Error(ex, $"Failed to delete '{channel.ID}'.");
            }
        }

        /// <summary>
        /// Delete a collection of channels.
        /// </summary>
        /// <param name="channels">Collection of channels to delete.</param>
        public static void Delete(IEnumerable<Channel> channels) {
            foreach (Channel channel in channels) {
                Delete(channel);
            }
        }

    }

}
