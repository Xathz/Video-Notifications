using System;
using System.Collections.Generic;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;
using VideoNotifications.Database.CollectionType;

namespace VideoNotifications.YouTube {

    internal class SearchForChannels : YouTubeBase {

        /// <summary>
        /// The full response of the api request.
        /// </summary>
        public SearchListResponse FullResponse { get; protected set; }

        /// <summary>
        /// List of the most relevance channels found.
        /// </summary>
        public List<YouTubeChannel> Channels { get; protected set; } = new List<YouTubeChannel>();

        /// <summary>
        /// Search for a channel by its name, or a multitude of other information.
        /// </summary>
        /// <param name="channelName">The name, or any other data about the channel to search for.</param>
        /// <param name="maxResults">Maximum number of channels to return. Min 1, Max 50.</param>
        public SearchForChannels(string channelName, long? maxResults = 10) {
            try {
                // https://developers.google.com/youtube/v3/docs/search/list
                SearchResource.ListRequest channelSearch = APIService.Search.List("id,snippet");
                channelSearch.Q = channelName;
                channelSearch.Type = "channel";
                channelSearch.Order = SearchResource.ListRequest.OrderEnum.Relevance;
                channelSearch.MaxResults = maxResults;
                channelSearch.PrettyPrint = false;

                FullResponse = channelSearch.Execute();

                foreach (SearchResult result in FullResponse.Items) {
                    if (result.Id.Kind == "youtube#channel") {
                        SearchResultSnippet channel = result.Snippet;

                        YouTubeChannel channelInfo = new YouTubeChannel {
                            ChannelID = channel.ChannelId,
                            Title = channel.Title,
                            Description = channel.Description,
                            URL = $"https://www.youtube.com/channel/{channel.ChannelId}",
                            ThumbnailURL = GetBestThumbnail(channel.Thumbnails)
                        };

                        Channels.Add(channelInfo);
                        LoggingManager.Log.Info($"Channel information processed for '{channelInfo.Title}' ({channelInfo.ChannelID}).");
                    }
                }
            } catch (Exception ex) {
                LoggingManager.Log.Error(ex, $"Failed to search for channels, searched for term: {channelName}.");
            }
        }

    }

}
