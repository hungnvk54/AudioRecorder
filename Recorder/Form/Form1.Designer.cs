namespace Recorder
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuStrip2 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.BT_Pause = new System.Windows.Forms.Button();
            this.BT_Start = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.TB_AudioLength = new System.Windows.Forms.TextBox();
            this.tb_SessionId = new System.Windows.Forms.TextBox();
            this.TB_FileName = new System.Windows.Forms.TextBox();
            this.sessionId = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cb_ListMicIn = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.VM_VolumeMeter = new NAudio.Gui.VolumeMeter();
            this.LB_ConnectionState = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.LB_Timer = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.RT_ShortSentence = new System.Windows.Forms.RichTextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.RT_FullSentence = new System.Windows.Forms.RichTextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuStrip2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Location = new System.Drawing.Point(0, 28);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1348, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menuStrip2
            // 
            this.menuStrip2.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip2.Location = new System.Drawing.Point(0, 0);
            this.menuStrip2.Name = "menuStrip2";
            this.menuStrip2.Size = new System.Drawing.Size(1348, 28);
            this.menuStrip2.TabIndex = 2;
            this.menuStrip2.Text = "menuStrip2";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(46, 24);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // settingToolStripMenuItem
            // 
            this.settingToolStripMenuItem.Name = "settingToolStripMenuItem";
            this.settingToolStripMenuItem.Size = new System.Drawing.Size(139, 26);
            this.settingToolStripMenuItem.Text = "Setting";
            this.settingToolStripMenuItem.Click += new System.EventHandler(this.settingToolStripMenuItem_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.AutoSize = true;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.groupBox3);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Location = new System.Drawing.Point(12, 28);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1336, 688);
            this.panel1.TabIndex = 3;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.BT_Pause);
            this.groupBox3.Controls.Add(this.BT_Start);
            this.groupBox3.Location = new System.Drawing.Point(1121, 275);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(210, 86);
            this.groupBox3.TabIndex = 14;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Ghi âm";
            // 
            // BT_Pause
            // 
            this.BT_Pause.Enabled = false;
            this.BT_Pause.Location = new System.Drawing.Point(115, 27);
            this.BT_Pause.Name = "BT_Pause";
            this.BT_Pause.Size = new System.Drawing.Size(85, 42);
            this.BT_Pause.TabIndex = 11;
            this.BT_Pause.Text = "Pause";
            this.BT_Pause.UseVisualStyleBackColor = true;
            this.BT_Pause.Click += new System.EventHandler(this.BT_Pause_Click);
            // 
            // BT_Start
            // 
            this.BT_Start.Location = new System.Drawing.Point(8, 27);
            this.BT_Start.Name = "BT_Start";
            this.BT_Start.Size = new System.Drawing.Size(87, 42);
            this.BT_Start.TabIndex = 6;
            this.BT_Start.Text = "Start";
            this.BT_Start.UseVisualStyleBackColor = true;
            this.BT_Start.Click += new System.EventHandler(this.BT_Start_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.TB_AudioLength);
            this.groupBox2.Controls.Add(this.tb_SessionId);
            this.groupBox2.Controls.Add(this.TB_FileName);
            this.groupBox2.Controls.Add(this.sessionId);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.cb_ListMicIn);
            this.groupBox2.Location = new System.Drawing.Point(1121, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(207, 266);
            this.groupBox2.TabIndex = 13;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Thông tin phiên";
            // 
            // TB_AudioLength
            // 
            this.TB_AudioLength.Location = new System.Drawing.Point(7, 239);
            this.TB_AudioLength.Name = "TB_AudioLength";
            this.TB_AudioLength.Size = new System.Drawing.Size(193, 22);
            this.TB_AudioLength.TabIndex = 9;
            this.TB_AudioLength.TextChanged += new System.EventHandler(this.TB_AudioLength_TextChanged);
            this.TB_AudioLength.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tb_SessionId_KeyPress);
            // 
            // tb_SessionId
            // 
            this.tb_SessionId.Location = new System.Drawing.Point(8, 43);
            this.tb_SessionId.Name = "tb_SessionId";
            this.tb_SessionId.Size = new System.Drawing.Size(194, 22);
            this.tb_SessionId.TabIndex = 0;
            this.tb_SessionId.TextChanged += new System.EventHandler(this.tb_SessionId_TextChanged);
            this.tb_SessionId.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tb_SessionId_KeyPress);
            // 
            // TB_FileName
            // 
            this.TB_FileName.Location = new System.Drawing.Point(8, 175);
            this.TB_FileName.Name = "TB_FileName";
            this.TB_FileName.Size = new System.Drawing.Size(193, 22);
            this.TB_FileName.TabIndex = 8;
            this.TB_FileName.TextChanged += new System.EventHandler(this.TB_FileName_TextChanged);
            // 
            // sessionId
            // 
            this.sessionId.AutoSize = true;
            this.sessionId.Location = new System.Drawing.Point(8, 23);
            this.sessionId.Name = "sessionId";
            this.sessionId.Size = new System.Drawing.Size(71, 17);
            this.sessionId.TabIndex = 1;
            this.sessionId.Text = "Mã  Phiên";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 219);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(80, 17);
            this.label6.TabIndex = 7;
            this.label6.Text = "Độ Dài Tệp";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 86);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Mic In";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(11, 154);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(62, 17);
            this.label5.TabIndex = 6;
            this.label5.Text = "Tên Tệp";
            // 
            // cb_ListMicIn
            // 
            this.cb_ListMicIn.FormattingEnabled = true;
            this.cb_ListMicIn.Location = new System.Drawing.Point(7, 106);
            this.cb_ListMicIn.Name = "cb_ListMicIn";
            this.cb_ListMicIn.Size = new System.Drawing.Size(194, 24);
            this.cb_ListMicIn.TabIndex = 3;
            this.cb_ListMicIn.SelectedIndexChanged += new System.EventHandler(this.cb_ListMicIn_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.VM_VolumeMeter);
            this.groupBox1.Controls.Add(this.LB_ConnectionState);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.LB_Timer);
            this.groupBox1.Location = new System.Drawing.Point(1121, 367);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(210, 147);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Thông tin ghi âm";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(120, 17);
            this.label3.TabIndex = 8;
            this.label3.Text = "Thời Gian Ghi Âm";
            // 
            // VM_VolumeMeter
            // 
            this.VM_VolumeMeter.Amplitude = 0F;
            this.VM_VolumeMeter.ForeColor = System.Drawing.Color.Chartreuse;
            this.VM_VolumeMeter.Location = new System.Drawing.Point(9, 79);
            this.VM_VolumeMeter.MaxDb = 18F;
            this.VM_VolumeMeter.MinDb = -60F;
            this.VM_VolumeMeter.Name = "VM_VolumeMeter";
            this.VM_VolumeMeter.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.VM_VolumeMeter.Size = new System.Drawing.Size(194, 23);
            this.VM_VolumeMeter.TabIndex = 5;
            this.VM_VolumeMeter.Text = "volumeMeter1";
            // 
            // LB_ConnectionState
            // 
            this.LB_ConnectionState.AutoSize = true;
            this.LB_ConnectionState.BackColor = System.Drawing.Color.Red;
            this.LB_ConnectionState.Location = new System.Drawing.Point(136, 123);
            this.LB_ConnectionState.Name = "LB_ConnectionState";
            this.LB_ConnectionState.Size = new System.Drawing.Size(36, 17);
            this.LB_ConnectionState.TabIndex = 10;
            this.LB_ConnectionState.Text = "       ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 17);
            this.label2.TabIndex = 4;
            this.label2.Text = "Mic Volume";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 123);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 17);
            this.label4.TabIndex = 9;
            this.label4.Text = "Kết Nối Server";
            // 
            // LB_Timer
            // 
            this.LB_Timer.AutoSize = true;
            this.LB_Timer.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LB_Timer.Location = new System.Drawing.Point(131, 27);
            this.LB_Timer.Name = "LB_Timer";
            this.LB_Timer.Size = new System.Drawing.Size(73, 20);
            this.LB_Timer.TabIndex = 7;
            this.LB_Timer.Text = "00:00:00";
            this.LB_Timer.Click += new System.EventHandler(this.LB_Timer_Click);
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.RT_ShortSentence);
            this.panel3.Location = new System.Drawing.Point(3, 574);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1112, 109);
            this.panel3.TabIndex = 1;
            // 
            // RT_ShortSentence
            // 
            this.RT_ShortSentence.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.RT_ShortSentence.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RT_ShortSentence.Location = new System.Drawing.Point(-1, -1);
            this.RT_ShortSentence.Name = "RT_ShortSentence";
            this.RT_ShortSentence.ReadOnly = true;
            this.RT_ShortSentence.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.RT_ShortSentence.Size = new System.Drawing.Size(1112, 105);
            this.RT_ShortSentence.TabIndex = 0;
            this.RT_ShortSentence.Text = "";
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.RT_FullSentence);
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1112, 565);
            this.panel2.TabIndex = 0;
            // 
            // RT_FullSentence
            // 
            this.RT_FullSentence.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.RT_FullSentence.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RT_FullSentence.Location = new System.Drawing.Point(0, 0);
            this.RT_FullSentence.Name = "RT_FullSentence";
            this.RT_FullSentence.ReadOnly = true;
            this.RT_FullSentence.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.RT_FullSentence.Size = new System.Drawing.Size(1111, 564);
            this.RT_FullSentence.TabIndex = 0;
            this.RT_FullSentence.Text = "";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1348, 713);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.menuStrip2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximumSize = new System.Drawing.Size(1366, 760);
            this.MinimumSize = new System.Drawing.Size(1366, 760);
            this.Name = "Form1";
            this.Text = "Audio Recording Apps";
            this.menuStrip2.ResumeLayout(false);
            this.menuStrip2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.MenuStrip menuStrip2;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingToolStripMenuItem;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label sessionId;
        private System.Windows.Forms.TextBox tb_SessionId;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cb_ListMicIn;
        private System.Windows.Forms.Label label2;
        private NAudio.Gui.VolumeMeter VM_VolumeMeter;
        private System.Windows.Forms.Label LB_Timer;
        private System.Windows.Forms.Button BT_Start;
        private System.Windows.Forms.RichTextBox RT_ShortSentence;
        private System.Windows.Forms.RichTextBox RT_FullSentence;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label LB_ConnectionState;
        private System.Windows.Forms.TextBox TB_AudioLength;
        private System.Windows.Forms.TextBox TB_FileName;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button BT_Pause;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox3;
    }
}

