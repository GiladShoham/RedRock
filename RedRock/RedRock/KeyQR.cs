using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RedRock
{
    public partial class KeyQR : Form
    {
        public KeyQR(Bitmap qr)
        {
            InitializeComponent();
            //qr.Save("gilad.bmp");
            qr.Save("gilad.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
            //this.keyQRPic.Image = ((System.Drawing.Image)(Image.FromFile("gilad.jpg")));
            ///keyQRPic.Image = null;
            //keyQRPic.Image = Image.FromFile("gilad.jpg");
            keyQRPic.Image = (Image) qr;
            
        }
    }
}
