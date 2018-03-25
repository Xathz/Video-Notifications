using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using VideoNotifications.Database;
using VideoNotifications.Database.CollectionType;
using VideoNotifications.Forms;
using VideoNotifications.Settings;
using VideoNotifications.Utilities;
using VideoNotifications.YouTube;

namespace VideoNotifications {

    public partial class MainForm : Form {

        private bool _Close = false;

        public MainForm() {
            InitializeComponent();

            // The icon is a derivative of (https://www.flaticon.com/free-icon/notification_402727)
            // By Smashicons (https://www.flaticon.com/authors/smashicons, https://smashicons.com)
            // Creative Commons BY 3.0 (http://creativecommons.org/licenses/by/3.0/)
            Icon = (SettingsManager.Configuration.UseLightWindowIcon) ? Properties.Resources.VideoNotificationsLightIcon : Properties.Resources.VideoNotificationsIcon;
            TrayNotifyIcon.Icon = (SettingsManager.Configuration.UseLightTrayIcon) ? Properties.Resources.VideoNotificationsLightIcon : Properties.Resources.VideoNotificationsIcon;

            // Add tool strip label to the right-click context menu
            ToolStripLabel toolStripLabelTitle = new ToolStripLabel() { Text = "Video Notifications for YouTube" };
            toolStripLabelTitle.Font = new Font(toolStripLabelTitle.Font, FontStyle.Bold);
            TrayContextMenuStrip.Items.Insert(0, toolStripLabelTitle);

            ToolStripLabel toolStripLabelInfo = new ToolStripLabel() {
                TextAlign = ContentAlignment.MiddleLeft,
                Text = $"v{Version.Parse(Application.ProductVersion).ToString(3)}, by {Application.CompanyName}{Environment.NewLine}Click tray icon to show window",
                ForeColor = SystemColors.ControlDarkDark
            };
            TrayContextMenuStrip.Items.Insert(1, toolStripLabelInfo);

            // Process settings
            PauseNotificationsMenuItem.Checked = SettingsManager.Configuration.PauseNotifications;
            MinimizeToTrayMenuItem.Checked = SettingsManager.Configuration.MinimizeToTray;
            LightIconMenuItem.Checked = SettingsManager.Configuration.UseLightTrayIcon;
            YouTubeCheckTimer.Interval = (SettingsManager.Configuration.CheckForNewVideosEveryMinutes * 60000);

            if (SettingsManager.Configuration.MainWindow.PositionSet()) {
                Location = new Point(SettingsManager.Configuration.MainWindow.X, SettingsManager.Configuration.MainWindow.Y);
            } else {
                CenterToScreen();
            }
        }

        private void MainForm_Load(object sender, EventArgs e) {
            AddAllChannels();

            TrayNotifyIcon.Visible = true;
            YouTubeCheckTimer.Start();

            ProcessUtils.SetForegroundWindow(Handle.ToInt32());
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e) {
            if (!_Close) {
                e.Cancel = true;
                ShowInTaskbar = false;
                Hide();
                return;
            }

            TrayNotifyIcon.Visible = false;
            SettingsManager.Save();
            LoggingManager.Log.Info("Exiting application.");
            LoggingManager.Flush();
        }

        private void MainForm_LocationChanged(object sender, EventArgs e) {
            if (WindowState == FormWindowState.Normal) {
                SettingsManager.Configuration.MainWindow.X = Location.X;
                SettingsManager.Configuration.MainWindow.Y = Location.Y;
                SettingsManager.Configuration.MainWindow.Width = 800;
                SettingsManager.Configuration.MainWindow.Height = 600;
            }
        }

        private void MainForm_Resize(object sender, EventArgs e) {
            if (SettingsManager.Configuration.MinimizeToTray) {
                if (WindowState == FormWindowState.Normal) {
                    ShowInTaskbar = true;
                } else if (WindowState == FormWindowState.Minimized) {
                    ShowInTaskbar = false;
                }
            }
        }

        private void ChannelsListView_SelectedIndexChanged(object sender, EventArgs e) {
            if (ChannelsListView.SelectedItems.Count == 1) {
                YouTubeChannel selectedChannel = (YouTubeChannel)ChannelsListView.SelectedItems[0].Tag;

                Image imageFaded = ImageUtils.SetImageOpacity(Files.GetThumbnail(selectedChannel.ChannelID), 0.16f);
                BackgroundImageLayout = (imageFaded.Size.Height < Size.Height) ? ImageLayout.Stretch : ImageLayout.Center;
                BackgroundImage = imageFaded;

                AddChannelVideos(selectedChannel.ChannelID);

                if (VideosListView.Items.Count > 0) { VideoStatusPanel.Visible = true; }
            } else {
                ChannelsListViewChanged();
            }
        }

        private void VideosListView_SelectedIndexChanged(object sender, EventArgs e) {
            if (VideosListView.SelectedItems.Count == 1) {
                YouTubeVideo selectedVideo = (YouTubeVideo)VideosListView.SelectedItems[0].Tag;
                string openVideoTip = $"Open video in browser.{Environment.NewLine}{selectedVideo.URL}";

                VideoPictureBox.Image = Files.GetThumbnail(selectedVideo.VideoID);
                VideoPictureBox.Visible = true;
                GeneralToolTip.SetToolTip(VideoPictureBox, openVideoTip);
                VideoDescriptionLabel.Text = StringUtils.FormatVideoDescription(selectedVideo.Description);

                VideoLinkLabel.Tag = selectedVideo.URL;
                VideoLinkLabel.Text = selectedVideo.Title;
                GeneralToolTip.SetToolTip(VideoLinkLabel, openVideoTip);
                PostedLabel.Text = selectedVideo.Posted.Value.ToString();
                DurationLabel.Text = TimeSpanUtils.ConvertDuration(selectedVideo.Duration);

                VideoInfoPanel.Visible = true;

                NotificationForm newNotification = new NotificationForm(selectedVideo);
                newNotification.Show();
            } else {
                VideosListViewChanged();
            }
        }

        private void WatchedColorLabel_Click(object sender, EventArgs e) => SetStatus(Status.Watched);

        private void UnwatchedColorLabel_Click(object sender, EventArgs e) => SetStatus(Status.Unwatched);

        private void DismissedColorLabel_Click(object sender, EventArgs e) => SetStatus(Status.Dismissed);

        private void IgnoredColorLabel_Click(object sender, EventArgs e) => SetStatus(Status.Ignored);

        private void VideoPictureBox_Click(object sender, EventArgs e) => VideoURLLinkLabel_LinkClicked(new object(), null);

        private void VideoURLLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            if (VideosListView.SelectedItems.Count == 1) {
                ProcessUtils.Start((string)VideoLinkLabel.Tag);
            }
        }

        private void TrayNotifyIcon_MouseClick(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Left) {
                WindowState = FormWindowState.Normal;
                ShowInTaskbar = true;
                Show();
            }
        }

        private void WebsiteMenuItem_Click(object sender, EventArgs e) => ProcessUtils.Start("https://github.com/Xathz/Video-Notifications");

        private void AddChannelMenuItem_Click(object sender, EventArgs e) {
            AddChannelForm newAddChannelForm = new AddChannelForm();
            newAddChannelForm.Show();
        }

        private void DeleteChannelMenuItem_Click(object sender, EventArgs e) {
            DeleteChannelForm newDeleteChannelForm = new DeleteChannelForm();
            newDeleteChannelForm.Show();
        }

        private void PauseNotificationsMenuItem_CheckedChanged(object sender, EventArgs e) => SettingsManager.Configuration.PauseNotifications = PauseNotificationsMenuItem.Checked;

        private void MinimizeToTrayMenuItem_CheckedChanged(object sender, EventArgs e) {
            SettingsManager.Configuration.MinimizeToTray = MinimizeToTrayMenuItem.Checked;
            if (WindowState == FormWindowState.Minimized) {
                ShowInTaskbar = false;
            }
        }

        private void LightIconMenuItem_CheckedChanged(object sender, EventArgs e) {
            SettingsManager.Configuration.UseLightTrayIcon = LightIconMenuItem.Checked;
            TrayNotifyIcon.Icon = (SettingsManager.Configuration.UseLightTrayIcon) ? Properties.Resources.VideoNotificationsLightIcon : Properties.Resources.VideoNotificationsIcon;
        }

        private void ExitMenuItem_Click(object sender, EventArgs e) {
            _Close = true;
            Close();
        }

        private void YouTubeCheckTimer_Tick(object sender, EventArgs e) => YouTubeCheckWorker.RunWorkerAsync();

        private void YouTubeCheckWorker_DoWork(object sender, DoWorkEventArgs e) {
            try {
                if (string.IsNullOrWhiteSpace(SettingsManager.Configuration.YouTubeAPIKey)) {
                    LoggingManager.Log.Error(new ArgumentNullException(nameof(SettingsManager.Configuration.YouTubeAPIKey)), "The YouTubeAPIKey can not be: null, blank, or just whitespace. Aborted YouTubeCheckWorker.");
                    return;
                }

                foreach (YouTubeChannel channel in Channels.GetAll()) {
                    ChannelVideos channelVideos = new ChannelVideos(channel.ChannelID);
                    List<string> videosToGetInfo = new List<string>();
                    foreach (string videoID in channelVideos.VideosIDs) {
                        if (!Videos.Exists(videoID)) {
                            videosToGetInfo.Add(videoID);
                        }
                    }

                    VideoInfoBulk videoInfoBulk = new VideoInfoBulk(videosToGetInfo);
                    foreach (YouTubeVideo video in videoInfoBulk.Videos) {
                        Videos.Insert(video);
                        Files.StoreImage($"{video.VideoID}-thumbnail", NetworkUtils.DownloadFileToMemoryStream(video.ThumbnailURL));
                    }
                }

                e.Result = true;
            } catch (Exception ex) {
                LoggingManager.Log.Error(ex, "There was an error while checking for new videos.");
                e.Result = false;
            }
        }

        private void YouTubeCheckWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
            if (SettingsManager.Configuration.PauseNotifications) { return; }
            IEnumerable<YouTubeVideo> unwatchedVideos = Videos.GetAllUnwatched();

            if (unwatchedVideos.Count() > 0) {
                if ((bool)e.Result == false) {
                    LoggingManager.Log.Info($"There was an error checking for new videos, but there were {unwatchedVideos.Count()} unwatched videos found in the database.");
                } else {
                    LoggingManager.Log.Info($"{unwatchedVideos.Count()} unwatched videos were found.");
                }

                foreach (YouTubeVideo video in unwatchedVideos) {
                    NotificationForm newNotification = new NotificationForm(video);
                    newNotification.Show();
                }
            }
        }

    }

}
