using LiteDB;

namespace VideoNotifications.Database.CollectionType {

    /// <summary>
    /// A YouTube channel.
    /// </summary>
    public class YouTubeChannel {

        /// <summary>
        /// Channel ID.
        /// </summary>
        [BsonId]
        public string ChannelID { get; set; }

        /// <summary>
        /// Channel title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Channel description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// URL to the channel. https://www.youtube.com/channel/<see cref="ChannelID"/>
        /// </summary>
        public string URL { get; set; }

        /// <summary>
        /// URL to the channel banner.
        /// </summary>
        public string BannerURL { get; set; }

        /// <summary>
        /// URL to the <see cref="YouTube.YouTubeBase.GetBestThumbnail(Google.Apis.YouTube.v3.Data.ThumbnailDetails)"/> result.
        /// </summary>
        public string ThumbnailURL { get; set; }

    }

}
