﻿namespace RedSender
{
    partial class RedRock
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
            this.btnStartRecieving = new System.Windows.Forms.Button();
            this.btCancel = new System.Windows.Forms.Button();
            this.videoSourcePlayer1 = new AForge.Controls.VideoSourcePlayer();
            this.lblStatus = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnStartRecieving
            // 
            this.btnStartRecieving.Location = new System.Drawing.Point(518, 506);
            this.btnStartRecieving.Name = "btnStartRecieving";
            this.btnStartRecieving.Size = new System.Drawing.Size(75, 23);
            this.btnStartRecieving.TabIndex = 0;
            this.btnStartRecieving.Text = "בצע פענוח";
            this.btnStartRecieving.UseVisualStyleBackColor = true;
            this.btnStartRecieving.Click += new System.EventHandler(this.btnStartRecieving_Click);
            // 
            // btCancel
            // 
            this.btCancel.Location = new System.Drawing.Point(24, 506);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(75, 23);
            this.btCancel.TabIndex = 1;
            this.btCancel.Text = "בטל פעולה";
            this.btCancel.UseVisualStyleBackColor = true;
            this.btCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // videoSourcePlayer1
            // 
            this.videoSourcePlayer1.Location = new System.Drawing.Point(24, 26);
            this.videoSourcePlayer1.Name = "videoSourcePlayer1";
            this.videoSourcePlayer1.Size = new System.Drawing.Size(569, 374);
            this.videoSourcePlayer1.TabIndex = 2;
            this.videoSourcePlayer1.Text = "videoSourcePlayer1";
            this.videoSourcePlayer1.VideoSource = null;
            // 
            // lblStatus
            // 
            this.lblStatus.Location = new System.Drawing.Point(24, 402);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(569, 101);
            this.lblStatus.TabIndex = 3;
            this.lblStatus.Text = "נקלט:";
            // 
            // RedRock
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(619, 541);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.videoSourcePlayer1);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.btnStartRecieving);
            this.Name = "RedRock";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Text = "הסלע האדום";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnStartRecieving;
        private System.Windows.Forms.Button btCancel;
        private AForge.Controls.VideoSourcePlayer videoSourcePlayer1;
        private System.Windows.Forms.Label lblStatus;

    }
}

