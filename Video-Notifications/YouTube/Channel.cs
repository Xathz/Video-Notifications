using System;
using System.Collections.Generic;
using System.Linq;
//using VideoNotifications.Database.CollectionType;
using API = Google.Apis.YouTube.v3;

namespace VideoNotifications.YouTube {

    internal class Channel : YouTubeBase {

        /// <summary>
        /// Get information about a channel.
        /// </summary>
        /// <param name="channelID">ID of the channel to get information for</param>
        /// <returns>Information about a channel.</returns>
        public Database.CollectionType.YouTubeChannel Info(string channelID) {
            try {
                // https://developers.google.com/youtube/v3/docs/channels/list
                API.ChannelsResource.ListRequest channelInfo = APIService.Channels.List("id,snippet,brandingSettings");
                channelInfo.Id = channelID;
                channelInfo.MaxResults = 1;
                channelInfo.PrettyPrint = false;

                API.Data.Channel response = channelInfo.Execute().Items[0];

                Database.CollectionType.YouTubeChannel channel = new Database.CollectionType.YouTubeChannel {
                    ChannelID = response.Id,
                    Title = response.Snippet.Title,
                    Description = response.Snippet.Description,
                    URL = $"https://www.youtube.com/channel/{response.Id}",
                    BannerURL = response.BrandingSettings.Image.BannerMobileExtraHdImageUrl,
                    ThumbnailURL = GetBestThumbnail(response.Snippet.Thumbnails)
                };

                LoggingManager.Log.Info($"Channel information processed for '{channel.Title}' ({channel.ChannelID}).");

                return channel;
            } catch (Exception ex) {
                LoggingManager.Log.Error(ex, $"Failed to get channel information for: {channelID}.");

                return null;
            }
        }

        /// <summary>
        /// Get the specified most recent videos from a channel.
        /// </summary>
        /// <param name="channelID">ID of the channel to get videos for.</param>
        public List<Database.CollectionType.YouTubeVideo> RecentVideos(string channelID) {
            try {
                List<string> response = RecentVideoIDs(channelID);

                List <Database.CollectionType.YouTubeVideo> videos = new Videos().Bulk(response);

                LoggingManager.Log.Info($"Channel videos retrieved for ({channelID}), {response.Count} videoIDs were retrieved.");

                return videos;
            } catch (Exception ex) {
                LoggingManager.Log.Error(ex, $"Failed to get channel videos for: {channelID}.");

                return null;
            }
        }

        /// <summary>
        /// Get the specified most recent videos IDs from a channel.
        /// </summary>
        /// <param name="channelID">ID of the channel to get videos IDs for.</param>
        public List<string> RecentVideoIDs(string channelID) {
            try {
                // https://developers.google.com/youtube/v3/docs/search/list
                API.SearchResource.ListRequest channelSearch = APIService.Search.List("id");
                channelSearch.ChannelId = channelID;
                channelSearch.Order = API.SearchResource.ListRequest.OrderEnum.Date;
                channelSearch.SafeSearch = API.SearchResource.ListRequest.SafeSearchEnum.None;
                channelSearch.MaxResults = 20;
                channelSearch.PrettyPrint = false;

                API.Data.SearchListResponse response = channelSearch.Execute();

                //List<string> ids = new List<string>();
                //foreach (API.Data.SearchResult item in response.Items) {
                //    if (item.Id.Kind == "youtube#video") { ids.Add(item.Id.VideoId); }
                //}


                IEnumerable<string> ids = response.Items.ToList().Where(x => x.Id.Kind == "youtube#video").Select(x => x.Id.VideoId);

                LoggingManager.Log.Info($"Channel videos retrieved for ({channelID}), {response.Items.Count} videoIDs were retrieved.");

                return ids.ToList();
            } catch (Exception ex) {
                LoggingManager.Log.Error(ex, $"Failed to get channel videos for: {channelID}.");

                return null;
            }
        }

        /// <summary>
        /// Search for a channel by its name, or a multitude of other information.
        /// </summary>
        /// <param name="channelName">The name, or any other data about the channel to search for.</param>
        /// <param name="maxResults">Maximum number of channels to return. Min 1, Max 50.</param>
        public List<Database.CollectionType.YouTubeChannel> Search(string channelName, long? maxResults = 10) {
            try {
                // https://developers.google.com/youtube/v3/docs/search/list
                API.SearchResource.ListRequest channelSearch = APIService.Search.List("id,snippet");
                channelSearch.Q = channelName;
                channelSearch.Type = "channel";
                channelSearch.Order = API.SearchResource.ListRequest.OrderEnum.Relevance;
                channelSearch.MaxResults = maxResults;
                channelSearch.PrettyPrint = false;

                API.Data.SearchListResponse response = channelSearch.Execute();

                List<Database.CollectionType.YouTubeChannel> channelsReturn = new List<Database.CollectionType.YouTubeChannel>();

                foreach (API.Data.SearchResult item in response.Items) {
                    if (item.Id.Kind == "youtube#channel") {
                        API.Data.SearchResultSnippet channel = item.Snippet;

                        Database.CollectionType.YouTubeChannel channelInfo = new Database.CollectionType.YouTubeChannel {
                            ChannelID = channel.ChannelId,
                            Title = channel.Title,
                            Description = channel.Description,
                            URL = $"https://www.youtube.com/channel/{channel.ChannelId}",
                            ThumbnailURL = GetBestThumbnail(channel.Thumbnails)
                        };

                        channelsReturn.Add(channelInfo);
                        LoggingManager.Log.Info($"Channel information processed for '{channelInfo.Title}' ({channelInfo.ChannelID}).");
                    }
                }

                return channelsReturn;
            } catch (Exception ex) {
                LoggingManager.Log.Error(ex, $"Failed to search for channels, searched for term: {channelName}.");

                return null;
            }
        }
    }

}
