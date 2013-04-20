namespace RedSender
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RedRock));
            this.btnStartRecieving = new System.Windows.Forms.Button();
            this.btCancel = new System.Windows.Forms.Button();
            this.videoSourcePlayer1 = new AForge.Controls.VideoSourcePlayer();
            this.lblStatus = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.prgStatusBar = new System.Windows.Forms.ProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnStartRecieving
            // 
            this.btnStartRecieving.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStartRecieving.BackColor = System.Drawing.Color.Black;
            this.btnStartRecieving.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnStartRecieving.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnStartRecieving.ForeColor = System.Drawing.Color.White;
            this.btnStartRecieving.Location = new System.Drawing.Point(518, 611);
            this.btnStartRecieving.Name = "btnStartRecieving";
            this.btnStartRecieving.Size = new System.Drawing.Size(75, 23);
            this.btnStartRecieving.TabIndex = 0;
            this.btnStartRecieving.Text = "בצע פענוח";
            this.btnStartRecieving.UseVisualStyleBackColor = false;
            this.btnStartRecieving.Click += new System.EventHandler(this.btnStartRecieving_Click);
            // 
            // btCancel
            // 
            this.btCancel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btCancel.BackColor = System.Drawing.Color.Black;
            this.btCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btCancel.ForeColor = System.Drawing.Color.White;
            this.btCancel.Location = new System.Drawing.Point(24, 611);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(75, 23);
            this.btCancel.TabIndex = 1;
            this.btCancel.Text = "בטל פעולה";
            this.btCancel.UseVisualStyleBackColor = false;
            this.btCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // videoSourcePlayer1
            // 
            this.videoSourcePlayer1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.videoSourcePlayer1.Location = new System.Drawing.Point(24, 131);
            this.videoSourcePlayer1.Name = "videoSourcePlayer1";
            this.videoSourcePlayer1.Size = new System.Drawing.Size(569, 374);
            this.videoSourcePlayer1.TabIndex = 2;
            this.videoSourcePlayer1.Text = "videoSourcePlayer1";
            this.videoSourcePlayer1.VideoSource = null;
            // 
            // lblStatus
            // 
            this.lblStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStatus.Location = new System.Drawing.Point(24, 538);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(569, 70);
            this.lblStatus.TabIndex = 3;
            this.lblStatus.Text = "נקלט:";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::RedSender.Properties.Resources.redrock__1_;
            this.pictureBox1.Location = new System.Drawing.Point(150, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(318, 106);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // prgStatusBar
            // 
            this.prgStatusBar.Location = new System.Drawing.Point(24, 512);
            this.prgStatusBar.Name = "prgStatusBar";
            this.prgStatusBar.Size = new System.Drawing.Size(566, 23);
            this.prgStatusBar.TabIndex = 5;
            this.prgStatusBar.Visible = false;
            // 
            // RedRock
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(619, 646);
            this.Controls.Add(this.prgStatusBar);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.videoSourcePlayer1);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.btnStartRecieving);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "RedRock";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Text = "הסלע האדום";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnStartRecieving;
        private System.Windows.Forms.Button btCancel;
        private AForge.Controls.VideoSourcePlayer videoSourcePlayer1;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ProgressBar prgStatusBar;

    }
}

