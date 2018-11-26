using System;
using System.Linq;
using System.Collections.Generic;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;
using VideoNotifications.Database.CollectionType;
using VideoNotifications.Utilities;

namespace VideoNotifications.YouTube {

    internal class VideoInfoBulk : YouTubeBase {

        /// <summary>
        /// The full response of the api request.
        /// </summary>
        public VideoListResponse FullResponse { get; protected set; }

        /// <summary>
        /// List of information about the videos.
        /// </summary>
        public List<YouTubeVideo> Videos { get; protected set; } = new List<YouTubeVideo>();

        /// <summary>
        /// Get information about mutiple videos.
        /// </summary>
        /// <param name="videoIDs">Videos to get information about.</param>
        public VideoInfoBulk(List<string> videoIDs) {
            try {
                // https://developers.google.com/youtube/v3/docs/videos/list
                if (videoIDs.Count == 0) { return; }

                List<List<string>> videoIDChunks = new List<List<string>>();

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
                    VideosResource.ListRequest videoSearch = APIService.Videos.List("id,snippet,contentDetails");
                    videoSearch.Id = string.Join(",", chunk);
                    videoSearch.MaxResults = chunk.Count;
                    videoSearch.PrettyPrint = false;

                    FullResponse = videoSearch.Execute();

                    IList<Video> videos = FullResponse.Items;
                    foreach (Video video in videos) {
                        YouTubeVideo videoInfo = new YouTubeVideo {
                            VideoID = video.Id,
                            ChannelID = video.Snippet.ChannelId,
                            Title = video.Snippet.Title,
                            Description = video.Snippet.Description,
                            Duration = TimeSpanUtils.ConvertDuration(video.ContentDetails.Duration),
                            Posted = video.Snippet.PublishedAt,
                            URL = $"https://www.youtube.com/watch?v={video.Id}",
                            ThumbnailURL = GetBestThumbnail(video.Snippet.Thumbnails),
                            Status = Status.Unwatched
                        };

                        Videos.Add(videoInfo);
                        LoggingManager.Log.Info($"Video information processed for '{videoInfo.Title}' ({videoInfo.VideoID}) by channel ({videoInfo.ChannelID}).");
                    }
                }

            } catch (Exception ex) {
                LoggingManager.Log.Error(ex, $"Failed to get bulk video information for: {string.Join(",", videoIDs)}.");
            }
        }

    }

}
