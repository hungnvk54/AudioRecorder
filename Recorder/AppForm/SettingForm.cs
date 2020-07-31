using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NAudio.Wave;

namespace AudioRecorderApps
{
    public partial class SettingForm : Form
    {
        public SettingForm()
        {
            InitializeComponent();

            //Fill the data with configure parameter
            for (int i = 0; i < WaveIn.DeviceCount; i++)
            {
                CB_DefaultMic.Items.Add(WaveIn.GetCapabilities(i).ProductName);
            }


            TB_WebsocketUrl.Text = AppsSettings.GetInstance().WebsocketUrl;
            TB_DefaultAudioLength.Text = AppsSettings.GetInstance().DefaultAudioLength;
            TB_ApiUrl.Text = AppsSettings.GetInstance().ApiUrl;
            this.ConfigForm_TB_DataPath.Text = AppsSettings.GetInstance().DataDir;
            if (Int16.Parse(AppsSettings.GetInstance().DefaultMic) != -1)
            {
                CB_DefaultMic.SelectedIndex = Int16.Parse(AppsSettings.GetInstance().DefaultMic);
            }
        }

        private void TB_DefaultAudioLength_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void BT_Save_Click(object sender, EventArgs e)
        {
            AppsSettings.GetInstance().WebsocketUrl = TB_WebsocketUrl.Text;
            AppsSettings.GetInstance().DefaultAudioLength = TB_DefaultAudioLength.Text;
            AppsSettings.GetInstance().DefaultMic = CB_DefaultMic.SelectedIndex.ToString();
            AppsSettings.GetInstance().ApiUrl = TB_ApiUrl.Text;
            AppsSettings.GetInstance().DataDir = ConfigForm_TB_DataPath.Text;
            AppsSettings.GetInstance().SaveConfigure();

            this.Close();
        }

        private void BT_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ConfigForm_BT_FolderBrower_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    this.ConfigForm_TB_DataPath.Text = fbd.SelectedPath;
                }
            }
        }
    }
}
