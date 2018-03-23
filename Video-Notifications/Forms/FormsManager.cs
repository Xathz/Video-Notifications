using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace VideoNotifications.Forms {

    /// <summary>
    /// Keeps track of open forms. <see cref="Application.OpenForms"/> does not work sometimes. See <see langword="remarks"/>.
    /// </summary>
    /// <remarks>
    /// https://stackoverflow.com/a/3751748
    /// <para>
    /// There's a bug in Windows Forms that makes a form disappear from the Application.OpenForms collection.
    /// This will happen when you assign the ShowInTaskbar, FormBorderStyle, ControlBox, Min/MaximizedBox, RightToLeftLayout, 
    /// HelpButton, Opacity, TransparencyKey, ShowIcon or MdiParent property after the window was created. 
    /// These properties are special in that they are specified as style flags in the native CreateWindowEx() call.
    /// </para>
    /// </remarks>
    internal static class FormsManager {

        private static MainForm _StaticMainForm;
        /// <summary>
        /// A public <see langword="static"/> holding for the <see cref="MainForm"/> to be called/referenced by other forms.
        /// </summary>
        public static MainForm StaticMainForm {
            get => _StaticMainForm;
            set => _StaticMainForm = value;
        }

        private static List<Form> _OpenForms = new List<Form>();
        /// <summary>
        /// List of open forms.
        /// For this to work all forms must add themselves to this list on <see cref="Form.Load"/>.
        /// They must also remove themselves from this list on <see cref="Form.FormClosed"/>.
        /// </summary>
        public static List<Form> OpenForms => _OpenForms;

        /// <summary>
        /// Check if a form is already open of the same <see cref="Type"/>. The matching <see cref="Form"/> will be returned, or <see langword="null"/> if one is not already open.
        /// <para>Currently open forms are stored in <see cref="OpenForms"/>.</para>
        /// </summary>
        public static Form GetOpenForm(Form form) => _OpenForms.FirstOrDefault(f => f.GetType() == form.GetType());

    }

}
