using System;
using System.Drawing;
using System.Runtime.Caching;
using VideoNotifications.Extensions;
using VideoNotifications.Settings;
using IO = System.IO;

namespace VideoNotifications.Database {

    /// <summary>
    /// 
    /// </summary>
    internal abstract class ImageFile {

        private static ObjectCache _ImageCache = MemoryCache.Default;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        public static bool Exists(string path) => IO.File.Exists(path);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="type"></param>
        public static bool Exists(string id, Types.ImageType type) => Exists(IO.Path.Combine(type.Path(), id));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="id"></param>
        /// <param name="type"></param>
        public static void Insert(string url, string id, Types.ImageType type) {
            string path = IO.Path.Combine(type.Path(), id);

            try {
                if (!Exists(path)) {
                    Utilities.NetworkUtils.DownloadFile(url, path);

                    LoggingManager.Log.Info($"'{id}' was inserted (downloaded to '{path}').");
                }
            } catch (Exception ex) {
                LoggingManager.Log.Error(ex, $"Failed to download/insert '{url}' to '{path}'.");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Image Get(string id, Types.ImageType type) {
            string path = IO.Path.Combine(type.Path(), id);

            try {
                if (_ImageCache.Contains(path)) { return (Image)_ImageCache.Get(path); }

                if (Exists(path)) {
                    Image image;
                    using (Bitmap bitmap = new Bitmap(IO.File.Open(path, IO.FileMode.Open, IO.FileAccess.Read, IO.FileShare.Read))) {
                        image = new Bitmap(bitmap);
                    }
                    _ImageCache.Set(path, image, new CacheItemPolicy() { SlidingExpiration = TimeSpan.FromMinutes(SettingsManager.Configuration.ImageCacheLifetime) });

                    return image;
                }

                return Properties.Resources.VideoNotificationsPNG;
            } catch (Exception ex) {
                LoggingManager.Log.Error(ex, $"Failed to open '{path}'.");
                return Properties.Resources.VideoNotificationsPNG;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="type"></param>
        public static void Delete(string id, Types.ImageType type) {
            string path = IO.Path.Combine(type.Path(), id);

            try {
                if (Exists(path)) {
                    IO.File.Delete(path);

                    LoggingManager.Log.Info($"'{path}' was deleted.");
                }
            } catch (Exception ex) {
                LoggingManager.Log.Error(ex, $"Failed to delete '{path}'.");
            }
        }

    }

}
