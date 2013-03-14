namespace RedRock
{
    partial class KeyQR
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
            this.keyQRPic = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.keyQRPic)).BeginInit();
            this.SuspendLayout();
            // 
            // keyQRPic
            // 
            this.keyQRPic.Location = new System.Drawing.Point(57, 36);
            this.keyQRPic.Name = "keyQRPic";
            this.keyQRPic.Size = new System.Drawing.Size(570, 570);
            this.keyQRPic.TabIndex = 0;
            this.keyQRPic.TabStop = false;
            // 
            // KeyQR
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(723, 667);
            this.Controls.Add(this.keyQRPic);
            this.Name = "KeyQR";
            this.Text = "KeyQR";
            ((System.ComponentModel.ISupportInitialize)(this.keyQRPic)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox keyQRPic;
    }
}