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
using IEC16022Sharp;
using System.Threading;
using com.google.zxing.datamatrix.detector;

namespace RedSender
{
    public partial class RedRock : Form
    {
        private Boolean stop_executing = false;
        private FileProcessor mDoAction; 

        public RedRock()
        {
            InitializeComponent();

            this.mDoAction = new FileProcessor();
            this.mDoAction.stop_executing = false;
           }

        private void btnStartRecieving_Click(object sender, EventArgs e)
        { /*
            Thread backgroundThread =
                new Thread(new ThreadStart(this.mDoAction.ProcessFiles));
            backgroundThread.Name = "BackgroundThread";
            backgroundThread.IsBackground = true;

            backgroundThread.Start();

            MessageBox.Show("מבוצעת קליטה");*/
            this.ProcessFiles();
        }

        public static void EndProcessAnnounce()
        {
            MessageBox.Show("קליטה בוצעה בהצלחה!");
        }

        private void ProcessFiles()
        {
            string sPicPath = @"C:\Users\Nitzan\Desktop\RedRock\Picture";

            string sFileResultType = String.Empty;

            Hashtable htAllPictureParts = new Hashtable();
            int nAddedPictures = 0;
            int nLastPicture = -1;
            int nTotalLenght = 0;

            while (!stop_executing &&
                   (nLastPicture == -1 || nAddedPictures < nLastPicture + 1)
                  )
            {
                string[] filePaths = Directory.GetFiles(@sPicPath, "*.*");

                foreach (string strPic in filePaths)
                {
                  /*  if (!this.stop_executing &&
                        ImageExtensions.Contains(Path.GetExtension(strPic).ToUpperInvariant()))*/
                    {
                        // Load the picture
                        Bitmap image = (Bitmap)Image.FromFile(strPic);

                        /*ImageConverter converter = new ImageConverter();
                        Byte[] DecodedArr = (byte[])converter.ConvertTo(image, typeof(byte[]));

                        Byte[] FileIndex = new Byte[8];

                        System.Buffer.BlockCopy(DecodedArr, 0, FileIndex, 0, 8);

                        string sDecodedPicture = System.Text.Encoding.ASCII.GetString(FileIndex);
                        */

                        string sDecodedPicture = this.QRDecode(image, new QRCodeReader());

                        // Split the decoded string to the picture and picture index
                        int nSpace = sDecodedPicture.IndexOf(' ');
                        string sPicIndex = sDecodedPicture.Substring(0, nSpace);
                        string sPicture = sDecodedPicture.Remove(0, nSpace + 1);

                        // If it is the last file - it will have *GIF* ending
                        int nStarIndex = sPicIndex.IndexOf('*');

                        if (nStarIndex != -1)
                        {
                            int nSecondStarIndex = sPicIndex.IndexOf('*', nStarIndex + 1);
                            sFileResultType = sPicIndex.Substring(nStarIndex + 1, nSecondStarIndex - nStarIndex - 1);
                            sPicIndex = sPicIndex.Substring(0, nStarIndex);
                            nLastPicture = int.Parse(sPicIndex);
                        }

                        int nPicIndex;

                        try
                        {
                            nPicIndex = int.Parse(sPicIndex);
                        }
                        catch (Exception E)
                        {
                            throw new Exception("Picture format not valid");
                        }

                        if (!htAllPictureParts.ContainsKey(nPicIndex))
                        {
                            //Byte[] btPic = Encoding.ASCII.GetBytes(sPicture);
                            Byte[] btPic = Convert.FromBase64String(sPicture.Trim());

                            htAllPictureParts.Add(nPicIndex, btPic);
                            ++nAddedPictures;

                            nTotalLenght = nTotalLenght + btPic.Length;
                        }

                        image.Dispose();
                    }
                    // delete the picture when closes
                    //        System.IO.File.Delete(sPicPath);
                }
            }
            if (!stop_executing)
            {
                this.CreateResultFile(htAllPictureParts, nTotalLenght, sFileResultType);
            }
            else
            {
                this.stop_executing = false;
            }
        }


        private void CreateResultFile(Hashtable htPictures, int nTotalLenght, string sFileType)
        {
            ByteArray arr = new ByteArray();

            byte[] rv = new byte[nTotalLenght];
            int offset = 0;
            for (int i = 0; i <= htPictures.Count - 1; i++)
            {
                Byte[] array = (Byte[])htPictures[i];
                System.Buffer.BlockCopy(array, 0, rv, offset, array.Length);
                offset += array.Length;
            }

            using (FileStream fs = File.Create(@"C:\Users\Nitzan\Desktop\RedRock\Result\Result" + sFileType))
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

            string sdecodedString = string.Empty;

                LuminanceSource source = new RGBLuminanceSource(image, image.Width, image.Height);
                var rgb = new HybridBinarizer(source);
                var binBitmap = new BinaryBitmap(rgb);
                
                BitMatrix bm = binBitmap.BlackMatrix;

				
		



                 /*   
                Detector detector = new Detector(bm);
                DetectorResult result = detector.detect();

                string retStr = "Found at points ";
                BitMatrix QRImageData = result.Bits;
                  */
/*
                image m = QRImageData;
                LuminanceSource source = new RGBLuminanceSource(QRImageData, image.Width, image.Height);
                var rgb = new HybridBinarizer(source);
                var binBitmap = new BinaryBitmap(rgb);


                BinaryBitmap m = new BinaryBitmap(

                sdecodedString = decoder.decode(().Text;*/
  


            
            //Byte[] result = decoder.decode(binBitmap, null).;

            //return null;

            return sdecodedString;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.mDoAction.stop_executing = true;
        }
    }
}
