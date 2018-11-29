using System;
using LiteDB;

namespace VideoNotifications.Database.Types {

    /// <summary>
    /// A YouTube video.
    /// </summary>
    public class Video {

        /// <summary>
        /// Video ID.
        /// </summary>
        [BsonId]
        public string ID { get; set; }

        /// <summary>
        /// Channel ID. Corresponds to a <see cref="Channel.ID"/>.
        /// </summary>
        public string ChannelID { get; set; }

        /// <summary>
        /// Video title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Video description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Video duration.
        /// </summary>
        public TimeSpan Duration { get; set; }

        /// <summary>
        /// Date and time posted.
        /// </summary>
        public DateTime? Posted { get; set; }

        /// <summary>
        /// URL to the video. https://www.youtube.com/watch?v=<see cref="ID"/>
        /// </summary>
        [BsonIgnore]
        public string URL => $"https://www.youtube.com/watch?v={ID}";

        /// <summary>
        /// URL to the <see cref="YouTube.Base.GetBestThumbnail(Google.Apis.YouTube.v3.Data.ThumbnailDetails)"/> result.
        /// </summary>
        public string ThumbnailURL { get; set; }

        /// <summary>
        /// Watched status of the video.
        /// </summary>
        public WatchStatus WatchStatus { get; set; }

    }

}
