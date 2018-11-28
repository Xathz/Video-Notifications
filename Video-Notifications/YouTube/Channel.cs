using System;
using System.Collections.Generic;
using System.Linq;
using API = Google.Apis.YouTube.v3;

namespace VideoNotifications.YouTube {

    internal class Channel : YouTubeBase {

        /// <summary>
        /// Get information about a channel.
        /// </summary>
        /// <param name="channelID">ID of the channel to get information for</param>
        /// <returns>Information about a channel.</returns>
        public Database.Types.Channel Info(string channelID) {
            try {
                // https://developers.google.com/youtube/v3/docs/channels/list
                API.ChannelsResource.ListRequest channelInfo = APIService.Channels.List("id,snippet,brandingSettings");
                channelInfo.Id = channelID;
                channelInfo.MaxResults = 1;
                channelInfo.PrettyPrint = false;

                API.Data.Channel response = channelInfo.Execute().Items[0];

                Database.Types.Channel channel = new Database.Types.Channel {
                    ID = response.Id,
                    Title = response.Snippet.Title,
                    Description = response.Snippet.Description,
                    BannerURL = response.BrandingSettings.Image.BannerMobileExtraHdImageUrl,
                    ThumbnailURL = GetBestThumbnail(response.Snippet.Thumbnails)
                };

                LoggingManager.Log.Info($"Information processed for '{channel.ID}'.");
                return channel;
            } catch (Exception ex) {
                LoggingManager.Log.Error(ex, $"Failed to get information for '{channelID}'.");
                return null;
            }
        }

        /// <summary>
        /// Get the specified most recent videos from a channel.
        /// </summary>
        /// <param name="channelID">ID of the channel to get videos for.</param>
        public List<Database.Types.Video> RecentVideos(string channelID) {
            try {
                List<Database.Types.Video> videos = new Videos().Bulk(RecentVideoIDs(channelID));

                LoggingManager.Log.Info($"Videos retrieved for '{channelID}' with {videos.Count} results.");
                return videos;
            } catch (Exception ex) {
                LoggingManager.Log.Error(ex, $"Failed to get videos for '{channelID}'.");
                return new List<Database.Types.Video>();
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

                List<string> ids = channelSearch.Execute().Items.ToList().Where(x => x.Id.Kind == "youtube#video").Select(x => x.Id.VideoId).ToList();

                LoggingManager.Log.Info($"Videos retrieved for '{channelID}' with {ids.Count} results.");
                return ids;
            } catch (Exception ex) {
                LoggingManager.Log.Error(ex, $"Failed to get videos for '{channelID}'.");
                return new List<string>();
            }
        }

        /// <summary>
        /// Search for a channel by its name, or a multitude of other information.
        /// </summary>
        /// <param name="channelName">The name, or any other data about the channel to search for.</param>
        /// <param name="maxResults">Maximum number of channels to return. Min 1, Max 50.</param>
        public List<Database.Types.Channel> Search(string channelName, long? maxResults = 10) {
            try {
                // https://developers.google.com/youtube/v3/docs/search/list
                API.SearchResource.ListRequest channelSearch = APIService.Search.List("id,snippet");
                channelSearch.Q = channelName;
                channelSearch.Type = "channel";
                channelSearch.Order = API.SearchResource.ListRequest.OrderEnum.Relevance;
                channelSearch.MaxResults = maxResults;
                channelSearch.PrettyPrint = false;

                List<Database.Types.Channel> results = new List<Database.Types.Channel>();

                foreach (API.Data.SearchResult item in channelSearch.Execute().Items) {
                    if (item.Id.Kind == "youtube#channel") {
                        API.Data.SearchResultSnippet channel = item.Snippet;

                        Database.Types.Channel channelInfo = new Database.Types.Channel {
                            ID = channel.ChannelId,
                            Title = channel.Title,
                            Description = channel.Description,
                            ThumbnailURL = GetBestThumbnail(channel.Thumbnails)
                        };

                        results.Add(channelInfo);
                        LoggingManager.Log.Info($"Information processed for '{channelInfo.ID}'.");
                    }
                }

                return results;
            } catch (Exception ex) {
                LoggingManager.Log.Error(ex, $"Failed to search for '{channelName}'.");
                return new List<Database.Types.Channel>();
            }
        }

    }

}
