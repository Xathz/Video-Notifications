using System;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;
using VideoNotifications.Database.CollectionType;
using VideoNotifications.Utilities;

namespace VideoNotifications.YouTube {

    internal class VideoInfo : YouTubeBase {

        /// <summary>
        /// The full response of the api request.
        /// </summary>
        public VideoListResponse FullResponse { get; protected set; }

        /// <summary>
        /// Information about the video.
        /// </summary>
        public YouTubeVideo Info { get; protected set; }

        /// <summary>
        /// Get information about a video.
        /// </summary>
        /// <param name="videoID">ID of the video to get information for.</param>
        public VideoInfo(string videoID) {
            try {
                // https://developers.google.com/youtube/v3/docs/videos/list
                VideosResource.ListRequest videoSearch = APIService.Videos.List("id,snippet,contentDetails");
                videoSearch.Id = videoID;
                videoSearch.MaxResults = 1;
                videoSearch.PrettyPrint = false;

                FullResponse = videoSearch.Execute();

                Video video = FullResponse.Items[0];
                Info = new YouTubeVideo {
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
            } catch (Exception ex) {
                LoggingManager.Log.Error(ex, $"Failed to get video information for: {videoID}.");
            }
        }

    }

}
