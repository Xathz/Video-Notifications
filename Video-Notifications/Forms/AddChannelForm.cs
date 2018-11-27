using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
//using VideoNotifications.Database;
//using VideoNotifications.Database.CollectionType;
using VideoNotifications.Settings;
using VideoNotifications.Utilities;
//using VideoNotifications.YouTube;

namespace VideoNotifications.Forms {

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
            if (FormsManager.GetOpenForm(this) != null) {
                FormsManager.GetOpenForm(this).BringToFront();
                Dispose();
            } else {
                FormsManager.OpenForms.Add(this);
            }
        }

        private void AddChannelForm_FormClosing(object sender, FormClosingEventArgs e) => SettingsManager.Save();

        private void AddChannelForm_FormClosed(object sender, FormClosedEventArgs e) => FormsManager.OpenForms.Remove(this);

        private void AddChannelForm_LocationChanged(object sender, EventArgs e) {
            if (WindowState == FormWindowState.Normal) {
                SettingsManager.Configuration.AddChannelWindow.X = Location.X;
                SettingsManager.Configuration.AddChannelWindow.Y = Location.Y;
                SettingsManager.Configuration.AddChannelWindow.Width = 437;
                SettingsManager.Configuration.AddChannelWindow.Height = 539;
            }
        }

        private void SearchButton_Click(object sender, EventArgs e) {
            DisableControls("Searching...", true);

            YouTube.Channel channelSearch = new YouTube.Channel();
            foreach (Database.Types.Channel channel in channelSearch.Search(SearchTextBox.Text)) {
                AddChannelToListView(channel);
            }

            EnableControls();
        }

        private void ChannelsListView_MouseDoubleClick(object sender, MouseEventArgs e) {
            ListViewItem clickedItem = ChannelsListView.GetItemAt(e.X, e.Y);
            if (clickedItem != null) {
                Database.Types.Channel clickedChannel = (Database.Types.Channel)clickedItem.Tag;
                if (Database.Channels.Exists(clickedChannel.ID)) {
                    MessageBox.Show($"{clickedChannel.Title} ({clickedChannel.ID}) already exists.", "Channel Exists", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }

                if (MessageBox.Show($"Really add {clickedChannel.Title}?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes) {
                    try {
                        DisableControls("Processing...", false);

                        Database.Types.Channel channel = new YouTube.Channel().Info(clickedChannel.ID);
                        Database.Channels.Upsert(channel);

                        Database.Files.StoreImage($"{channel.ID}-thumbnail", NetworkUtils.DownloadFileToMemoryStream(channel.ThumbnailURL));
                        Database.Files.StoreImage($"{channel.ID}-banner", NetworkUtils.DownloadFileToMemoryStream(channel.BannerURL));

                        List<Database.Types.Video> videos = new YouTube.Channel().RecentVideos(channel.ID);
                        foreach (Database.Types.Video video in videos) {
                            video.WatchStatus = SettingsManager.Configuration.NewChannelDefaultVideoStatus;
                            Database.Videos.Insert(video);
                            Database.Files.StoreImage($"{video.ID}-thumbnail", NetworkUtils.DownloadFileToMemoryStream(video.ThumbnailURL));
                        }

                        FormsManager.StaticMainForm.AddAllChannels();
                        LoggingManager.Log.Info($"Added channel: '{channel.Title}' ({channel.ID}).");
                        EnableControls();
                        MessageBox.Show($"'{channel.Title}' was added along with {videos.Count} videos.", "Channel Added", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    } catch (Exception ex) {
                        LoggingManager.Log.Error(ex, $"Failed to add a channel. Channel: '{clickedChannel.Title}' ({clickedChannel.ID}).");
                        EnableControls();
                    }
                }
            }
        }

        /// <summary>
        /// Add a single channel to <see cref="ChannelsListView"/>.
        /// </summary>
        /// <param name="channel">Channel to add.</param>
        private void AddChannelToListView(Database.Types.Channel channel) {
            if (!ChannelsImageList.Images.ContainsKey(channel.ID)) {
                MemoryStream stream = NetworkUtils.DownloadFileToMemoryStream(channel.ThumbnailURL);
                Image image = Image.FromStream(stream);
                Image resizedImage = ImageUtils.ResizeImage(image, 24, 24);
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
