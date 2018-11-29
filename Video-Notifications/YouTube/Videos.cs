using System;
using System.Collections.Generic;
using System.Linq;
using VideoNotifications.Utilities;
using API = Google.Apis.YouTube.v3;

namespace VideoNotifications.YouTube {

    internal class Videos : Base {

        /// <summary>
        /// Get information about a single video.
        /// </summary>
        /// <param name="id">ID of the video to get information for.</param>
        public Database.Types.Video Single(string id) {
            try {
                // https://developers.google.com/youtube/v3/docs/videos/list
                API.VideosResource.ListRequest videoSearch = APIService.Videos.List("id,snippet,contentDetails.duration");
                videoSearch.Id = id;
                videoSearch.MaxResults = 1;
                videoSearch.PrettyPrint = false;

                API.Data.Video response = videoSearch.Execute().Items[0];

                Database.Types.Video video = new Database.Types.Video {
                    ID = response.Id,
                    ChannelID = response.Snippet.ChannelId,
                    Title = response.Snippet.Title,
                    Description = response.Snippet.Description,
                    Duration = TimeSpanUtils.ConvertDuration(response.ContentDetails.Duration),
                    Posted = response.Snippet.PublishedAt,
                    ThumbnailURL = GetBestThumbnail(response.Snippet.Thumbnails),
                    WatchStatus = WatchStatus.Unwatched
                };

                LoggingManager.Log.Info($"Information processed for '{video.ID}' posted by '{video.ChannelID}'.");
                return video;
            } catch (Exception ex) {
                LoggingManager.Log.Error(ex, $"Failed to get information for '{id}'.");
                return null;
            }
        }

        /// <summary>
        /// Get information about mutiple videos.
        /// </summary>
        /// <param name="ids">Videos to get information for.</param>
        public List<Database.Types.Video> Bulk(List<string> ids) {
            try {
                // https://developers.google.com/youtube/v3/docs/videos/list
                if (ids.Count == 0) { return null; }

                List<(int chunkID, List<string> videoIDs)> videoIDChunks = new List<(int chunkID, List<string> videoIDs)>();
                List<Database.Types.Video> videosReturn = new List<Database.Types.Video>();
                int chunkNumber = 1;

                SplitIntoChunksLoop:          
                if (ids.Count > 0) {
                    if ((ids.Count > 50) || (ids.Count == 50)) {
                        videoIDChunks.Add((chunkNumber, ids.Take(50).ToList()));
                        ids.RemoveRange(0, 50);
                    } else {
                        videoIDChunks.Add((chunkNumber, ids.Take(ids.Count).ToList()));
                        ids.RemoveRange(0, ids.Count);
                    }

                    chunkNumber++;
                    goto SplitIntoChunksLoop;
                }
                LoggingManager.Log.Info($"Video chunk processing finished with {videoIDChunks.Count} total chunks.");

                foreach ((int chunkID, List<string> videoIDs) in videoIDChunks) {
                    API.VideosResource.ListRequest videoSearch = APIService.Videos.List("id,snippet,contentDetails");
                    videoSearch.Id = string.Join(",", videoIDs);
                    videoSearch.MaxResults = videoIDs.Count;
                    videoSearch.PrettyPrint = false;

                    API.Data.VideoListResponse response = videoSearch.Execute();

                    IList<API.Data.Video> videos = response.Items;
                    foreach (API.Data.Video video in videos) {
                        Database.Types.Video videoInfo = new Database.Types.Video {
                            ID = video.Id,
                            ChannelID = video.Snippet.ChannelId,
                            Title = video.Snippet.Title,
                            Description = video.Snippet.Description,
                            Duration = TimeSpanUtils.ConvertDuration(video.ContentDetails.Duration),
                            Posted = video.Snippet.PublishedAt,
                            ThumbnailURL = GetBestThumbnail(video.Snippet.Thumbnails),
                            WatchStatus = WatchStatus.Unwatched
                        };

                        videosReturn.Add(videoInfo);
                        LoggingManager.Log.Info($"Chunk {chunkID}: Information processed for '{videoInfo.ID}' posted by '{videoInfo.ChannelID}'.");
                    }
                }

                return videosReturn;
            } catch (Exception ex) {
                LoggingManager.Log.Error(ex, $"Failed to get information for '{string.Join(",", ids)}'.");
                return null;
            }
        }

    }

}
