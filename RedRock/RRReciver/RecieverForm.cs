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

            string sFileResultType = String.Empty;

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

                    ImageConverter converter = new ImageConverter();
                    Byte[] DecodedArr = (byte[])converter.ConvertTo(image, typeof(byte[]));

                    Byte[] FileIndex = new Byte[8];

                    System.Buffer.BlockCopy(DecodedArr, 0, FileIndex, 0, 8);

                    string sDecodedPicture = System.Text.Encoding.ASCII.GetString(FileIndex);

                    // Split the decoded string to the picture and picture index
                    int nSpace = sDecodedPicture.IndexOf(' ');
                    string sPicIndex = sDecodedPicture.Substring(0, nSpace);
                    string sPicture = sDecodedPicture.Remove(0, nSpace + 1);

                    // If it is the last file - it will have *GIF* ending
                    int nStarIndex = sPicIndex.IndexOf('*');

                    if (nStarIndex != -1)
                    {
                        sFileResultType = sPicIndex.Substring(nStarIndex + 1, sPicIndex.Length - nStarIndex - 1);
                        sPicIndex = sPicIndex.Substring(0, nStarIndex);
                        nLastPicture = int.Parse(sPicIndex);
                    }

                    int nPicIndex;

                    try
                    {
                        nPicIndex= int.Parse(sPicIndex);
                    }
                    catch( Exception E)
                    {
                        throw new Exception("Picture format not valid");
                    }

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

            this.CreateResultFile(htAllPictureParts, nTotalLenght, sFileResultType);
        }


        private void CreateResultFile(Hashtable htPictures, int nTotalLenght, string sFileType)
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

            using (FileStream fs = File.Create(@"C:\Users\Nitzan\Desktop\RedRock\Result\Result." + sFileType))
            {
                foreach (Byte b in rv)
                {
                    fs.WriteByte(b);
                }
            }

            MessageBox.Show("saved seccessfully");
        }

        private string QRDecode(Bitmap image, Reader decoder)
        {
            /*
            MemoryStream ms = new MemoryStream();
            image.Save(ms,System.Drawing.Imaging.ImageFormat.Bmp);

            DataMatrix dmDecoder = new DataMatrix(ms.ToArray(), image.Width, image.Height, EncodingType.Binary);

            return dmDecoder.HexPbm;
             * */
            

            
            var rgb = new RGBLuminanceSource(image, image.Width, image.Height);
            var hybrid = new HybridBinarizer(rgb);

            BinaryBitmap binBitmap = new BinaryBitmap(hybrid);
           // string sdecodedString = decoder.decode(binBitmap, null).RawBytes;
            //Byte[] result = decoder.decode(binBitmap, null).;

            //return null;

            return null;
        }
    }
}
