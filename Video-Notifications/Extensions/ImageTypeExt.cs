namespace VideoNotifications.Extensions {

    public static class ImageTypeExt {

        /// <summary>
        /// Get the image storage path for the type.
        /// </summary>
        /// <param name="type">Type of image.</param>
        public static string Path(this Database.Types.ImageType type) {
            switch (type) {
                case Database.Types.ImageType.ChannelBanner: return Constants.ChannelBannerDirectory;
                case Database.Types.ImageType.ChannelIcon: return Constants.ChannelIconDirectory;
                case Database.Types.ImageType.VideoThumbnail: return Constants.VideoThumbnailDirectory;
                default: return string.Empty;
            }

        }

    }

}
