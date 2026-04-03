
namespace Recorder.AppForm
{
    partial class FunctionSelection
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
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(40, 88);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(177, 89);
            this.button1.TabIndex = 0;
            this.button1.Text = "Hop To";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(270, 88);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(159, 89);
            this.button2.TabIndex = 1;
            this.button2.Text = "Hop Hoi Truong";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // FunctionSelection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(572, 247);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "FunctionSelection";
            this.Text = "FunctionSelection";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}