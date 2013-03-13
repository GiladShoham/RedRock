using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.IO;
using System.Drawing;
using com.google.zxing.qrcode;
using com.google.zxing;
using com.google.zxing.common;

namespace RedSender
{
    class FileProcessor
    {
        public Boolean stop_executing = false;
        public static readonly List<string> ImageExtensions = new List<string> { ".JPG", ".JPE", ".BMP", ".GIF", ".PNG" };

        public void ProcessFiles()
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
                    if (!this.stop_executing &&
                        ImageExtensions.Contains(Path.GetExtension(strPic).ToUpperInvariant()))
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
                RedSender.RedRock.EndProcessAnnounce();
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

            string sdecodedString = decoder.decode(binBitmap, null).Text;
            //Byte[] result = decoder.decode(binBitmap, null).;

            //return null;

            return sdecodedString;
        }
    
    }
}
