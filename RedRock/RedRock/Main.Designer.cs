namespace RedRock
{
    partial class Main
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
            this.source = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.sigend = new System.Windows.Forms.TextBox();
            this.encrypted = new System.Windows.Forms.TextBox();
            this.decrypted = new System.Windows.Forms.TextBox();
            this.decryptedWrong = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.unsigned = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(37, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "source";
            // 
            // source
            // 
            this.source.Location = new System.Drawing.Point(112, 40);
            this.source.Name = "source";
            this.source.Size = new System.Drawing.Size(100, 20);
            this.source.TabIndex = 1;
            this.source.TextChanged += new System.EventHandler(this.source_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(37, 84);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "encrypt";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(37, 124);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "sigend";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(41, 254);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "decrypte";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 298);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(80, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "decrypteWrong";
            // 
            // sigend
            // 
            this.sigend.Location = new System.Drawing.Point(112, 124);
            this.sigend.Multiline = true;
            this.sigend.Name = "sigend";
            this.sigend.Size = new System.Drawing.Size(257, 45);
            this.sigend.TabIndex = 6;
            // 
            // encrypted
            // 
            this.encrypted.Location = new System.Drawing.Point(112, 77);
            this.encrypted.Multiline = true;
            this.encrypted.Name = "encrypted";
            this.encrypted.Size = new System.Drawing.Size(257, 41);
            this.encrypted.TabIndex = 7;
            // 
            // decrypted
            // 
            this.decrypted.Location = new System.Drawing.Point(112, 254);
            this.decrypted.Name = "decrypted";
            this.decrypted.Size = new System.Drawing.Size(100, 20);
            this.decrypted.TabIndex = 8;
            // 
            // decryptedWrong
            // 
            this.decryptedWrong.Location = new System.Drawing.Point(112, 291);
            this.decryptedWrong.Name = "decryptedWrong";
            this.decryptedWrong.Size = new System.Drawing.Size(100, 20);
            this.decryptedWrong.TabIndex = 9;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(112, 322);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 10;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // unsigned
            // 
            this.unsigned.Location = new System.Drawing.Point(112, 176);
            this.unsigned.Multiline = true;
            this.unsigned.Name = "unsigned";
            this.unsigned.Size = new System.Drawing.Size(257, 46);
            this.unsigned.TabIndex = 11;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(37, 176);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(50, 13);
            this.label6.TabIndex = 3;
            this.label6.Text = "unsigend";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(616, 463);
            this.Controls.Add(this.unsigned);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.decryptedWrong);
            this.Controls.Add(this.decrypted);
            this.Controls.Add(this.encrypted);
            this.Controls.Add(this.sigend);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.source);
            this.Controls.Add(this.label1);
            this.Name = "Main";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox source;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox sigend;
        private System.Windows.Forms.TextBox encrypted;
        private System.Windows.Forms.TextBox decrypted;
        private System.Windows.Forms.TextBox decryptedWrong;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox unsigned;
        private System.Windows.Forms.Label label6;
    }
}

