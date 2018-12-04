using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Humanizer;
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
            foreach (Database.Types.Channel channel in Database.Channels.GetAll()) {
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

            IEnumerable<Database.Types.Video> videos = Database.Channels.GetAllVideos(channelID).OrderByDescending(v => v.Posted);
            foreach (Database.Types.Video video in videos) {
                AddVideoToListView(video);
            }

            VideosListView.Columns[PostedColumn.Index].TextAlign = HorizontalAlignment.Right;
            VideosListView.EndUpdate();
        }

        /// <summary>
        /// Add a single channel to <see cref="ChannelsListView"/>.
        /// </summary>
        /// <param name="channel">Channel to add.</param>
        private void AddChannelToListView(Database.Types.Channel channel) {
            if (!ChannelsImageList.Images.ContainsKey(channel.ID)) {
                Image resizedImage = ImageUtils.ResizeImage((Image)Database.ImageFile.Get(channel.ID, ImageType.ChannelIcon), 24, 24);
                ChannelsImageList.Images.Add(channel.ID, resizedImage);
            }

            ListViewItem channelItem = new ListViewItem {
                Name = channel.ID,
                Tag = channel,
                ImageKey = channel.ID,
                Font = new Font("Segoe UI Semibold", 10),
                Text = $" {channel.Title}"
            };

            ChannelsListView.Items.Add(channelItem);
        }

        /// <summary>
        /// Add a single video to <see cref="VideosListView"/>.
        /// </summary>
        /// <param name="video">Video to add.</param>
        private void AddVideoToListView(Database.Types.Video video) {
            ListViewItem videoItem = new ListViewItem {
                Name = video.ID,
                Tag = video,
                Text = $"{video.Title}",
                BackColor = DisplayStatusColor(video.WatchStatus)
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
        private void UpdateVideoInListView(Database.Types.Video video) {
            ListViewItem item = VideosListView.Items.Find(video.ID, false).FirstOrDefault();
            if (item != null) {
                Database.Types.Video updatedVideo = Database.Videos.GetByID(video.ID);
                item.Name = updatedVideo.ID;
                item.Tag = updatedVideo;
                item.Text = updatedVideo.Title;
                item.BackColor = DisplayStatusColor(updatedVideo.WatchStatus);
                item.SubItems[1].Text = updatedVideo.Posted.Value.ToUniversalTime().Humanize();
            }
        }

        /// <summary>
        /// Set the selected or all video(s) status.
        /// </summary>
        /// <param name="status">Status to set the video to.</param>
        private void SetStatus(WatchStatus status) {
            if (ModifierKeys == Keys.Shift) {
                if (MessageBox.Show($"Really mark all videos as {status.ToString()}?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes) {
                    VideosListView.BeginUpdate();
                    foreach (ListViewItem item in VideosListView.Items) {
                        if (((Database.Types.Video)item.Tag).WatchStatus != status) {
                            Database.Videos.SetStatus(item.Name, status);
                            UpdateVideoInListView((Database.Types.Video)item.Tag);
                        }
                    }
                    VideosListView.EndUpdate();
                }
            } else {
                if (VideosListView.SelectedItems.Count == 1) {
                    ListViewItem selectedItem = VideosListView.SelectedItems[0];
                    Database.Videos.SetStatus(selectedItem.Name, status);
                    UpdateVideoInListView((Database.Types.Video)selectedItem.Tag);
                }
            }
        }

        /// <summary>
        /// The color based on the video <see cref="WatchStatus"/>. Value will be a <see cref="Color"/> or <see cref="SystemColors"/> type.
        /// </summary>
        /// <param name="status">Status to get the color for.</param>
        private dynamic DisplayStatusColor(WatchStatus status) {
            switch (status) {
                case WatchStatus.Watched:
                    return Color.LightGreen;
                case WatchStatus.Unwatched:
                    return SystemColors.Window;
                case WatchStatus.Dismissed:
                    return Color.LemonChiffon;
                case WatchStatus.Ignored:
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
