using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Recorder
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            DialogResult result;
            using (var login_form = new LoginForm())
                result = login_form.ShowDialog();

            if (result == DialogResult.OK)
            {
                Application.Run(new Recorder_Tab_OfflineRecording());
            }
        }
    }
}
