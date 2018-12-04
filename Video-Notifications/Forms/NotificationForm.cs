using System;
using System.Drawing;
using System.Windows.Forms;
using Humanizer;
using VideoNotifications.Settings;
using VideoNotifications.Utilities;

namespace VideoNotifications.Forms {

    public partial class NotificationForm : Form {

        private int _Countdown = 2;
        private Database.Types.Video _Video { get; set; }

        public NotificationForm() {
            InitializeComponent();
            SetIcon();

            if (SettingsManager.Configuration.NotificationWindow.PositionSet()) {
                Location = new Point(SettingsManager.Configuration.NotificationWindow.X, SettingsManager.Configuration.NotificationWindow.Y);
            } else {
                CenterToScreen();
            }
        }

        public NotificationForm(Database.Types.Video video) {
            InitializeComponent();
            SetIcon();

            if (SettingsManager.Configuration.NotificationWindow.PositionSet()) {
                Location = new Point(SettingsManager.Configuration.NotificationWindow.X, SettingsManager.Configuration.NotificationWindow.Y);
            } else {
                CenterToScreen();
            }

            _Video = video;

            Image channelImage = Database.ImageFile.Get(_Video.ChannelID, ImageType.ChannelIcon);
            Image channelImageResized = ImageUtils.ResizeImage(channelImage, 365, 365);
            Image channelImageFaded = ImageUtils.SetImageOpacity(channelImageResized, 0.16f);
            BackgroundImage = channelImageFaded;

            ChannelPictureBox.Image = channelImage;
            VideoPictureBox.Image = Database.ImageFile.Get(_Video.ID, ImageType.VideoThumbnail);

            ChannelLabel.Text = $"A new video was posted by {_Video.Channel.Title} {_Video.Posted.Value.Humanize()}.";
            DurationLabel.Text = TimeSpanUtils.ConvertDurationCompact(video.Duration);

            MarkButton.Text = $"Close & mark as: (wait {(_Countdown + 1)}s)";
            OpenVideoCheckBox.Checked = SettingsManager.Configuration.NotificationOpenVideo;

            OpenVideoStatusComboBox.Items.Add(WatchStatus.Unwatched);
            OpenVideoStatusComboBox.Items.Add(WatchStatus.Watched);
            OpenVideoStatusComboBox.Items.Add(WatchStatus.Dismissed);
            OpenVideoStatusComboBox.Items.Add(WatchStatus.Ignored);
            OpenVideoStatusComboBox.SelectedItem = SettingsManager.Configuration.NotificationDefaultVideoStatus;

            ControlBox = false;
        }

        private void NotificationForm_Load(object sender, EventArgs e) {
            FormsManager.OpenForms.Add(this);

            EnableControlsCountdownTimer.Start();
            EnableControlsTimer.Start();
        }

        private void NotificationForm_FormClosing(object sender, FormClosingEventArgs e) => SettingsManager.Save();

        private void NotificationForm_FormClosed(object sender, FormClosedEventArgs e) => FormsManager.OpenForms.Remove(this);

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

            MarkButton.Text = "Close & mark as:";
            OpenVideoCheckBox.Enabled = true;
            OpenVideoStatusComboBox.Enabled = true;
            MarkButton.Enabled = true;
        }

        private void EnableControlsCountdownTimer_Tick(object sender, EventArgs e) {
            MarkButton.Text = $"Close & mark as: (wait {_Countdown}s)";

            _Countdown--;
        }

        private void VideoPictureBox_Click(object sender, EventArgs e) => ProcessUtils.Start(_Video.URL);

        private void ChannelPictureBox_Click(object sender, EventArgs e) => ProcessUtils.Start(_Video.Channel.URL);

        private void OpenVideoCheckBox_CheckedChanged(object sender, EventArgs e) => SettingsManager.Configuration.NotificationOpenVideo = OpenVideoCheckBox.Checked;

        private void MarkButton_Click(object sender, EventArgs e) {
            Database.Videos.SetStatus(_Video.ID, (WatchStatus)Enum.Parse(typeof(WatchStatus), OpenVideoStatusComboBox.SelectedItem.ToString()));
            if (OpenVideoCheckBox.Checked) { ProcessUtils.Start(_Video.URL); }

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
