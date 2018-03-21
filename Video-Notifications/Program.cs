using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using VideoNotifications.Database;
using VideoNotifications.Settings;

namespace VideoNotifications {

    static class Program {

        private static MainForm _StaticMainForm;
        /// <summary>
        /// A public <see langword="static"/> holding for the <see cref="MainForm"/> to be called/referenced by other forms.
        /// </summary>
        public static MainForm StaticMainForm => _StaticMainForm;

        private static List<Form> _OpenForms = new List<Form>();
        /// <summary>
        /// List of open forms.
        /// For this to work all forms must add themselves to this list on <see cref="Form.Load"/>.
        /// They must also remove themselves from this list on <see cref="Form.FormClosed"/>.
        /// </summary>
        public static List<Form> OpenForms => _OpenForms;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args) {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

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

            LoggingManager.Initialize();
            SettingsManager.Load();
            DatabaseBase.Initialize();

            _StaticMainForm = new MainForm();
            _OpenForms.Add(_StaticMainForm);
            Application.Run(_StaticMainForm);
        }

        /// <summary>
        /// Check if a form is already open of the same <see cref="Type"/>. The matching <see cref="Form"/> will be returned, or <see langword="null"/> if one is not already open.
        /// <para>Currently open forms are stored in <see cref="OpenForms"/>.</para>
        /// </summary>
        public static Form GetOpenForm(Form form) => _OpenForms.FirstOrDefault(f => f.GetType() == form.GetType());

    }

}
