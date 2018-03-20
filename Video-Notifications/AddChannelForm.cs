using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using VideoNotifications.Database;
using VideoNotifications.Database.CollectionType;
using VideoNotifications.Settings;
using VideoNotifications.Utilities;
using VideoNotifications.YouTube;

namespace VideoNotifications {

    public partial class AddChannelForm : Form {

        public AddChannelForm() {
            InitializeComponent();

            // The icon is a derivative of (https://www.flaticon.com/free-icon/notification_402727)
            // By Smashicons (https://www.flaticon.com/authors/smashicons, https://smashicons.com)
            // Creative Commons BY 3.0 (http://creativecommons.org/licenses/by/3.0/)
            Icon = (SettingsManager.Configuration.UseLightWindowIcon) ? Properties.Resources.VideoNotificationsLightIcon : Properties.Resources.VideoNotificationsIcon;

            if (SettingsManager.Configuration.AddChannelWindow.PositionSet()) {
                Location = new Point(SettingsManager.Configuration.AddChannelWindow.X, SettingsManager.Configuration.AddChannelWindow.Y);
            } else {
                CenterToScreen();
            }
        }

        private void AddChannelForm_Load(object sender, EventArgs e) {
            if (Program.GetOpenForm(this) != null) {
                Program.GetOpenForm(this).BringToFront();
                Dispose();
            } else {
                Program.OpenForms.Add(this);
            }
        }

        private void AddChannelForm_FormClosing(object sender, FormClosingEventArgs e) => SettingsManager.Save();

        private void AddChannelForm_FormClosed(object sender, FormClosedEventArgs e) => Program.OpenForms.Remove(this);

        private void AddChannelForm_LocationChanged(object sender, EventArgs e) {
            if (WindowState == FormWindowState.Normal) {
                SettingsManager.Configuration.AddChannelWindow.X = Location.X;
                SettingsManager.Configuration.AddChannelWindow.Y = Location.Y;
                SettingsManager.Configuration.AddChannelWindow.Width = 437;
                SettingsManager.Configuration.DeleteChannelWindow.Height = 539;
            }
        }

        private void SearchButton_Click(object sender, EventArgs e) {
            DisableControls("Searching...", true);

            SearchForChannels channelSearch = new SearchForChannels(SearchTextBox.Text);
            foreach (YouTubeChannel channel in channelSearch.Channels) {
                AddChannelToListView(channel);
            }

            EnableControls();
        }

        private void ChannelsListView_MouseDoubleClick(object sender, MouseEventArgs e) {
            ListViewItem clickedItem = ChannelsListView.GetItemAt(e.X, e.Y);
            if (clickedItem != null) {
                YouTubeChannel clickedChannel = (YouTubeChannel)clickedItem.Tag;
                if (Channels.Exists(clickedChannel.ChannelID)) {
                    MessageBox.Show($"{clickedChannel.Title} ({clickedChannel.ChannelID}) already exists.", "Channel Exists", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }

                if (MessageBox.Show($"Really add {clickedChannel.Title}?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes) {
                    try {
                        DisableControls("Processing...", false);

                        ChannelInfo newChannel = new ChannelInfo(clickedChannel.ChannelID);
                        Channels.Upsert(newChannel.Info);

                        Files.StoreImage($"{newChannel.Info.ChannelID}-thumbnail", NetworkUtils.DownloadFileToMemoryStream(newChannel.Info.ThumbnailURL));
                        Files.StoreImage($"{newChannel.Info.ChannelID}-banner", NetworkUtils.DownloadFileToMemoryStream(newChannel.Info.BannerURL));

                        List<YouTubeVideo> videos = newChannel.ChannelVideos.VideosInfoBulk.Videos;
                        foreach (YouTubeVideo video in videos) {
                            video.Status = SettingsManager.Configuration.NewChannelDefaultVideoStatus;
                            Videos.Insert(video);
                            Files.StoreImage($"{video.VideoID}-thumbnail", NetworkUtils.DownloadFileToMemoryStream(video.ThumbnailURL));
                        }

                        Program.StaticMainForm.AddAllChannels();
                        LoggingManager.Log.Info($"Added channel: {newChannel.Info.Title} ({newChannel.Info.ChannelID})");
                        EnableControls();
                        MessageBox.Show($"{newChannel.Info.Title} was added along with {videos.Count} videos.", "Channel Added", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    } catch (Exception ex) {
                        LoggingManager.Log.Error(ex, $"Failed to add a channel. Channel: {clickedChannel.Title} ({clickedChannel.ChannelID})");
                        EnableControls();
                    }
                }
            }
        }

        /// <summary>
        /// Add a single channel to <see cref="ChannelsListView"/>.
        /// </summary>
        /// <param name="channel">Channel to add.</param>
        private void AddChannelToListView(YouTubeChannel channel) {
            if (!ChannelsImageList.Images.ContainsKey(channel.ChannelID)) {
                MemoryStream stream = NetworkUtils.DownloadFileToMemoryStream(channel.ThumbnailURL);
                Image image = Image.FromStream(stream);
                Image resizedImage = ImageUtils.ResizeImage(image, 24, 24);
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
        /// Enable controls after something is done loading/processing.
        /// </summary>
        private void EnableControls() {
            LoadingLabel.Visible = false;
            ChannelsListView.EndUpdate();
            ChannelsListView.Enabled = true;
            SearchPanel.Enabled = true;
        }

        /// <summary>
        /// Disable controls before something is loading/processing.
        /// </summary>
        /// <param name="loadingMessage">Message to show in <see cref="LoadingLabel.Text"/></param>
        /// <param name="clearChannelsListView">Clear all items in <see cref="ChannelsListView.Items"/></param>
        private void DisableControls(string loadingMessage, bool clearChannelsListView) {
            SearchPanel.Enabled = false;
            if (clearChannelsListView) {
                ChannelsListView.Items.Clear();
                ChannelsImageList.Images.Clear();
            }
            ChannelsListView.BeginUpdate();
            ChannelsListView.Enabled = false;
            LoadingLabel.Text = loadingMessage;
            LoadingLabel.Visible = true;
            Application.DoEvents();
        }

    }

}
