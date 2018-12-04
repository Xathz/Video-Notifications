namespace VideoNotifications.Extensions {

    internal static class ImageTypeExt {

        /// <summary>
        /// Get the image storage path for the type.
        /// </summary>
        /// <param name="type">Type of image.</param>
        public static string Path(this ImageType type) {
            switch (type) {
                case ImageType.ChannelBanner: return Constants.ChannelBannerDirectory;
                case ImageType.ChannelIcon: return Constants.ChannelIconDirectory;
                case ImageType.VideoThumbnail: return Constants.VideoThumbnailDirectory;
                default: return string.Empty;
            }

        }

    }

}
