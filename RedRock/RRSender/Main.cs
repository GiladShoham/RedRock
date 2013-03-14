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
        public const int BYTES_IN_FRAME        = 200;
        public const int NUM_OF_SEQUENCE_DIGIT = 8;
        public const int QR_HIGHET             = 57;
        public const int QR_WIDTH              = 57;
        
        
        // Data Member
        private string OUTPUT_FOLDER_BMPS;
        private string OUTPUT_FOLDER_TXT;
        private string OUTPUT_FOLDER_GIF;
        private string OUTPUT_FOLDER_BASE = @"D:\Tomer\Studies\Dropbox\RedRock\QRSample\New\";
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
        }

        #endregion

        #region Event Methods

        private void CreateFolders()
        {
            string strTestCounter = (Directory.GetDirectories(OUTPUT_FOLDER_BASE).Length + 1) + @"\";

            Directory.CreateDirectory(OUTPUT_FOLDER_BASE + strTestCounter);
            
            File.Copy(m_strCurrFileLocation, OUTPUT_FOLDER_BASE + strTestCounter + Path.GetFileName(m_strCurrFileLocation));
            m_strCurrFileLocation = OUTPUT_FOLDER_BASE + strTestCounter + Path.GetFileName(m_strCurrFileLocation);

            OUTPUT_FOLDER_BMPS = OUTPUT_FOLDER_BASE + strTestCounter + "BMP\\";
            OUTPUT_FOLDER_TXT = OUTPUT_FOLDER_BASE + strTestCounter + "TXT\\";
            OUTPUT_FOLDER_GIF = OUTPUT_FOLDER_BASE + strTestCounter + "GIF\\";

            Directory.CreateDirectory(OUTPUT_FOLDER_BMPS);
            Directory.CreateDirectory(OUTPUT_FOLDER_GIF);
            Directory.CreateDirectory(OUTPUT_FOLDER_TXT);
        }

        private void btnCreateCode_Click(object sender, EventArgs e)
        {
            OUTPUT_FOLDER_BASE = Path.GetTempPath();
            //Util.StartCapturing();
            this.FileToQRCode(this.txtFilePath.Text);
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

            // Create Folders
            CreateFolders();

            // Comprass the file
            string strZipFile = this.Comprass(m_strCurrFileLocation);
            byte[] btFullOrigianlArray = File.ReadAllBytes(strZipFile);

            // Create the bitmap array
            Decimal dcNumOfFrame = Decimal.Ceiling(Decimal.Parse(btFullOrigianlArray.Length.ToString()) / Decimal.Parse((BYTES_IN_FRAME + NUM_OF_SEQUENCE_DIGIT).ToString()));
            int NumOfFrame = int.Parse(dcNumOfFrame.ToString());
            Bitmap[] arrbpmOutput = new Bitmap[NumOfFrame];

            // Creaate QR code of each frame
            QRCodeWriter qcCode = new QRCodeWriter();
            string strLastStringAddon = string.Empty;
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
                int output_size = ((BYTES_IN_FRAME * 4) / 3) + (BYTES_IN_FRAME / 96) - 1;

                // Encode the sting to QR code
                strOneFrame = strOneFrame.PadRight(output_size, ' ');
                ByteMatrix btMatrix = qcCode.encode(strOneFrame, BarcodeFormat.QR_CODE, QR_WIDTH, QR_HIGHET);

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

                btMatrix.ToBitmap().Save(OUTPUT_FOLDER_BMPS + nCurrFrameNumber + ".jpg", GetEncoder(ImageFormat.Jpeg), myEncoderParameters);

                // Add text file for debug
                File.WriteAllText(OUTPUT_FOLDER_TXT + nCurrFrameNumber + ".txt", strOneFrame);

            }

            // Create animited gif from bitmap
            string strGIFFileName = this.CreateAnimitedGif();

            return (strGIFFileName);
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
            byte[] Delay = { 10, 0 };

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
