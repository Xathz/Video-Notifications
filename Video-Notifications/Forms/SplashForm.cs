using System;
using System.Threading;
using System.Windows.Forms;
using VideoNotifications.Utilities;

namespace VideoNotifications.Forms {

    /// <remarks>https://stackoverflow.com/a/15836105</remarks>
    public partial class SplashForm : Form {

        private delegate void CloseDelegate();

        private static SplashForm _SplashForm;

        public static void ShowSplashForm() {
            if (_SplashForm != null) { return; }

            Thread thread = new Thread(new ThreadStart(ShowFormInternal));
            thread.SetApartmentState(ApartmentState.STA);
            thread.IsBackground = true;
            thread.Start();
        }

        public static void CloseSplashForm() => _SplashForm.Invoke(new CloseDelegate(CloseFormInternal));

        private static void ShowFormInternal() {
            _SplashForm = new SplashForm();
            Application.Run(_SplashForm);
        }

        private static void CloseFormInternal() {
            _SplashForm.Close();
            _SplashForm = null;
        }

        public SplashForm() => InitializeComponent();

        private void SplashForm_Load(object sender, EventArgs e) => LogoPictureBox.Image = ImageUtils.ResizeImage(Properties.Resources.VideoNotificationsPNG, 64, 64);

    }

}
