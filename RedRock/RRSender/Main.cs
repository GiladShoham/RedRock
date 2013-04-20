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
using System.IO;
using Gif.Components;
using System.Drawing.Imaging;
using SevenZip;
using System.Collections;
using com.google.zxing.multi.qrcode;
using com.google.zxing.datamatrix.detector;
using System.Diagnostics;

namespace RRReciver
{
    public partial class Main : Form
    {
        #region Data Members

        // Const Members
        public const int BYTES_IN_FRAME        = 100;
        public const int NUM_OF_SEQUENCE_DIGIT = 8;
        public const int QR_HIGHET             = 0;
        public const int QR_WIDTH              = 0;
        public const int QR_IN_FRAME           = 2;
        public const int FRAME_RATE            = 8;

        
        
        
        // Data Member
        private string OUTPUT_FOLDER_BMPS;
        private string OUTPUT_FOLDER_TXT;
        private string OUTPUT_FOLDER_GIF;
        private string OUTPUT_FOLDER_BASE;
        private string OUTPUT_FOLDER_COMPRASS;

        private string m_strCurrFileLocation = string.Empty;
        private string m_strFileName         = string.Empty;
        private int    m_nCurrTestNum        = 0;

        #endregion

        #region Properties

        #endregion

        #region Methods

        public Main()
        {
            InitializeComponent();

            this.CreateFolders();
        }

        #endregion

        #region Event Methods

        private void CreateFolders()
        {
            OUTPUT_FOLDER_BASE = Path.GetTempPath();

            this.m_nCurrTestNum = (Directory.GetDirectories(OUTPUT_FOLDER_BASE).Length + 1);
            string strTestCounter = this.m_nCurrTestNum + @"\";

            OUTPUT_FOLDER_BMPS = OUTPUT_FOLDER_BASE + strTestCounter + "BMP\\";
            OUTPUT_FOLDER_TXT = OUTPUT_FOLDER_BASE + strTestCounter + "TXT\\";
            OUTPUT_FOLDER_GIF = OUTPUT_FOLDER_BASE + strTestCounter + "GIF\\";
            OUTPUT_FOLDER_COMPRASS = OUTPUT_FOLDER_BASE + strTestCounter + "COMPRASS\\";

            Directory.CreateDirectory(OUTPUT_FOLDER_BASE + strTestCounter);
            
            Directory.CreateDirectory(OUTPUT_FOLDER_BMPS);
            Directory.CreateDirectory(OUTPUT_FOLDER_GIF);
            Directory.CreateDirectory(OUTPUT_FOLDER_TXT);
            Directory.CreateDirectory(OUTPUT_FOLDER_COMPRASS);
        }

        private void btnCreateCode_Click(object sender, EventArgs e)
        {
            this.FileToQRCode(this.txtFilePath.Text);
        }


        /// <summary>
        /// The method get a file, comprass it, change it to binary and decode it to qrcode image
        /// </summary>
        /// <param name="OriginalFile"></param>
        /// <returns></returns>
        public string FileToQRCode(string OriginalFile, bool bComprassImage)
        {
            string strOrginName = OriginalFile;
            if (bComprassImage)
            {
                string strExtension = Path.GetExtension(OriginalFile).ToUpper().Substring(1,3);
                // Check the file type
                if ((strExtension == "DOC") || (strExtension == "DOCX"))
                {
                    string strTempPathWithFile = OUTPUT_FOLDER_COMPRASS + "\\" + Path.GetFileName(strOrginName);
                    File.Move(strOrginName, strTempPathWithFile);
                    strOrginName = DocToRTF.ConvertDOCtoRTF(strTempPathWithFile);
                }
                else if ((strExtension == "PNG") || (strExtension == "JPG") || (strExtension == "BMP") || (strExtension == "GIF") || (strExtension == "TIF"))
                {

                    Bitmap btBitmap = new Bitmap(OriginalFile);
                    int nNewWidth = 100;
                    int nNewHeight = 200;

                    float fProprtion = btBitmap.Width / nNewWidth;
                    if (btBitmap.Height > nNewWidth * 2)
                    {
                        fProprtion = btBitmap.Height / nNewHeight;
                        nNewWidth = int.Parse(Math.Round((float)btBitmap.Width / fProprtion).ToString());
                    }
                    else
                    {
                        nNewHeight = int.Parse(Math.Round((float)btBitmap.Height / fProprtion).ToString());
                    }

                    string strNewFile = CreateComprassFile(btBitmap, new Size(nNewWidth, nNewHeight));

                    // Rename the file
                    strOrginName = OUTPUT_FOLDER_COMPRASS + Path.GetFileNameWithoutExtension(OriginalFile) + Path.GetExtension(strNewFile);
                    File.Move(strNewFile, strOrginName);
                }
            }

            return (this.FileToQRCode(strOrginName));
        }

        /// <summary>
        /// The method get a file, comprass it, change it to binary and decode it to qrcode image
        /// </summary>
        /// <param name="OriginalFile"></param>
        /// <returns></returns>
        public string FileToQRCode(string OriginalFile)
        {
            // Set the curr file details to the data members
            m_strCurrFileLocation = OriginalFile;
            m_strFileName = Path.GetFileName(m_strCurrFileLocation);

            // Create file
            File.Copy(m_strCurrFileLocation, OUTPUT_FOLDER_BASE + this.m_nCurrTestNum + "\\" + Path.GetFileName(m_strCurrFileLocation));
            m_strCurrFileLocation = OUTPUT_FOLDER_BASE + this.m_nCurrTestNum + "\\" + Path.GetFileName(m_strCurrFileLocation);


            // Comprass the file
            string strZipFile = this.Comprass(m_strCurrFileLocation);
            byte[] btFullOrigianlArray = File.ReadAllBytes(strZipFile);

            // Create the bitmap array
            Decimal dcNumOfFrame = Decimal.Ceiling(Decimal.Parse(btFullOrigianlArray.Length.ToString()) / Decimal.Parse((BYTES_IN_FRAME).ToString()));
            int NumOfFrame = int.Parse(dcNumOfFrame.ToString());
            Bitmap[] arrbpmOutput = new Bitmap[NumOfFrame];

            // Creaate QR code of each frame
            QRCodeWriter qcCode = new QRCodeWriter();
            string strLastStringAddon = string.Empty;
            List<ByteMatrix> lsByteMatrix = new List<ByteMatrix>();

            for (int nCurrFrameNumber = 0; nCurrFrameNumber < NumOfFrame; nCurrFrameNumber++)
            {
                byte[] btOneFrame = null;

                // Check if it is the last string or not
                if (nCurrFrameNumber != NumOfFrame - 1)
                {
                    // If it is not the last string - then it cut the string from the right point in the right size
                    btOneFrame = SubStringArrays<byte>(btFullOrigianlArray, nCurrFrameNumber * BYTES_IN_FRAME, BYTES_IN_FRAME);
                }
                else
                {
                    // If it is the last string - cut from the right point till the end
                    btOneFrame = SubStringArrays<byte>(btFullOrigianlArray, nCurrFrameNumber * BYTES_IN_FRAME);
                    strLastStringAddon = "*" + Path.GetExtension(m_strFileName) + "*";
                }

                // Add the number (with the 0 if it is only one digit) to the start of the image
                string strOneFrame = string.Format("{0:00}", nCurrFrameNumber) + strLastStringAddon + " " + Convert.ToBase64String(btOneFrame);
                int output_size = (((BYTES_IN_FRAME * 4) / 3) + (BYTES_IN_FRAME / 96) - 1);

                // Encode the sting to QR code
                
                strOneFrame = strOneFrame.PadRight(output_size, ' ');
                ByteMatrix btMatrix = qcCode.encode(strOneFrame, BarcodeFormat.QR_CODE, QR_WIDTH, QR_HIGHET);

                lsByteMatrix.Add(btMatrix);

            }

            CreateBitmapFile(lsByteMatrix);

            // Create animited gif from bitmap
            string strGIFFileName = this.CreateAnimitedGif();

            return (strGIFFileName);
        }

        private void CreateBitmapFile(List<ByteMatrix> lsByteMatrix)
        {
            List<Bitmap> lsBitmap = new List<Bitmap>();

            for (int nCurrBMPNumber = 0; nCurrBMPNumber < lsByteMatrix.Count; nCurrBMPNumber++) 
            {
                ByteMatrix btMatrix = lsByteMatrix[nCurrBMPNumber];
                // Create an Encoder object based on the GUID 
                // for the Quality parameter category.
                System.Drawing.Imaging.Encoder myEncoder =
                    System.Drawing.Imaging.Encoder.Quality;

                // Create an EncoderParameters object. 
                // An EncoderParameters object has an array of EncoderParameter 
                // objects. In this case, there is only one 
                // EncoderParameter object in the array.
                EncoderParameters myEncoderParameters = new EncoderParameters(1);

                EncoderParameter myEncoderParameter = new EncoderParameter(myEncoder, 100L);
                myEncoderParameters.Param[0] = myEncoderParameter;

                // Save the bitmap in the array for saving to compla
                lsBitmap.Add(btMatrix.ToBitmap());

                // 
                if ((lsBitmap.Count == QR_IN_FRAME) || (nCurrBMPNumber + 1 == lsByteMatrix.Count))
                {
                    Bitmap bmpFinall = null;
                    if (lsBitmap.Count == 6)
                    {
                        int nWidth = Math.Max((Math.Max(lsBitmap[0].Width + lsBitmap[1].Width, lsBitmap[2].Width + lsBitmap[3].Width)),lsBitmap[4].Width + lsBitmap[5].Width);
                        int nHeight = Math.Max(lsBitmap[0].Height + lsBitmap[2].Height + lsBitmap[4].Height, lsBitmap[1].Height + lsBitmap[3].Height + lsBitmap[5].Height);
                        bmpFinall = new Bitmap(nWidth, nHeight);


                        Graphics g = Graphics.FromImage(bmpFinall);
                        g.DrawImage(lsBitmap[0], 0, 0);
                        g.DrawImage(lsBitmap[1], lsBitmap[0].Width, 0);
                        g.DrawImage(lsBitmap[2], 0, lsBitmap[0].Height);
                        g.DrawImage(lsBitmap[3], lsBitmap[0].Width, lsBitmap[0].Height);
                        g.DrawImage(lsBitmap[4], 0, lsBitmap[0].Height + lsBitmap[2].Height);
                        g.DrawImage(lsBitmap[5], lsBitmap[0].Width, lsBitmap[1].Height + lsBitmap[3].Height);
                    }
                    if (lsBitmap.Count == 5)
                    {
                        int nWidth = Math.Max((Math.Max(lsBitmap[0].Width + lsBitmap[1].Width, lsBitmap[2].Width + lsBitmap[3].Width)), lsBitmap[4].Width);
                        int nHeight = Math.Max(lsBitmap[0].Height + lsBitmap[2].Height + lsBitmap[4].Height, lsBitmap[1].Height + lsBitmap[3].Height);
                        bmpFinall = new Bitmap(nWidth, nHeight);

                        Graphics g = Graphics.FromImage(bmpFinall);
                        g.DrawImage(lsBitmap[0], 0, 0);
                        g.DrawImage(lsBitmap[1], lsBitmap[0].Width, 0);
                        g.DrawImage(lsBitmap[2], 0, lsBitmap[0].Height);
                        g.DrawImage(lsBitmap[3], lsBitmap[0].Width, lsBitmap[0].Height);
                        g.DrawImage(lsBitmap[4], 0, lsBitmap[0].Height + lsBitmap[2].Height);
                    }
                    if (lsBitmap.Count == 4)
                    {
                        int nWidth = Math.Max(lsBitmap[0].Width + lsBitmap[1].Width, lsBitmap[2].Width + lsBitmap[3].Width);
                        int nHeight = Math.Max(lsBitmap[0].Height + lsBitmap[2].Height, lsBitmap[1].Height + lsBitmap[3].Height);
                        bmpFinall = new Bitmap(nWidth, nHeight);


                        Graphics g = Graphics.FromImage(bmpFinall);
                        g.DrawImage(lsBitmap[0], 0, 0);
                        g.DrawImage(lsBitmap[1], lsBitmap[0].Width, 0);
                        g.DrawImage(lsBitmap[2], 0, lsBitmap[0].Height);
                        g.DrawImage(lsBitmap[3], lsBitmap[0].Width, lsBitmap[0].Height);
                    }
                    if (lsBitmap.Count == 3)
                    {
                        int nWidth = Math.Max(Math.Max(lsBitmap[0].Width, lsBitmap[2].Width), lsBitmap[1].Width);
                        int nHeight = lsBitmap[0].Height + lsBitmap[1].Height + lsBitmap[2].Height;
                        bmpFinall = new Bitmap(nWidth, nHeight);

                        Graphics g = Graphics.FromImage(bmpFinall);
                        g.DrawImage(lsBitmap[0], 0, 0);
                        g.DrawImage(lsBitmap[1], 0, lsBitmap[0].Height);
                        g.DrawImage(lsBitmap[2], 0, lsBitmap[0].Height + lsBitmap[1].Height);
                    }
                    if (lsBitmap.Count == 2)
                    {
                        int nWidth = Math.Max(lsBitmap[0].Width, lsBitmap[1].Width);
                        int nHeight = lsBitmap[0].Height + lsBitmap[1].Height;
                        bmpFinall = new Bitmap(nWidth, nHeight);

                        Graphics g = Graphics.FromImage(bmpFinall);
                        g.DrawImage(lsBitmap[0], 0, 0);
                        g.DrawImage(lsBitmap[1], 0, lsBitmap[0].Height);
                    }
                    else if (lsBitmap.Count == 1)
                    {
                        bmpFinall = lsBitmap[0];
                    }

                    bmpFinall.Save(OUTPUT_FOLDER_BMPS + nCurrBMPNumber + ".jpg", GetEncoder(ImageFormat.Jpeg), myEncoderParameters);


                    lsBitmap.Clear();
                }
            }
        }

        private string CreateComprassFile(Bitmap btmBitmap, Size szSize)
        {
                // Create an Encoder object based on the GUID 
                // for the Quality parameter category.
                System.Drawing.Imaging.Encoder myEncoder =
                    System.Drawing.Imaging.Encoder.Quality;

                // Create an EncoderParameters object. 
                // An EncoderParameters object has an array of EncoderParameter 
                // objects. In this case, there is only one 
                // EncoderParameter object in the array.
                EncoderParameters myEncoderParameters = new EncoderParameters(1);

                EncoderParameter myEncoderParameter = new EncoderParameter(myEncoder, 30L);
                myEncoderParameters.Param[0] = myEncoderParameter;

                btmBitmap = this.ResizeBitmap(btmBitmap, szSize.Width, szSize.Height);

                btmBitmap.Save(OUTPUT_FOLDER_COMPRASS + "Comprassed" + ".jpg", GetEncoder(ImageFormat.Jpeg), myEncoderParameters);
                btmBitmap.Save(OUTPUT_FOLDER_COMPRASS + "Comprassed" + ".gif", GetEncoder(ImageFormat.Gif), myEncoderParameters);
                btmBitmap.Save(OUTPUT_FOLDER_COMPRASS + "Comprassed" + ".tif", GetEncoder(ImageFormat.Tiff), myEncoderParameters);
                string strReturnValue = FindSmallestFile(OUTPUT_FOLDER_COMPRASS + "Comprassed" + ".jpg", OUTPUT_FOLDER_COMPRASS + "Comprassed" + ".gif", OUTPUT_FOLDER_COMPRASS + "Comprassed" + ".tif");
                return (strReturnValue);
                
        }

        private string FindSmallestFile(params string[] FileList)
        {
            long nSmallest = (new FileInfo(FileList[0]).Length);
            string strSmallesFileName = FileList[0];

            for (int i = 0; i < FileList.Length; i++)
            {
                long nCurrFile = (new FileInfo(FileList[i]).Length);
                if (nCurrFile < nSmallest)
                {
                    nSmallest = nCurrFile;
                    strSmallesFileName = FileList[i];
                }
            }

            return (strSmallesFileName);
        }

        private Bitmap ResizeBitmap(Bitmap sourceBMP, int width, int height )
        {
            Bitmap result = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(result))
                g.DrawImage(sourceBMP, 0, 0, width, height);
            return result;
         }

        private ImageCodecInfo GetEncoder(ImageFormat format)
        {

            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();

            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }

        private string Comprass(string tarPath)
        {
            string strZipPath = Path.GetDirectoryName(m_strCurrFileLocation) + "\\" + Path.GetFileNameWithoutExtension(m_strCurrFileLocation) + ".z7";

            Util.CompressFileLZMA(m_strCurrFileLocation, strZipPath);

            return (strZipPath);
        }

        private string CreateAnimitedGif()
        {
            /* create Gif */
            // you should replace filepath
            
            String outputFilePath = OUTPUT_FOLDER_GIF + "output.gif";

            
            byte[] GifAnimation = { 33, 255, 11, 78, 69, 84, 83, 67, 65, 80, 69, 50, 46, 48, 3, 1, 0, 0, 0 };

            string[] Files = Directory.GetFiles(OUTPUT_FOLDER_BMPS, "*.jpg");
            MemoryStream MS = new MemoryStream();
            BinaryReader BR = new BinaryReader(MS);
            BinaryWriter BW = new BinaryWriter(new FileStream(outputFilePath, FileMode.Create));
            Image.FromFile(Files[0]).Save(MS, ImageFormat.Gif);
            byte[] B = MS.ToArray();
            B[10] = (byte)(B[10] & 0X78); //No global color table.
            BW.Write(B, 0, 13);
            BW.Write(GifAnimation);
            WriteGifImg(B, BW);
            for (int I = 1; I < Files.Length; I++)
            {
                MS.SetLength(0);
                Image.FromFile(Files[I]).Save(MS, ImageFormat.Gif);
                B = MS.ToArray();
                WriteGifImg(B, BW);
            }
            BW.Write(B[B.Length - 1]);
            BW.Close();
            MS.Dispose();



            //AnimatedGifEncoder ege = new AnimatedGifEncoder();
            //ege.SetQuality(32000);
            //ege.Start(outputFilePath);
            //ege.SetDelay(1000);
            ////-1:no repeat,0:always repeat
            //ege.SetRepeat(0);
            //for (int i = 0, count = imageFilePaths.Length; i < count; i++)
            //{
            //    ege.AddFrame(Image.FromFile(imageFilePaths[i]));
            //}
            //ege.Finish();

            return (outputFilePath);
        }

        public void WriteGifImg(byte[] B, BinaryWriter BW)
        {
            byte[] Delay = { 100 / FRAME_RATE, 0 };

            B[785] = Delay[0]; //5 secs delay
            B[786] = Delay[1];
            B[798] = (byte)(B[798] | 0X87);
            BW.Write(B, 781, 18);
            BW.Write(B, 13, 768);
            BW.Write(B, 799, B.Length - 800);
        }

        public static T[] ConcatArrays<T>(params T[][] list)
        {
            var result = new T[list.Sum(a => a.Length)];
            int offset = 0;
            for (int x = 0; x < list.Length; x++)
            {
                list[x].CopyTo(result, offset);
                offset += list[x].Length;
            }
            return result;
        }

        public static T[] SubStringArrays<T>(T[] list, int offset)
        {
            int length = list.Length - offset;
            return SubStringArrays<T>(list, offset, length);
        }

        public static T[] SubStringArrays<T>(T[] list, int offset, int length)
        {
            var result = new T[length];

            for (int x = offset, i=0; x < offset + length; x++, i++)
            {
                result[i] = list[x];
            }

            return result;
        }

        #endregion
    }
}
