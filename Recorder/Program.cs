using AudioRecorderApps.AppForm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

using Recorder.AppForm;

namespace AudioRecorderApps
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 

        static void ConfigureTlsVersion()
        {
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12 | System.Net.SecurityProtocolType.Tls11
                                                                | System.Net.SecurityProtocolType.Tls | System.Net.SecurityProtocolType.Ssl3;
        }

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            ConfigureTlsVersion();

            DialogResult result = DialogResult.OK;
            using (var login_form = new LoginForm())
               result = login_form.ShowDialog();

            if (result == DialogResult.OK)
            {
                Application.Run(new AudioRecordingForms());
            }

            //Application.Run(new AudioTesting());
        }
    }
}
