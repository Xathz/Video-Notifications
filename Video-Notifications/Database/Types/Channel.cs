using LiteDB;

namespace VideoNotifications.Database.Types {

    /// <summary>
    /// A YouTube channel.
    /// </summary>
    public class Channel {

        /// <summary>
        /// Channel ID.
        /// </summary>
        [BsonId]
        public string ID { get; set; }

        /// <summary>
        /// Channel title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Channel description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// URL to the channel. https://www.youtube.com/channel/<see cref="ID"/>
        /// </summary>
        [BsonIgnore]
        public string URL => $"https://www.youtube.com/channel/{ID}";

        /// <summary>
        /// URL to the <see cref="YouTube.Base.GetBestThumbnail(Google.Apis.YouTube.v3.Data.ThumbnailDetails)"/> result.
        /// </summary>
        public string ThumbnailURL { get; set; }

    }

}
