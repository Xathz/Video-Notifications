using System;
using System.Collections.Generic;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;

namespace VideoNotifications.YouTube {

    internal class ChannelVideos : YouTubeBase {

        /// <summary>
        /// The full response of the api request.
        /// </summary>
        public SearchListResponse FullResponse { get; protected set; }

        /// <summary>
        /// List of video IDs from the channel.
        /// </summary>
        public List<string> VideosIDs { get; protected set; } = new List<string>();

        /// <summary>
        /// <see cref="VideoInfoBulk"/> instance with the <see cref="VideosIDs"/>.
        /// </summary>
        public VideoInfoBulk VideosInfoBulk { get; protected set; }

        /// <summary>
        /// Get the 20 most recent videos from a channel.
        /// </summary>
        /// <param name="channelID">ID of the channel to get videos for.</param>
        public ChannelVideos(string channelID) {
            try {
                // https://developers.google.com/youtube/v3/docs/search/list
                SearchResource.ListRequest channelSearch = APIService.Search.List("id");
                channelSearch.ChannelId = channelID;
                channelSearch.Order = SearchResource.ListRequest.OrderEnum.Date;
                channelSearch.MaxResults = 20;
                channelSearch.PrettyPrint = false;

                FullResponse = channelSearch.Execute();

                foreach (SearchResult result in FullResponse.Items) {
                    if (result.Id.Kind == "youtube#video") { VideosIDs.Add(result.Id.VideoId); }
                }

                VideosInfoBulk = new VideoInfoBulk(VideosIDs);
            } catch (Exception ex) {
                LoggingManager.Log.Error(ex, $"Failed to get channel videos for: {channelID}.");
            }
        }

    }

}
