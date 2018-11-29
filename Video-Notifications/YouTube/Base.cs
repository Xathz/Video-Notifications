using System;
using System.Collections.Generic;
using System.Linq;
using Google.Apis.Services;
using VideoNotifications.Settings;
using API = Google.Apis.YouTube.v3;

namespace VideoNotifications.YouTube {

    internal abstract class Base {

        /// <summary>
        /// API base client information and setup.
        /// </summary>
        protected static BaseClientService.Initializer APIBaseClient = new BaseClientService.Initializer {
            ApiKey = string.IsNullOrWhiteSpace(SettingsManager.Configuration.YouTubeAPIKey) ? throw new ArgumentNullException(nameof(SettingsManager.Configuration.YouTubeAPIKey), "The YouTubeAPIKey can not be: null, blank, or just whitespace.") : SettingsManager.Configuration.YouTubeAPIKey,
            ApplicationName = Constants.ApplicationNameFormatted,
            GZipEnabled = true
        };

        /// <summary>
        /// YouTube API service.
        /// </summary>
        protected static API.YouTubeService APIService = new API.YouTubeService(APIBaseClient);

        /// <summary>
        /// Sort all thumbnails and returns the best one based on width.
        /// </summary>
        /// <param name="thumbnailDetails">The YouTube <see cref="ThumbnailDetails"/>.</param>
        protected string GetBestThumbnail(API.Data.ThumbnailDetails thumbnailDetails) {
            List<API.Data.Thumbnail> thumbnails = new List<API.Data.Thumbnail>();

            if (thumbnailDetails.Default__ != null) { thumbnails.Add(thumbnailDetails.Default__); }
            if (thumbnailDetails.Standard != null) { thumbnails.Add(thumbnailDetails.Standard); }
            if (thumbnailDetails.Medium != null) { thumbnails.Add(thumbnailDetails.Medium); }
            if (thumbnailDetails.High != null) { thumbnails.Add(thumbnailDetails.High); }
            if (thumbnailDetails.Maxres != null) { thumbnails.Add(thumbnailDetails.Maxres); }

            return thumbnails.OrderByDescending(t => t.Width).FirstOrDefault().Url;
        }

    }

}
