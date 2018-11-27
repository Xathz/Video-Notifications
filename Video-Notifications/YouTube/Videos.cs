using System;
using API = Google.Apis.YouTube.v3;
//using VideoNotifications.Database.CollectionType;
using VideoNotifications.Utilities;
using System.Collections.Generic;
using System.Linq;

namespace VideoNotifications.YouTube {

    internal class Videos : YouTubeBase {

        /// <summary>
        /// Get information about a video.
        /// </summary>
        /// <param name="videoID">ID of the video to get information for.</param>
        public Database.CollectionType.YouTubeVideo Single(string videoID) {
            try {
                // https://developers.google.com/youtube/v3/docs/videos/list
                API.VideosResource.ListRequest videoSearch = APIService.Videos.List("id,snippet,contentDetails.duration");
                videoSearch.Id = videoID;
                videoSearch.MaxResults = 1;
                videoSearch.PrettyPrint = false;

                API.Data.Video response = videoSearch.Execute().Items[0];

                Database.CollectionType.YouTubeVideo video = new Database.CollectionType.YouTubeVideo {
                    VideoID = response.Id,
                    ChannelID = response.Snippet.ChannelId,
                    Title = response.Snippet.Title,
                    Description = response.Snippet.Description,
                    Duration = TimeSpanUtils.ConvertDuration(response.ContentDetails.Duration),
                    Posted = response.Snippet.PublishedAt,
                    URL = $"https://www.youtube.com/watch?v={response.Id}",
                    ThumbnailURL = GetBestThumbnail(response.Snippet.Thumbnails),
                    Status = Database.CollectionType.Status.Unwatched
                };

                LoggingManager.Log.Info($"Video information processed for '{video.Title}' ({video.VideoID}) by channel ({video.ChannelID}).");

                return video;
            } catch (Exception ex) {
                LoggingManager.Log.Error(ex, $"Failed to get video information for: {videoID}.");

                return null;
            }
        }

        /// <summary>
        /// Get information about mutiple videos.
        /// </summary>
        /// <param name="videoIDs">Videos to get information about.</param>
        public List<Database.CollectionType.YouTubeVideo> Bulk(List<string> videoIDs) {
            try {
                // https://developers.google.com/youtube/v3/docs/videos/list
                if (videoIDs.Count == 0) { return null; }

                List<List<string>> videoIDChunks = new List<List<string>>();
                List<Database.CollectionType.YouTubeVideo> videosReturn = new List<Database.CollectionType.YouTubeVideo>();

                SplitIntoChunksLoop:
                if (videoIDs.Count > 0) {

                    if ((videoIDs.Count > 50) || (videoIDs.Count == 50)) {
                        videoIDChunks.Add(videoIDs.Take(50).ToList());
                        videoIDs.RemoveRange(0, 50);
                    } else {
                        videoIDChunks.Add(videoIDs.Take(videoIDs.Count).ToList());
                        videoIDs.RemoveRange(0, videoIDs.Count);
                    }

                    goto SplitIntoChunksLoop;
                }

                LoggingManager.Log.Info($"Video ID chunk processing finished. {videoIDChunks.Count} total chunks.");
                for (int i = 0; i < videoIDChunks.Count; i++) {
                    LoggingManager.Log.Info($"Chunk #{i} contains {videoIDChunks[i].Count} videoIDs.");
                }

                foreach (List<string> chunk in videoIDChunks) {
                    API.VideosResource.ListRequest videoSearch = APIService.Videos.List("id,snippet,contentDetails");
                    videoSearch.Id = string.Join(",", chunk);
                    videoSearch.MaxResults = chunk.Count;
                    videoSearch.PrettyPrint = false;

                    API.Data.VideoListResponse response = videoSearch.Execute();

                    IList<API.Data.Video> videos = response.Items;
                    foreach (API.Data.Video video in videos) {
                        Database.CollectionType.YouTubeVideo videoInfo = new Database.CollectionType.YouTubeVideo {
                            VideoID = video.Id,
                            ChannelID = video.Snippet.ChannelId,
                            Title = video.Snippet.Title,
                            Description = video.Snippet.Description,
                            Duration = TimeSpanUtils.ConvertDuration(video.ContentDetails.Duration),
                            Posted = video.Snippet.PublishedAt,
                            URL = $"https://www.youtube.com/watch?v={video.Id}",
                            ThumbnailURL = GetBestThumbnail(video.Snippet.Thumbnails),
                            Status = Database.CollectionType.Status.Unwatched
                        };

                        videosReturn.Add(videoInfo);
                        LoggingManager.Log.Info($"Video information processed for '{videoInfo.Title}' ({videoInfo.VideoID}) by channel ({videoInfo.ChannelID}).");
                    }
                }

                return videosReturn;
            } catch (Exception ex) {
                LoggingManager.Log.Error(ex, $"Failed to get bulk video information for: {string.Join(",", videoIDs)}.");

                return null;
            }
        }

    }

}
