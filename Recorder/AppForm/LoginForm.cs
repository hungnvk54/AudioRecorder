using AudioRecorderApps.DataMessage;
using AudioRecorderApps.Login;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WebSocketSharp;

namespace AudioRecorderApps
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
            LoadLoginInfo();

            if (this.Login_TB_Password.Text.IsNullOrEmpty())
            {
                this.LoginForm_CB_SavePassword.Checked = false;
            }
            else
            {
                this.LoginForm_CB_SavePassword.Checked = true;
            }
        }

        private void SaveInfo()
        {
            PasswordRepository password_repos = new PasswordRepository();

            password_repos.SaveUserName(this.Login_TB_UserName.Text);

            if (this.LoginForm_CB_SavePassword.Checked)
            {
                password_repos.SavePassword(this.Login_TB_Password.Text);
            }
        }

        private void LoadLoginInfo()
        {
            PasswordRepository password_repos = new PasswordRepository();

            string userName = password_repos.GetUsername();
            string password = password_repos.GetPassword();

            this.Login_TB_UserName.Text = userName;
            this.Login_TB_Password.Text = password;
        }

        private void Login_To_System()
        {
            // Set cursor as hourglass
            Cursor.Current = Cursors.WaitCursor;
            this.SaveInfo();
            LoginSuccessfullyMessage token = Request.RequestLogin(this.Login_TB_UserName.Text, this.Login_TB_Password.Text);

            if (token == null)
            {
                ///Return False here
                Logger.GetInstance().Logging.Error("Cannot login to system");
                this.LoginForm_LB_Info.Text = "Không thể đăng nhập vào hệ thống.\nKiểm tra lại Tên đăng nhập/Mật khẩu";
            }
            else if (token.roleType == "2")
            {
                Logger.GetInstance().Logging.Error("Cannot login to system with this role");
                this.LoginForm_LB_Info.Text = "Không thể đăng nhập vào hệ thống \nbằng quyền người duyệt";
            }
            else
            {
                Logger.GetInstance().Logging.Info("Login done");
                this.LoginForm_LB_Info.Text = "";
                AppsSettings.GetInstance().AuthorizeToken = token.Authorization;
                DialogResult = DialogResult.OK;
            }
            // Set cursor as default arrow
            Cursor.Current = Cursors.Default;
        }

        private void Login_BT_Login_Click(object sender, EventArgs e)
        {
            Login_To_System();
        }

        private void settinToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var setting = new SettingForm();
            setting.ShowDialog();
        }

        private void LoginForm_CB_SavePassword_CheckedChanged(object sender, EventArgs e)
        {
            if (this.LoginForm_CB_SavePassword.Checked)
            {
                SaveInfo();
            }
            else
            {
                PasswordRepository password_repos = new PasswordRepository();
                password_repos.SavePassword("");
            }
        }
    }
}
