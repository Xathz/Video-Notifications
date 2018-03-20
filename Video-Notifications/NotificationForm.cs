using System;
using System.Drawing;
using System.Windows.Forms;
using Humanizer;
using VideoNotifications.Database;
using VideoNotifications.Database.CollectionType;
using VideoNotifications.Settings;
using VideoNotifications.Utilities;

namespace VideoNotifications {

    public partial class NotificationForm : Form {

        private int _Countdown = 2;
        private YouTubeVideo _Video { get; set; }
        private YouTubeChannel _Channel { get; set; }

        public NotificationForm() {
            InitializeComponent();
            SetIcon();

            if (SettingsManager.Configuration.NotificationWindow.PositionSet()) {
                Location = new Point(SettingsManager.Configuration.NotificationWindow.X, SettingsManager.Configuration.NotificationWindow.Y);
            } else {
                CenterToScreen();
            }
        }

        public NotificationForm(YouTubeVideo video) {
            InitializeComponent();
            SetIcon();

            if (SettingsManager.Configuration.NotificationWindow.PositionSet()) {
                Location = new Point(SettingsManager.Configuration.NotificationWindow.X, SettingsManager.Configuration.NotificationWindow.Y);
            } else {
                CenterToScreen();
            }

            _Video = video;
            _Channel = Channels.GetByID(_Video.ChannelID);

            Image channelImage = Files.GetThumbnail(_Video.ChannelID);
            Image channelImageResized = ImageUtils.ResizeImage(channelImage, 365, 365);
            Image channelImageFaded = ImageUtils.SetImageOpacity(channelImageResized, 0.16f);
            BackgroundImage = channelImageFaded;

            ChannelPictureBox.Image = channelImage;
            ChannelPictureBox.Tag = _Channel.URL;

            VideoPictureBox.Image = Files.GetThumbnail(_Video.VideoID);
            VideoPictureBox.Tag = _Video.URL;

            ChannelLabel.Text = $"A new video was posted by {_Channel.Title} {_Video.Posted.Value.Humanize()}.";
            DurationLabel.Text = TimeSpanUtils.ConvertDurationCompact(video.Duration);

            WatchedButton.Text = $"Open video && mark as watched (wait {(_Countdown + 1)}s)";
            DismissButton.Text = $"Dismiss notification (wait {(_Countdown + 1)}s)";

            ControlBox = false;
        }

        private void NotificationForm_Load(object sender, EventArgs e) {
            Program.OpenForms.Add(this);

            EnableControlsCountdownTimer.Start();
            EnableControlsTimer.Start();
        }

        private void NotificationForm_FormClosing(object sender, FormClosingEventArgs e) => SettingsManager.Save();

        private void NotificationForm_FormClosed(object sender, FormClosedEventArgs e) => Program.OpenForms.Remove(this);

        private void NotificationForm_LocationChanged(object sender, EventArgs e) {
            if (WindowState == FormWindowState.Normal) {
                SettingsManager.Configuration.NotificationWindow.X = Location.X;
                SettingsManager.Configuration.NotificationWindow.Y = Location.Y;
                SettingsManager.Configuration.NotificationWindow.Width = 729;
                SettingsManager.Configuration.NotificationWindow.Height = 199;
            }
        }

        private void EnableControlsTimer_Tick(object sender, EventArgs e) {
            EnableControlsCountdownTimer.Stop();
            EnableControlsTimer.Stop();

            WatchedButton.Text = "Open video && mark as watched";
            DismissButton.Text = "Dismiss notification";

            WatchedButton.Enabled = true;
            DismissButton.Enabled = true;
        }

        private void EnableControlsCountdownTimer_Tick(object sender, EventArgs e) {
            WatchedButton.Text = $"Open video && mark as watched (wait {_Countdown}s)";
            DismissButton.Text = $"Dismiss notification (wait {_Countdown}s)";

            _Countdown--;
        }

        private void VideoPictureBox_Click(object sender, EventArgs e) {

        }

        private void ChannelPictureBox_Click(object sender, EventArgs e) {

        }

        private void WatchedButton_Click(object sender, EventArgs e) {
            ProcessUtils.Start(_Video.URL);
            Videos.SetStatus(_Video.VideoID, Status.Watched);
            Close();
        }

        private void DismissButton_Click(object sender, EventArgs e) {
            Videos.SetStatus(_Video.VideoID, Status.Dismissed);
            Close();
        }

        /// <summary>
        /// The icon is a derivative of (https://www.flaticon.com/free-icon/notification_402727)
        /// By Smashicons (https://www.flaticon.com/authors/smashicons, https://smashicons.com)
        /// Creative Commons BY 3.0 (http://creativecommons.org/licenses/by/3.0/)
        /// </summary>
        private void SetIcon() => Icon = (SettingsManager.Configuration.UseLightWindowIcon) ? Properties.Resources.VideoNotificationsLightIcon : Properties.Resources.VideoNotificationsIcon;

    }

}
