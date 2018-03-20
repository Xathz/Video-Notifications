namespace VideoNotifications.Database.CollectionType {

    /// <summary>
    /// Video watched status.
    /// </summary>
    public enum Status {

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

}
