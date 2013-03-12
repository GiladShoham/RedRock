using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using com.google.zxing.qrcode.encoder;
using com.google.zxing.qrcode;
using com.google.zxing;
using com.google.zxing.common;

namespace RRReciver
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnCreateCode_Click(object sender, EventArgs e)
        {
            QRCodeWriter qcCode = new QRCodeWriter();
            ByteMatrix btMatrix = qcCode.encode("mayan cohen tomer shohet gilad soham mayan cohen tomer shohet gilad soham mayan cohen tomer shohet gilad soham ", BarcodeFormat.QR_CODE, 200, 200);
            Bitmap bmpOutput = btMatrix.ToBitmap();

            picOutput.Image = bmpOutput;

        }
    }
}
