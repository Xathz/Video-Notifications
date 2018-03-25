using System;
using System.Windows.Forms;
using VideoNotifications.Settings;
using VideoNotifications.Utilities;

namespace VideoNotifications.Forms {

    public partial class APIKeyForm : Form {

        public APIKeyForm() {
            InitializeComponent();

            // The icon is a derivative of (https://www.flaticon.com/free-icon/notification_402727)
            // By Smashicons (https://www.flaticon.com/authors/smashicons, https://smashicons.com)
            // Creative Commons BY 3.0 (http://creativecommons.org/licenses/by/3.0/)
            Icon = (SettingsManager.Configuration.UseLightWindowIcon) ? Properties.Resources.VideoNotificationsLightIcon : Properties.Resources.VideoNotificationsIcon;
            ControlBox = false;
        }

        private void APIKeyForm_Load(object sender, EventArgs e) { }

        private void ProjectCreateLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) => ProcessUtils.Start("https://console.developers.google.com/projectcreate");

        private void YouTubeAPILinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) => ProcessUtils.Start("https://console.developers.google.com/apis/library/youtube.googleapis.com");

        private void CredentialsLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) => ProcessUtils.Start("https://console.developers.google.com/apis/credentials");

        private void APIKeyTextBox_TextChanged(object sender, EventArgs e) => OKButton.Enabled = !string.IsNullOrWhiteSpace(APIKeyTextBox.Text);

        private void ExitLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) => Environment.Exit(1);

        private void SkipLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            if (MessageBox.Show($"Are you sure you want to continue without entering a YouTube API key?{Environment.NewLine}{Environment.NewLine}This is not recommended as the program is completely useless without one.", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes) {
                Dispose();
            }
        }

        private void OKButton_Click(object sender, EventArgs e) {
            SettingsManager.Configuration.YouTubeAPIKey = APIKeyTextBox.Text.Trim();
            SettingsManager.Save();
            Close();
        }

    }

}
