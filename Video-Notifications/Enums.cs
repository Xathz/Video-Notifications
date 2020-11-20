namespace VideoNotifications {

    /// <summary>
    /// Video watched status.
    /// </summary>
    public enum WatchStatus {

        /// <summary>
        /// The video is unwatched. This is the default status.
        /// </summary>
        Unwatched,

        /// <summary>
        /// Video was dismissed for now, ask later.
        /// </summary>
        Dismissed,

        /// <summary>
        /// Video was ignored, do not ask again.
        /// </summary>
        Ignored,

        /// <summary>
        /// Video was watched.
        /// </summary>
        Watched

    }

    /// <summary>
    /// Type of image.
    /// </summary>
    public enum ImageType {

        /// <summary>
        /// Channel icon.
        /// </summary>
        ChannelIcon,

        /// <summary>
        /// Video thumbnail
        /// </summary>
        VideoThumbnail

    }

}
