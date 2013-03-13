using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using com.google.zxing;
using com.google.zxing.qrcode.decoder;
using com.google.zxing.common;
using com.google.zxing.qrcode;
using System.Collections;

namespace RedSender
{
    public partial class RedRock : Form
    {
        public RedRock()
        {
            InitializeComponent();
        }

        private void btnStartRecieving_Click(object sender, EventArgs e)
        {
            string sPicPath = @"C:\Users\Nitzan\Desktop\RedRock\Picture";

            

            Hashtable htAllPictureParts = new Hashtable();
            int       nAddedPictures = 0;
            int       nLastPicture   = -1;
            int       nTotalLenght   = 0;

            while (nLastPicture == -1 || nAddedPictures < nLastPicture + 1)
            {
                string[] filePaths = Directory.GetFiles(@sPicPath, "*.bmp");

                foreach (string strPic in filePaths)
                {
                    // Load the picture
                    Bitmap image = (Bitmap)Image.FromFile(strPic);
                    string sDecodedPicture = this.QRDecode(image, new QRCodeReader());

                    // Split the decoded string to the picture and picture index
                    int nSpace = sDecodedPicture.IndexOf(' ');
                    string sPicIndex = sDecodedPicture.Substring(0, nSpace);
                    string sPicture = sDecodedPicture.Remove(0, nSpace + 1);

                    // If it is the last file - it will have *GIF* ending
                    int nStarIndex = sPicIndex.IndexOf('*');

                    if (nStarIndex != -1)
                    {
                        sPicIndex = sPicIndex.Remove(nStarIndex, sPicIndex.Length - nStarIndex);
                        nLastPicture = int.Parse(sPicIndex);
                    }

                    int nPicIndex = int.Parse(sPicIndex);

                    if (!htAllPictureParts.ContainsKey(nPicIndex))
                    {
                        Byte[] btPic = Encoding.ASCII.GetBytes(sPicture);

                        File.WriteAllBytes(@"C:\Users\Nitzan\Desktop\RedRock\Result\.GIF", btPic);

                        htAllPictureParts.Add(nPicIndex, btPic);
                        ++nAddedPictures;

                        nTotalLenght = nTotalLenght + btPic.Length;
                    }

                    image.Dispose();
                }
                // delete the picture when closes
        //        System.IO.File.Delete(sPicPath);
            }

            this.CreateResultFile(htAllPictureParts, nTotalLenght);
        }


        private void CreateResultFile(Hashtable htPictures, int nTotalLenght)
        {
            ByteArray arr = new ByteArray();

            byte[] rv = new byte[nTotalLenght];
            int offset = 0;
            for (int i = 0; i <= htPictures.Count - 1; i++)
            {
                Byte[] array = (Byte[]) htPictures[i];
                System.Buffer.BlockCopy(array, 0, rv, offset, array.Length);
                offset += array.Length;
            }

            this.SavePicture(rv);
        }

        private void SavePicture(byte[] arrDetails)
        {
            File.WriteAllBytes(@"C:\Users\Nitzan\Desktop\RedRock\Result\Result.GIF", arrDetails);
            /*
            using (FileStream fs = File.Create(@"C:\Users\Nitzan\Desktop\RedRock\Result\Result.GIF"))
            {
                foreach (Byte b in arrDetails)
                {
                    
                    fs.WriteByte(b);
                }
            }*/

            using (FileStream fs = File.Create(@"C:\Users\Nitzan\Desktop\RedRock\Result\Result.txt"))
            {
                foreach (Byte b in arrDetails)
                {
                    fs.WriteByte(b);
                }
            }
            /*
            MemoryStream ms = new MemoryStream(arrDetails);
            Image returnImage = Image.FromStream(ms);

            
                Bitmap bmp1 = (Bitmap) returnImage;

                // Save the image as a GIF.
                bmp1.Save(@"C:\Users\Nitzan\Desktop\RedRock\Result\Result.gif", System.Drawing.Imaging.ImageFormat.Gif);
            */
        }

        private string QRDecode(Bitmap image, Reader decoder)
        {
            var rgb = new RGBLuminanceSource(image, image.Width, image.Height);
            var hybrid = new HybridBinarizer(rgb);

            BinaryBitmap binBitmap = new BinaryBitmap(hybrid);
            string sdecodedString = decoder.decode(binBitmap, null).Text;

            return sdecodedString;
        }
    }
}
