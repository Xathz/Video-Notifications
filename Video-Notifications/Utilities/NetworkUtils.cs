using System;
using System.IO;
using System.Net;

namespace VideoNotifications.Utilities {

    internal static class NetworkUtils {

        private static WebClient _WebClient = new WebClient();

        /// <summary>
        /// Download a file to a <see cref="MemoryStream"/>.
        /// </summary>
        /// <param name="url">URL of the file to download.</param>
        public static MemoryStream DownloadFileToMemoryStream(string url) {
            try {
                MemoryStream memoryStream = new MemoryStream(_WebClient.DownloadData(url));
                return memoryStream;
            } catch (Exception ex) {
                LoggingManager.Log.Error(ex, $"Failed to download '{url}'.");
                return null;
            }
        }

        /// <summary>
        /// Download a file.
        /// </summary>
        /// <param name="url">URL of the file to download.</param>
        /// <param name="path">Local path to save the file.</param>
        public static void DownloadFile(string url, string path) {
            try {
                _WebClient.DownloadFile(url, path);
                LoggingManager.Log.Info($"Downloaded '{url}' to '{path}'.");
            } catch (Exception ex) {
                LoggingManager.Log.Error(ex, $"Failed to download '{url}'.");
            }
        }

    }

}
