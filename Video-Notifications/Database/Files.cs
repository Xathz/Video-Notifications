using System;
using System.Drawing;
using System.IO;
using LiteDB;

namespace VideoNotifications.Database {

    /// <summary>
    /// Options for managing files in the database.
    /// </summary>
    internal abstract class Files : DatabaseBase {

        /// <summary>
        /// Stores a image in the database.
        /// </summary>
        /// <param name="imageID">Image file ID.</param>
        /// <param name="stream"><see cref="IO.Stream"/> of the image.</param>
        public static void StoreImage(string imageID, Stream stream) {
            try {
                if (!_Database.FileStorage.Exists(imageID)) {
                    if (stream != null) {
                        _Database.FileStorage.Upload(imageID, $"{imageID}.jpg", stream);
                    }
                }
            } catch (Exception ex) {
                LoggingManager.Log.Error(ex, $"Failed to store image: {imageID}.");
            }
        }

        /// <summary>
        /// Get an image from the database by ID.
        /// </summary>
        /// <param name="imageID">Image file ID.</param>
        public static Image GetImage(string imageID) {
            try {
                if (_Database.FileStorage.Exists(imageID)) {
                    LiteFileInfo fileInfo = _Database.FileStorage.FindById(imageID);

                    Image image;
                    using (Bitmap bmpTemp = new Bitmap(fileInfo.OpenRead())) {
                        image = new Bitmap(bmpTemp);
                    }
                    return image;
                }

                return Properties.Resources.VideoNotificationsPNG;
            } catch (Exception ex) {
                LoggingManager.Log.Error(ex, $"Failed to get image: {imageID}.");
                return Properties.Resources.VideoNotificationsPNG;
            }
        }

        /// <summary>
        /// Get a channel or video thumbnail from the database by ID.
        /// </summary>
        /// <param name="imageID">Image file ID.</param>
        public static Image GetThumbnail(string imageID) => GetImage($"{imageID}-thumbnail");

        /// <summary>
        /// Get a channel banner from the database by ID.
        /// </summary>
        /// <param name="imageID">Image file ID.</param>
        public static Image GetBanner(string imageID) => GetImage($"{imageID}-banner");

        /// <summary>
        /// Deletes an image from the database by ID.
        /// </summary>
        /// <param name="imageID">Image to delete.</param>
        public static void DeleteImage(string imageID) {
            try {
                if (_Database.FileStorage.Exists($"{imageID}-thumbnail")) { _Database.FileStorage.Delete($"{imageID}-thumbnail"); }
            } catch (Exception ex) {
                LoggingManager.Log.Error(ex, $"Failed to delete image: {imageID}-thumbnail.");
            }
            try {
                if (_Database.FileStorage.Exists($"{imageID}-banner")) { _Database.FileStorage.Delete($"{imageID}-banner"); }
            } catch (Exception ex) {
                LoggingManager.Log.Error(ex, $"Failed to delete image: {imageID}-banner.");
            }
        }

    }

}
