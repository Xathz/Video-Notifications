using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Humanizer;
using VideoNotifications.Database;
using VideoNotifications.Database.CollectionType;
using VideoNotifications.Utilities;

namespace VideoNotifications {

    public partial class MainForm {

        /// <summary>
        /// Add all channels to <see cref="ChannelsListView"/> from <see cref="Channels.GetAll()"/>.
        /// Clears all existing channels already in the listview (if any).
        /// </summary>
        public void AddAllChannels() {
            ChannelsListView.BeginUpdate();
            VideosListView.BeginUpdate();

            ChannelsListView.Items.Clear();
            VideosListView.Items.Clear();

            ChannelsListViewChanged();
            foreach (YouTubeChannel channel in Channels.GetAll()) {
                AddChannelToListView(channel);
            }

            ChannelsListView.EndUpdate();
            VideosListView.EndUpdate();
        }

        /// <summary>
        /// Add all videos to <see cref="VideosListView"/> from <see cref="Channels.GetAllVideos(string)"/>.
        /// Clears all existing videos already in the listview (if any).
        /// </summary>
        /// <param name="channelID">Channel ID to add the videos from.</param>
        public void AddChannelVideos(string channelID) {
            VideosListView.BeginUpdate();
            VideosListView.Items.Clear();

            IEnumerable<YouTubeVideo> videos = Channels.GetAllVideos(channelID).OrderByDescending(v => v.Posted);
            foreach (YouTubeVideo video in videos) {
                AddVideoToListView(video);
            }

            VideosListView.Columns[PostedColumn.Index].TextAlign = HorizontalAlignment.Right;
            VideosListView.EndUpdate();
        }

        /// <summary>
        /// Add a single channel to <see cref="ChannelsListView"/>.
        /// </summary>
        /// <param name="channel">Channel to add.</param>
        private void AddChannelToListView(YouTubeChannel channel) {
            if (!ChannelsImageList.Images.ContainsKey(channel.ChannelID)) {
                Image resizedImage = ImageUtils.ResizeImage(Files.GetThumbnail(channel.ChannelID), 24, 24);
                ChannelsImageList.Images.Add(channel.ChannelID, resizedImage);
            }

            ListViewItem channelItem = new ListViewItem {
                Name = channel.ChannelID,
                Tag = channel,
                ImageKey = channel.ChannelID,
                Font = new Font("Segoe UI Semibold", 10),
                Text = $" {channel.Title}"
            };

            ChannelsListView.Items.Add(channelItem);
        }

        /// <summary>
        /// Add a single video to <see cref="VideosListView"/>.
        /// </summary>
        /// <param name="video">Video to add.</param>
        private void AddVideoToListView(YouTubeVideo video) {
            ListViewItem videoItem = new ListViewItem {
                Name = video.VideoID,
                Tag = video,
                Text = $"{video.Title}",
                BackColor = DisplayStatusColor(video.Status)
            };

            ListViewItem.ListViewSubItem postedDateItem = new ListViewItem.ListViewSubItem {
                Text = video.Posted.Value.ToUniversalTime().Humanize(),
            };
            videoItem.SubItems.Add(postedDateItem);

            VideosListView.Items.Add(videoItem);
        }

        /// <summary>
        /// Updates a video in <see cref="VideosListView"/> from <see cref="Videos"/> data.
        /// </summary>
        /// <param name="video">Video to update.</param>
        private void UpdateVideoInListView(YouTubeVideo video) {
            ListViewItem item = VideosListView.Items.Find(video.VideoID, false).FirstOrDefault();
            if (item != null) {
                YouTubeVideo updatedVideo = Videos.GetByID(video.VideoID);
                item.Name = updatedVideo.VideoID;
                item.Tag = updatedVideo;
                item.Text = updatedVideo.Title;
                item.BackColor = DisplayStatusColor(updatedVideo.Status);
                item.SubItems[1].Text = updatedVideo.Posted.Value.ToUniversalTime().Humanize();
            }
        }

        /// <summary>
        /// Set the selected or all video(s) status.
        /// </summary>
        /// <param name="status">Status to set the video to.</param>
        private void SetStatus(Status status) {
            if (ModifierKeys == Keys.Shift) {
                if (MessageBox.Show($"Really mark all videos as {status.ToString()}?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes) {
                    VideosListView.BeginUpdate();
                    foreach (ListViewItem item in VideosListView.Items) {
                        if (((YouTubeVideo)item.Tag).Status != status) {
                            Videos.SetStatus(item.Name, status);
                            UpdateVideoInListView((YouTubeVideo)item.Tag);
                        }
                    }
                    VideosListView.EndUpdate();
                }
            } else {
                if (VideosListView.SelectedItems.Count == 1) {
                    ListViewItem selectedItem = VideosListView.SelectedItems[0];
                    Videos.SetStatus(selectedItem.Name, status);
                    UpdateVideoInListView((YouTubeVideo)selectedItem.Tag);
                }
            }
        }

        /// <summary>
        /// The color based on the video <see cref="Status"/>. Value will be a <see cref="Color"/> or <see cref="SystemColors"/> type.
        /// </summary>
        /// <param name="status">Status to get the color for.</param>
        private dynamic DisplayStatusColor(Status status) {
            switch (status) {
                case Status.Watched:
                    return Color.LightGreen;
                case Status.Unwatched:
                    return SystemColors.Window;
                case Status.Dismissed:
                    return Color.LemonChiffon;
                case Status.Ignored:
                    return Color.Gainsboro;
                default:
                    return SystemColors.Window;
            }
        }

        /// <summary>
        /// When selection in <see cref="ChannelsListView"/> changes.
        /// </summary>
        private void ChannelsListViewChanged() {
            VideoStatusPanel.Visible = false;
            VideosListView.Items.Clear();
            BackgroundImage = null;
            VideosListViewChanged();
        }

        /// <summary>
        /// When selection in <see cref="VideosListView"/> changes.
        /// </summary>
        private void VideosListViewChanged() {
            VideoInfoPanel.Visible = false;
            VideoPictureBox.Visible = false;
            VideoDescriptionLabel.Text = null;
        }

    }

}
