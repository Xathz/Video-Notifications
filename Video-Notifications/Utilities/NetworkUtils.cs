using System;
using System.IO;
using System.Net;

namespace VideoNotifications.Utilities {

    internal static class NetworkUtils {

        /// <summary>
        /// Download a file to a <see cref="MemoryStream"/>.
        /// </summary>
        /// <param name="url">URL of the file to download.</param>
        public static MemoryStream DownloadFileToMemoryStream(string url) {
            try {
                WebClient webClient = new WebClient();
                MemoryStream memoryStream = new MemoryStream(webClient.DownloadData(url));
                return memoryStream;
            } catch (Exception ex) {
                LoggingManager.Log.Error(ex, $"Failed to download '{url}'.");
                return null;
            }
        }

    }

}
