using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Crypto;

namespace RedRock
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void source_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Console.WriteLine("djasf");
            String text = "gilad";
            String key = "gil";
            //String wrongKey = "afsdafs";
            this.source.Text = text;

            String encrypted = EncDec.Encrypt(text, key);
            String signed = EncDec.Sign(encrypted);
            this.sigend.Text = signed;
            this.encrypted.Text = encrypted;
            this.decrypted.Text = EncDec.Decrypt(encrypted, key);
           // this.decryptedWrong.Text = EncDec.Decrypt(encrypted, wrongKey);

        }
    }
}
