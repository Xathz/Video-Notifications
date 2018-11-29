using System;
using System.IO;
using System.Windows.Forms;
using VideoNotifications.Forms;
using VideoNotifications.Settings;

namespace VideoNotifications {

    static class Program {

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args) {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            FormsManager.ShowSplashForm();

            if (args.Length > 0) {
                int length = (args.Length - 1);
                for (int i = 0; i <= length; i++) {
                    string thisArg = args[i];
                    string nextArg = ((i + 1) > length) ? "" : args[i + 1];

                    if (thisArg == "-w" || thisArg == "--workingdirectory") {
                        Constants.WorkingDirectory = nextArg;
                    }

                }
            }

            Directory.CreateDirectory(Constants.WorkingDirectory);
            Directory.CreateDirectory(Constants.LogDirectory);
            Directory.CreateDirectory(Constants.ImagesDirectory);
            Directory.CreateDirectory(Constants.ChannelBannerDirectory);
            Directory.CreateDirectory(Constants.ChannelIconDirectory);
            Directory.CreateDirectory(Constants.VideoThumbnailDirectory);

            LoggingManager.Initialize();
            SettingsManager.Load();
            Database.Base.Initialize();

            FormsManager.CloseSplashForm();
            if (string.IsNullOrWhiteSpace(SettingsManager.Configuration.YouTubeAPIKey)) {
                APIKeyForm apiKeyForm = new APIKeyForm();
                apiKeyForm.ShowDialog();
            }

            FormsManager.StaticMainForm = new MainForm();
            FormsManager.OpenForms.Add(FormsManager.StaticMainForm);
            Application.Run(FormsManager.StaticMainForm);
        }

    }

}
