namespace Recorder
{
    partial class SettingForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.TB_WebsocketUrl = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.BT_Cancel = new System.Windows.Forms.Button();
            this.BT_Save = new System.Windows.Forms.Button();
            this.CB_DefaultMic = new System.Windows.Forms.ComboBox();
            this.TB_DefaultAudioLength = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.TB_ApiUrl = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Websocket URL";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 110);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(114, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "Độ Dài Mặc Định";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 151);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(92, 17);
            this.label3.TabIndex = 2;
            this.label3.Text = "Mic Mặc Định";
            // 
            // TB_WebsocketUrl
            // 
            this.TB_WebsocketUrl.Location = new System.Drawing.Point(163, 32);
            this.TB_WebsocketUrl.Name = "TB_WebsocketUrl";
            this.TB_WebsocketUrl.Size = new System.Drawing.Size(594, 22);
            this.TB_WebsocketUrl.TabIndex = 3;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.TB_ApiUrl);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.BT_Cancel);
            this.groupBox1.Controls.Add(this.BT_Save);
            this.groupBox1.Controls.Add(this.CB_DefaultMic);
            this.groupBox1.Controls.Add(this.TB_DefaultAudioLength);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.TB_WebsocketUrl);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(776, 234);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Cài đặt";
            // 
            // BT_Cancel
            // 
            this.BT_Cancel.Location = new System.Drawing.Point(381, 194);
            this.BT_Cancel.Name = "BT_Cancel";
            this.BT_Cancel.Size = new System.Drawing.Size(88, 34);
            this.BT_Cancel.TabIndex = 7;
            this.BT_Cancel.Text = "Hủy";
            this.BT_Cancel.UseVisualStyleBackColor = true;
            this.BT_Cancel.Click += new System.EventHandler(this.BT_Cancel_Click);
            // 
            // BT_Save
            // 
            this.BT_Save.Location = new System.Drawing.Point(263, 194);
            this.BT_Save.Name = "BT_Save";
            this.BT_Save.Size = new System.Drawing.Size(90, 34);
            this.BT_Save.TabIndex = 6;
            this.BT_Save.Text = "Lưu";
            this.BT_Save.UseVisualStyleBackColor = true;
            this.BT_Save.Click += new System.EventHandler(this.BT_Save_Click);
            // 
            // CB_DefaultMic
            // 
            this.CB_DefaultMic.FormattingEnabled = true;
            this.CB_DefaultMic.Location = new System.Drawing.Point(163, 151);
            this.CB_DefaultMic.Name = "CB_DefaultMic";
            this.CB_DefaultMic.Size = new System.Drawing.Size(417, 24);
            this.CB_DefaultMic.TabIndex = 5;
            // 
            // TB_DefaultAudioLength
            // 
            this.TB_DefaultAudioLength.Location = new System.Drawing.Point(163, 110);
            this.TB_DefaultAudioLength.Name = "TB_DefaultAudioLength";
            this.TB_DefaultAudioLength.Size = new System.Drawing.Size(111, 22);
            this.TB_DefaultAudioLength.TabIndex = 4;
            this.TB_DefaultAudioLength.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            this.TB_DefaultAudioLength.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TB_DefaultAudioLength_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 70);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 17);
            this.label4.TabIndex = 8;
            this.label4.Text = "ApiUrl";
            // 
            // TB_ApiUrl
            // 
            this.TB_ApiUrl.Location = new System.Drawing.Point(163, 70);
            this.TB_ApiUrl.Name = "TB_ApiUrl";
            this.TB_ApiUrl.Size = new System.Drawing.Size(594, 22);
            this.TB_ApiUrl.TabIndex = 9;
            // 
            // SettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 277);
            this.Controls.Add(this.groupBox1);
            this.Name = "SettingForm";
            this.Text = "SettingForm";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox TB_WebsocketUrl;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox CB_DefaultMic;
        private System.Windows.Forms.TextBox TB_DefaultAudioLength;
        private System.Windows.Forms.Button BT_Cancel;
        private System.Windows.Forms.Button BT_Save;
        private System.Windows.Forms.TextBox TB_ApiUrl;
        private System.Windows.Forms.Label label4;
    }
}