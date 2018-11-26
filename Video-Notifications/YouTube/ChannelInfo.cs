using System;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;
using VideoNotifications.Database.CollectionType;

namespace VideoNotifications.YouTube {

    internal class ChannelInfo : YouTubeBase {

        /// <summary>
        /// The full response of the api request.
        /// </summary>
        public ChannelListResponse FullResponse { get; protected set; }

        /// <summary>
        /// Information about the channel.
        /// </summary>
        public YouTubeChannel Info { get; protected set; }

        private ChannelVideos _ChannelVideos = null;
        /// <summary>
        /// Get videos from channel.
        /// </summary>
        public ChannelVideos ChannelVideos {
            get {
                _ChannelVideos = _ChannelVideos ?? new ChannelVideos(Info.ChannelID, false);
                return _ChannelVideos;
            }
        }

        /// <summary>
        /// Get information about a channel.
        /// </summary>
        /// <param name="channelID">ID of the channel to get information for.</param>
        public ChannelInfo(string channelID) {
            try {
                // https://developers.google.com/youtube/v3/docs/channels/list
                ChannelsResource.ListRequest channelInfo = APIService.Channels.List("id,snippet,brandingSettings");
                channelInfo.Id = channelID;
                channelInfo.MaxResults = 1;
                channelInfo.PrettyPrint = false;

                FullResponse = channelInfo.Execute();

                Channel channel = FullResponse.Items[0];
                Info = new YouTubeChannel {
                    ChannelID = channel.Id,
                    Title = channel.Snippet.Title,
                    Description = channel.Snippet.Description,
                    URL = $"https://www.youtube.com/channel/{channel.Id}",
                    BannerURL = channel.BrandingSettings.Image.BannerMobileExtraHdImageUrl,
                    ThumbnailURL = GetBestThumbnail(channel.Snippet.Thumbnails)
                };

                LoggingManager.Log.Info($"Channel information processed for '{Info.Title}' ({Info.ChannelID}).");
            } catch (Exception ex) {
                LoggingManager.Log.Error(ex, $"Failed to get channel information for: {channelID}.");
            }
        }

    }

}
