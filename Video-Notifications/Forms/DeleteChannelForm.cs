using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using VideoNotifications.Database;
using VideoNotifications.Database.Types;
using VideoNotifications.Settings;
using VideoNotifications.Utilities;

namespace VideoNotifications.Forms {

    public partial class DeleteChannelForm : Form {

        public DeleteChannelForm() {
            InitializeComponent();

            // The icon is a derivative of (https://www.flaticon.com/free-icon/notification_402727)
            // By Smashicons (https://www.flaticon.com/authors/smashicons, https://smashicons.com)
            // Creative Commons BY 3.0 (http://creativecommons.org/licenses/by/3.0/)
            Icon = (SettingsManager.Configuration.UseLightWindowIcon) ? Properties.Resources.VideoNotificationsLightIcon : Properties.Resources.VideoNotificationsIcon;

            if (SettingsManager.Configuration.DeleteChannelWindow.PositionSet()) {
                Location = new Point(SettingsManager.Configuration.DeleteChannelWindow.X, SettingsManager.Configuration.DeleteChannelWindow.Y);
            } else {
                CenterToScreen();
            }
        }

        private void DeleteChannelForm_Load(object sender, EventArgs e) {
            if (FormsManager.GetOpenForm(this) != null) {
                FormsManager.GetOpenForm(this).BringToFront();
                Dispose();
            } else {
                FormsManager.OpenForms.Add(this);
            }

            AddAllChannels();
        }

        private void DeleteChannelForm_FormClosing(object sender, FormClosingEventArgs e) => SettingsManager.Save();

        private void DeleteChannelForm_FormClosed(object sender, FormClosedEventArgs e) => FormsManager.OpenForms.Remove(this);

        private void DeleteChannelForm_LocationChanged(object sender, EventArgs e) {
            if (WindowState == FormWindowState.Normal) {
                SettingsManager.Configuration.DeleteChannelWindow.X = Location.X;
                SettingsManager.Configuration.DeleteChannelWindow.Y = Location.Y;
                SettingsManager.Configuration.DeleteChannelWindow.Width = 295;
                SettingsManager.Configuration.DeleteChannelWindow.Height = 405;
            }
        }

        private void ChannelsListView_MouseDoubleClick(object sender, MouseEventArgs e) {
            ListViewItem clickedItem = ChannelsListView.GetItemAt(e.X, e.Y);
            if (clickedItem != null) {
                Channel channel = (Channel)clickedItem.Tag;
                if (MessageBox.Show($"Really delete {channel.Title}?{Environment.NewLine}{Environment.NewLine}You will no longer receive notifications and the videos from{Environment.NewLine}this channel will be deleted from the database.", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes) {
                    try {
                        IEnumerable<Video> channelVideos = Channels.GetAllVideos(channel.ID);
                        int channelVideosTotal = channelVideos.Count();

                        foreach (Video video in channelVideos) {
                            Videos.Delete(video);
                            Files.DeleteImage(video.ID);
                        }
                        Channels.Delete(channel);
                        Files.DeleteImage(channel.ID);

                        AddAllChannels();
                        FormsManager.StaticMainForm.AddAllChannels();
                        LoggingManager.Log.Info($"Deleted channel: '{channel.Title}' ({channel.ID}).");
                        MessageBox.Show($"{channel.Title} was deleted and {channelVideosTotal} videos were removed from the database.", "Channel Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    } catch (Exception ex) {
                        LoggingManager.Log.Error(ex, $"Failed to delete a channel. Channel: '{channel.Title}' ({channel.ID}).");
                    }
                }
            }
        }

        /// <summary>
        /// Add all channels to <see cref="ChannelsListView"/> from <see cref="Channels.GetAll()"/>.
        /// Clears all existing channels already in the listview (if any).
        /// </summary>
        private void AddAllChannels() {
            ChannelsListView.BeginUpdate();
            ChannelsListView.Items.Clear();

            foreach (Channel channel in Channels.GetAll()) {
                AddChannelToListView(channel);
            }

            ChannelsListView.EndUpdate();
        }

        /// <summary>
        /// Add a single channel to <see cref="ChannelsListView"/>.
        /// </summary>
        /// <param name="channel">Channel to add.</param>
        private void AddChannelToListView(Channel channel) {
            if (!ChannelsImageList.Images.ContainsKey(channel.ID)) {
                Image resizedImage = ImageUtils.ResizeImage(Files.GetThumbnail(channel.ID), 24, 24);
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

    }

}
