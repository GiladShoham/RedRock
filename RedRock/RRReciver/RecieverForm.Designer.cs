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
            this.SuspendLayout();
            // 
            // btnStartRecieving
            // 
            this.btnStartRecieving.Location = new System.Drawing.Point(104, 46);
            this.btnStartRecieving.Name = "btnStartRecieving";
            this.btnStartRecieving.Size = new System.Drawing.Size(75, 23);
            this.btnStartRecieving.TabIndex = 0;
            this.btnStartRecieving.Text = "בצע פענוח";
            this.btnStartRecieving.UseVisualStyleBackColor = true;
            this.btnStartRecieving.Click += new System.EventHandler(this.btnStartRecieving_Click);
            // 
            // RedRock
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 126);
            this.Controls.Add(this.btnStartRecieving);
            this.Name = "RedRock";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Text = "הסלע האדום";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnStartRecieving;

    }
}
