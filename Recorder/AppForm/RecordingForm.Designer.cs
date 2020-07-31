namespace AudioRecorderApps
{
    partial class AudioRecordingForms
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AudioRecordingForms));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mởThưMụcToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.VM_VolumeMeter = new NAudio.Gui.VolumeMeter();
            this.LB_ConnectionState = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.LB_Timer = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.TB_AudioLength = new System.Windows.Forms.TextBox();
            this.tb_SessionId = new System.Windows.Forms.TextBox();
            this.TB_FileName = new System.Windows.Forms.TextBox();
            this.sessionId = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cb_ListMicIn = new System.Windows.Forms.ComboBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.RecordingForm_CB_SaveAudio = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.BT_Pause = new System.Windows.Forms.Button();
            this.BT_Start = new System.Windows.Forms.Button();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.AudioRecording_Tab = new CustomControl.AudioRecordingTabControl();
            this.Online_Recording_Tab = new System.Windows.Forms.TabPage();
            this.RT_FullSentence = new System.Windows.Forms.RichTextBox();
            this.RT_ShortSentence = new System.Windows.Forms.RichTextBox();
            this.OfflineRecording_Tab = new System.Windows.Forms.TabPage();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.ListAudioFile = new System.Windows.Forms.ListView();
            this.name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.duration = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.status = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.fullPath = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panel2 = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.contextMenuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.AudioRecording_Tab.SuspendLayout();
            this.Online_Recording_Tab.SuspendLayout();
            this.OfflineRecording_Tab.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mởThưMụcToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(168, 28);
            // 
            // mởThưMụcToolStripMenuItem
            // 
            this.mởThưMụcToolStripMenuItem.Name = "mởThưMụcToolStripMenuItem";
            this.mởThưMụcToolStripMenuItem.Size = new System.Drawing.Size(167, 24);
            this.mởThưMụcToolStripMenuItem.Text = "Mở Thư Mục";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.VM_VolumeMeter);
            this.groupBox1.Controls.Add(this.LB_ConnectionState);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.LB_Timer);
            this.groupBox1.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(3, 528);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Size = new System.Drawing.Size(211, 151);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Thông tin ghi âm";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(4, 33);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(81, 19);
            this.label3.TabIndex = 8;
            this.label3.Text = "Thời Gian:";
            // 
            // VM_VolumeMeter
            // 
            this.VM_VolumeMeter.Amplitude = 0F;
            this.VM_VolumeMeter.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.VM_VolumeMeter.ForeColor = System.Drawing.Color.Chartreuse;
            this.VM_VolumeMeter.Location = new System.Drawing.Point(11, 86);
            this.VM_VolumeMeter.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.VM_VolumeMeter.MaxDb = 18F;
            this.VM_VolumeMeter.MinDb = -60F;
            this.VM_VolumeMeter.Name = "VM_VolumeMeter";
            this.VM_VolumeMeter.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.VM_VolumeMeter.Size = new System.Drawing.Size(195, 23);
            this.VM_VolumeMeter.TabIndex = 5;
            this.VM_VolumeMeter.Text = "volumeMeter1";
            // 
            // LB_ConnectionState
            // 
            this.LB_ConnectionState.AutoSize = true;
            this.LB_ConnectionState.BackColor = System.Drawing.Color.Red;
            this.LB_ConnectionState.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LB_ConnectionState.Location = new System.Drawing.Point(101, 121);
            this.LB_ConnectionState.Name = "LB_ConnectionState";
            this.LB_ConnectionState.Size = new System.Drawing.Size(37, 19);
            this.LB_ConnectionState.TabIndex = 10;
            this.LB_ConnectionState.Text = "       ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(5, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 19);
            this.label2.TabIndex = 4;
            this.label2.Text = "Mic Volume";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(5, 121);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 19);
            this.label4.TabIndex = 9;
            this.label4.Text = "Kết Nối";
            // 
            // LB_Timer
            // 
            this.LB_Timer.AutoSize = true;
            this.LB_Timer.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LB_Timer.Location = new System.Drawing.Point(91, 28);
            this.LB_Timer.Name = "LB_Timer";
            this.LB_Timer.Size = new System.Drawing.Size(96, 27);
            this.LB_Timer.TabIndex = 7;
            this.LB_Timer.Text = "00:00:00";
            this.LB_Timer.Click += new System.EventHandler(this.LB_Timer_Click);
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
            this.groupBox2.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(3, 100);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Size = new System.Drawing.Size(207, 278);
            this.groupBox2.TabIndex = 13;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Thông tin phiên";
            // 
            // TB_AudioLength
            // 
            this.TB_AudioLength.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TB_AudioLength.Location = new System.Drawing.Point(5, 241);
            this.TB_AudioLength.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.TB_AudioLength.Name = "TB_AudioLength";
            this.TB_AudioLength.Size = new System.Drawing.Size(193, 27);
            this.TB_AudioLength.TabIndex = 9;
            this.TB_AudioLength.TextChanged += new System.EventHandler(this.TB_AudioLength_TextChanged);
            this.TB_AudioLength.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tb_SessionId_KeyPress);
            // 
            // tb_SessionId
            // 
            this.tb_SessionId.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_SessionId.Location = new System.Drawing.Point(5, 46);
            this.tb_SessionId.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tb_SessionId.Name = "tb_SessionId";
            this.tb_SessionId.Size = new System.Drawing.Size(193, 27);
            this.tb_SessionId.TabIndex = 0;
            this.tb_SessionId.TextChanged += new System.EventHandler(this.tb_SessionId_TextChanged);
            this.tb_SessionId.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tb_SessionId_KeyPress);
            // 
            // TB_FileName
            // 
            this.TB_FileName.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TB_FileName.Location = new System.Drawing.Point(5, 175);
            this.TB_FileName.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.TB_FileName.Name = "TB_FileName";
            this.TB_FileName.Size = new System.Drawing.Size(193, 27);
            this.TB_FileName.TabIndex = 8;
            this.TB_FileName.TextChanged += new System.EventHandler(this.TB_FileName_TextChanged);
            // 
            // sessionId
            // 
            this.sessionId.AutoSize = true;
            this.sessionId.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sessionId.Location = new System.Drawing.Point(3, 23);
            this.sessionId.Name = "sessionId";
            this.sessionId.Size = new System.Drawing.Size(78, 19);
            this.sessionId.TabIndex = 1;
            this.sessionId.Text = "Mã  Phiên";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(3, 219);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(87, 19);
            this.label6.TabIndex = 7;
            this.label6.Text = "Độ Dài Tệp";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 86);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 19);
            this.label1.TabIndex = 2;
            this.label1.Text = "Mic In";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(3, 153);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(63, 19);
            this.label5.TabIndex = 6;
            this.label5.Text = "Tên Tệp";
            // 
            // cb_ListMicIn
            // 
            this.cb_ListMicIn.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb_ListMicIn.FormattingEnabled = true;
            this.cb_ListMicIn.Location = new System.Drawing.Point(5, 108);
            this.cb_ListMicIn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cb_ListMicIn.Name = "cb_ListMicIn";
            this.cb_ListMicIn.Size = new System.Drawing.Size(193, 27);
            this.cb_ListMicIn.TabIndex = 3;
            this.cb_ListMicIn.SelectedIndexChanged += new System.EventHandler(this.cb_ListMicIn_SelectedIndexChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.RecordingForm_CB_SaveAudio);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.BT_Pause);
            this.groupBox3.Controls.Add(this.BT_Start);
            this.groupBox3.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(3, 401);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox3.Size = new System.Drawing.Size(211, 101);
            this.groupBox3.TabIndex = 14;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Ghi âm";
            // 
            // RecordingForm_CB_SaveAudio
            // 
            this.RecordingForm_CB_SaveAudio.AutoSize = true;
            this.RecordingForm_CB_SaveAudio.Checked = true;
            this.RecordingForm_CB_SaveAudio.CheckState = System.Windows.Forms.CheckState.Checked;
            this.RecordingForm_CB_SaveAudio.Location = new System.Drawing.Point(115, 26);
            this.RecordingForm_CB_SaveAudio.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.RecordingForm_CB_SaveAudio.Name = "RecordingForm_CB_SaveAudio";
            this.RecordingForm_CB_SaveAudio.Size = new System.Drawing.Size(18, 17);
            this.RecordingForm_CB_SaveAudio.TabIndex = 13;
            this.RecordingForm_CB_SaveAudio.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(5, 25);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(36, 19);
            this.label7.TabIndex = 12;
            this.label7.Text = "Lưu";
            // 
            // BT_Pause
            // 
            this.BT_Pause.Enabled = false;
            this.BT_Pause.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BT_Pause.Location = new System.Drawing.Point(115, 63);
            this.BT_Pause.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BT_Pause.Name = "BT_Pause";
            this.BT_Pause.Size = new System.Drawing.Size(85, 32);
            this.BT_Pause.TabIndex = 11;
            this.BT_Pause.Text = "Pause";
            this.BT_Pause.UseVisualStyleBackColor = true;
            this.BT_Pause.Click += new System.EventHandler(this.BT_Pause_Click);
            // 
            // BT_Start
            // 
            this.BT_Start.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BT_Start.Location = new System.Drawing.Point(8, 63);
            this.BT_Start.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BT_Start.Name = "BT_Start";
            this.BT_Start.Size = new System.Drawing.Size(87, 32);
            this.BT_Start.TabIndex = 6;
            this.BT_Start.Text = "Start";
            this.BT_Start.UseVisualStyleBackColor = true;
            this.BT_Start.Click += new System.EventHandler(this.BT_Start_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "MP3.png");
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.AudioRecording_Tab);
            this.panel1.Location = new System.Drawing.Point(12, 7);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1109, 686);
            this.panel1.TabIndex = 16;
            // 
            // AudioRecording_Tab
            // 
            this.AudioRecording_Tab.Controls.Add(this.Online_Recording_Tab);
            this.AudioRecording_Tab.Controls.Add(this.OfflineRecording_Tab);
            this.AudioRecording_Tab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AudioRecording_Tab.HotTrack = true;
            this.AudioRecording_Tab.Location = new System.Drawing.Point(0, 0);
            this.AudioRecording_Tab.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.AudioRecording_Tab.Name = "AudioRecording_Tab";
            this.AudioRecording_Tab.Selectable = true;
            this.AudioRecording_Tab.SelectedIndex = 0;
            this.AudioRecording_Tab.ShowToolTips = true;
            this.AudioRecording_Tab.Size = new System.Drawing.Size(1107, 684);
            this.AudioRecording_Tab.TabIndex = 15;
            // 
            // Online_Recording_Tab
            // 
            this.Online_Recording_Tab.Controls.Add(this.RT_FullSentence);
            this.Online_Recording_Tab.Controls.Add(this.RT_ShortSentence);
            this.Online_Recording_Tab.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Online_Recording_Tab.Location = new System.Drawing.Point(4, 25);
            this.Online_Recording_Tab.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Online_Recording_Tab.Name = "Online_Recording_Tab";
            this.Online_Recording_Tab.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Online_Recording_Tab.Size = new System.Drawing.Size(1099, 655);
            this.Online_Recording_Tab.TabIndex = 0;
            this.Online_Recording_Tab.Text = "Dịch Trực Tuyến";
            this.Online_Recording_Tab.ToolTipText = "Dịch trực tuyến";
            this.Online_Recording_Tab.UseVisualStyleBackColor = true;
            // 
            // RT_FullSentence
            // 
            this.RT_FullSentence.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.RT_FullSentence.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RT_FullSentence.Location = new System.Drawing.Point(0, 0);
            this.RT_FullSentence.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.RT_FullSentence.Name = "RT_FullSentence";
            this.RT_FullSentence.ReadOnly = true;
            this.RT_FullSentence.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.RT_FullSentence.Size = new System.Drawing.Size(1099, 530);
            this.RT_FullSentence.TabIndex = 0;
            this.RT_FullSentence.Text = "";
            // 
            // RT_ShortSentence
            // 
            this.RT_ShortSentence.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.RT_ShortSentence.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RT_ShortSentence.Location = new System.Drawing.Point(0, 534);
            this.RT_ShortSentence.Margin = new System.Windows.Forms.Padding(5, 2, 3, 2);
            this.RT_ShortSentence.Name = "RT_ShortSentence";
            this.RT_ShortSentence.ReadOnly = true;
            this.RT_ShortSentence.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.RT_ShortSentence.Size = new System.Drawing.Size(1099, 119);
            this.RT_ShortSentence.TabIndex = 0;
            this.RT_ShortSentence.Text = "";
            // 
            // OfflineRecording_Tab
            // 
            this.OfflineRecording_Tab.Controls.Add(this.groupBox4);
            this.OfflineRecording_Tab.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OfflineRecording_Tab.Location = new System.Drawing.Point(4, 25);
            this.OfflineRecording_Tab.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.OfflineRecording_Tab.Name = "OfflineRecording_Tab";
            this.OfflineRecording_Tab.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.OfflineRecording_Tab.Size = new System.Drawing.Size(1099, 655);
            this.OfflineRecording_Tab.TabIndex = 1;
            this.OfflineRecording_Tab.Text = "Dịch & Ghi Tệp";
            this.OfflineRecording_Tab.ToolTipText = "Ghi Tệp và dịch";
            this.OfflineRecording_Tab.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.ListAudioFile);
            this.groupBox4.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.Location = new System.Drawing.Point(0, 0);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox4.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.groupBox4.Size = new System.Drawing.Size(1092, 655);
            this.groupBox4.TabIndex = 0;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Danh sách file";
            // 
            // ListAudioFile
            // 
            this.ListAudioFile.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.name,
            this.duration,
            this.status,
            this.fullPath});
            this.ListAudioFile.ContextMenuStrip = this.contextMenuStrip1;
            this.ListAudioFile.GridLines = true;
            this.ListAudioFile.HideSelection = false;
            this.ListAudioFile.Location = new System.Drawing.Point(3, 22);
            this.ListAudioFile.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ListAudioFile.MultiSelect = false;
            this.ListAudioFile.Name = "ListAudioFile";
            this.ListAudioFile.Size = new System.Drawing.Size(1089, 631);
            this.ListAudioFile.SmallImageList = this.imageList1;
            this.ListAudioFile.TabIndex = 0;
            this.ListAudioFile.UseCompatibleStateImageBehavior = false;
            this.ListAudioFile.View = System.Windows.Forms.View.Details;
            this.ListAudioFile.SelectedIndexChanged += new System.EventHandler(this.ListAudioFile_SelectedIndexChanged);
            // 
            // name
            // 
            this.name.Text = "Tên";
            this.name.Width = 125;
            // 
            // duration
            // 
            this.duration.Text = "Thời Lượng";
            this.duration.Width = 120;
            // 
            // status
            // 
            this.status.Text = "Trạng Thái";
            this.status.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.status.Width = 127;
            // 
            // fullPath
            // 
            this.fullPath.Text = "Đường dẫn";
            this.fullPath.Width = 702;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.groupBox2);
            this.panel2.Controls.Add(this.groupBox3);
            this.panel2.Controls.Add(this.groupBox1);
            this.panel2.Location = new System.Drawing.Point(1117, 7);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(218, 686);
            this.panel2.TabIndex = 16;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label8.Location = new System.Drawing.Point(27, 10);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(158, 76);
            this.label8.TabIndex = 15;
            this.label8.Text = "Dịch Trực\r\nTuyến";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // AudioRecordingForms
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1347, 711);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximumSize = new System.Drawing.Size(1365, 758);
            this.MinimumSize = new System.Drawing.Size(1365, 758);
            this.Name = "AudioRecordingForms";
            this.Text = "Audio Recording Apps";
            this.Load += new System.EventHandler(this.AudioRecordingForms_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.AudioRecording_Tab.ResumeLayout(false);
            this.Online_Recording_Tab.ResumeLayout(false);
            this.OfflineRecording_Tab.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private NAudio.Gui.VolumeMeter VM_VolumeMeter;
        private System.Windows.Forms.Label LB_ConnectionState;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label LB_Timer;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox TB_AudioLength;
        private System.Windows.Forms.TextBox tb_SessionId;
        private System.Windows.Forms.TextBox TB_FileName;
        private System.Windows.Forms.Label sessionId;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cb_ListMicIn;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox RecordingForm_CB_SaveAudio;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button BT_Pause;
        private System.Windows.Forms.Button BT_Start;
        private CustomControl.AudioRecordingTabControl AudioRecording_Tab;
        private System.Windows.Forms.RichTextBox RT_FullSentence;
        private System.Windows.Forms.RichTextBox RT_ShortSentence;
        private System.Windows.Forms.TabPage OfflineRecording_Tab;
        private System.Windows.Forms.TabPage Online_Recording_Tab;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ListView ListAudioFile;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ColumnHeader name;
        private System.Windows.Forms.ColumnHeader duration;
        private System.Windows.Forms.ColumnHeader status;
        private System.Windows.Forms.ColumnHeader fullPath;
        private System.Windows.Forms.ToolStripMenuItem mởThưMụcToolStripMenuItem;
        private System.Windows.Forms.Label label8;
    }
}

