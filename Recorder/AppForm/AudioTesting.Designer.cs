
namespace Recorder.AppForm
{
    partial class AudioTesting
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AudioTesting));
            this.lbl_mic = new System.Windows.Forms.Label();
            this.CB_ListMic = new System.Windows.Forms.ComboBox();
            this.btn_test = new System.Windows.Forms.Button();
            this.ListTestSample = new System.Windows.Forms.ListView();
            this.stt = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.audioName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.timestamp = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.playAudio = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.status = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.text = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.VM_VolumeMeter = new NAudio.Gui.VolumeMeter();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbl_mic
            // 
            this.lbl_mic.AutoSize = true;
            this.lbl_mic.Location = new System.Drawing.Point(11, 22);
            this.lbl_mic.Name = "lbl_mic";
            this.lbl_mic.Size = new System.Drawing.Size(24, 13);
            this.lbl_mic.TabIndex = 0;
            this.lbl_mic.Text = "Mic";
            // 
            // CB_ListMic
            // 
            this.CB_ListMic.FormattingEnabled = true;
            this.CB_ListMic.Location = new System.Drawing.Point(52, 19);
            this.CB_ListMic.Name = "CB_ListMic";
            this.CB_ListMic.Size = new System.Drawing.Size(121, 21);
            this.CB_ListMic.TabIndex = 1;
            this.CB_ListMic.SelectedIndexChanged += new System.EventHandler(this.CB_ListMic_SelectedIndexChanged);
            // 
            // btn_test
            // 
            this.btn_test.Location = new System.Drawing.Point(190, 17);
            this.btn_test.Name = "btn_test";
            this.btn_test.Size = new System.Drawing.Size(75, 23);
            this.btn_test.TabIndex = 2;
            this.btn_test.Text = "Test";
            this.btn_test.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btn_test.UseVisualStyleBackColor = true;
            this.btn_test.Click += new System.EventHandler(this.btn_test_Click);
            // 
            // ListTestSample
            // 
            this.ListTestSample.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.stt,
            this.audioName,
            this.timestamp,
            this.playAudio,
            this.status,
            this.text});
            this.ListTestSample.Font = new System.Drawing.Font("Times New Roman", 10.2F);
            this.ListTestSample.FullRowSelect = true;
            this.ListTestSample.GridLines = true;
            this.ListTestSample.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.ListTestSample.HideSelection = false;
            this.ListTestSample.Location = new System.Drawing.Point(2, 59);
            this.ListTestSample.Name = "ListTestSample";
            this.ListTestSample.Size = new System.Drawing.Size(951, 516);
            this.ListTestSample.TabIndex = 3;
            this.ListTestSample.UseCompatibleStateImageBehavior = false;
            this.ListTestSample.View = System.Windows.Forms.View.Details;
            this.ListTestSample.DoubleClick += new System.EventHandler(this.ListTestSample_DoubleClick);
            this.ListTestSample.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.ListTestSample_MouseDoubleClick);
            // 
            // stt
            // 
            this.stt.Text = "STT";
            this.stt.Width = 50;
            // 
            // audioName
            // 
            this.audioName.Text = "Tên";
            this.audioName.Width = 103;
            // 
            // timestamp
            // 
            this.timestamp.Text = "Thời Gian";
            this.timestamp.Width = 84;
            // 
            // playAudio
            // 
            this.playAudio.Text = "Âm thanh";
            this.playAudio.Width = 85;
            // 
            // status
            // 
            this.status.Text = "Trạng thái";
            this.status.Width = 113;
            // 
            // text
            // 
            this.text.Text = "Văn bản";
            this.text.Width = 645;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.CB_ListMic);
            this.groupBox1.Controls.Add(this.lbl_mic);
            this.groupBox1.Controls.Add(this.btn_test);
            this.groupBox1.Location = new System.Drawing.Point(2, 1);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(283, 52);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Điều khiển";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.VM_VolumeMeter);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(300, 1);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(315, 52);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Kết quả";
            // 
            // VM_VolumeMeter
            // 
            this.VM_VolumeMeter.Amplitude = 0F;
            this.VM_VolumeMeter.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.VM_VolumeMeter.Location = new System.Drawing.Point(60, 17);
            this.VM_VolumeMeter.MaxDb = 18F;
            this.VM_VolumeMeter.MinDb = -60F;
            this.VM_VolumeMeter.Name = "VM_VolumeMeter";
            this.VM_VolumeMeter.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.VM_VolumeMeter.Size = new System.Drawing.Size(200, 23);
            this.VM_VolumeMeter.TabIndex = 0;
            this.VM_VolumeMeter.Text = "volumeMeter1";
            this.VM_VolumeMeter.DoubleClick += new System.EventHandler(this.VM_VolumeMeter_DoubleClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Âm lượng";
            // 
            // AudioTesting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(954, 577);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.ListTestSample);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "AudioTesting";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Audio Testing";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lbl_mic;
        private System.Windows.Forms.ComboBox CB_ListMic;
        private System.Windows.Forms.Button btn_test;
        private System.Windows.Forms.ListView ListTestSample;
        private System.Windows.Forms.ColumnHeader stt;
        private System.Windows.Forms.ColumnHeader audioName;
        private System.Windows.Forms.ColumnHeader timestamp;
        private System.Windows.Forms.ColumnHeader playAudio;
        private System.Windows.Forms.ColumnHeader status;
        private System.Windows.Forms.ColumnHeader text;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private NAudio.Gui.VolumeMeter VM_VolumeMeter;
        private System.Windows.Forms.Label label1;
    }
}