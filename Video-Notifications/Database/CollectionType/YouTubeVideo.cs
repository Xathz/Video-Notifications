﻿using System;
using LiteDB;

namespace VideoNotifications.Database.CollectionType {

    /// <summary>
    /// A YouTube video.
    /// </summary>
    public class YouTubeVideo {

        /// <summary>
        /// Video ID.
        /// </summary>
        [BsonId]
        public string VideoID { get; set; }

        /// <summary>
        /// Channel ID. Corresponds to a <see cref="YouTubeChannel.ChannelID"/>.
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
        /// URL to the video. https://www.youtube.com/watch?v=<see cref="VideoID"/>
        /// </summary>
        public string URL { get; set; }

        /// <summary>
        /// URL to the <see cref="YouTube.YouTubeBase.GetBestThumbnail(Google.Apis.YouTube.v3.Data.ThumbnailDetails)"/> result.
        /// </summary>
        public string ThumbnailURL { get; set; }

        /// <summary>
        /// Watched status of the video.
        /// </summary>
        public Status Status { get; set; }

    }

}
