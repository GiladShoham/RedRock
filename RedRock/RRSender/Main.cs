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

using IEC16022Sharp;
using System.IO;
using Gif.Components;
using System.Drawing.Imaging;

namespace RRReciver
{
    public partial class Main : Form
    {
        #region Data Members

        // Const Members
        public const int BYTES_IN_FRAME        = 624;
        public const int NUM_OF_SEQUENCE_DIGIT = 3;
        public const int QR_HIGHET             = 144;
        public const int QR_WIDTH              = 144;
        private string OUTPUT_FOLDER_BASE = @"D:\Tomer\Studies\Dropbox\RedRock\QRSample\New\";
        
        // Data Member
        private string OUTPUT_FOLDER_BMPS;
        private string OUTPUT_FOLDER_TXT;
        private string OUTPUT_FOLDER_GIF;
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
            // Set the curr file details to the data members
            m_strCurrFileLocation = this.txtFilePath.Text;
            m_strFileName = Path.GetFileName(m_strCurrFileLocation);

            // Create Folders
            CreateFolders();

            //string strZipFile = this.Comprass(m_strCurrFileLocation);
            string strZipFile = m_strCurrFileLocation;
            byte[] btArray = File.ReadAllBytes(strZipFile);

            //String strFullBitmap = System.Text.Encoding.UTF8.GetString(btArray);

            int NumOfFrame = btArray.Length / (BYTES_IN_FRAME + NUM_OF_SEQUENCE_DIGIT);

            Bitmap[] arrbpmOutput = new Bitmap[NumOfFrame];

            QRCodeWriter qcCode = new QRCodeWriter();
            string strLastStringAddon = string.Empty;
            for (int nCurrFrameNumber = 0; nCurrFrameNumber < NumOfFrame; nCurrFrameNumber++ )
            {
                // TODO: Restore this
                /*
                string strOneFrame;

                // Check if it is the last string or not
                if (nCurrFrameNumber != NumOfFrame - 1)
                {
                    // If it is not the last string - then it cut the string from the right point in the right size
                    strOneFrame = strFullBitmap.Substring(nCurrFrameNumber * BYTES_IN_FRAME, BYTES_IN_FRAME);
                }
                else
                {
                    // If it is the last string - cut from the right point till the end
                    strOneFrame = strFullBitmap.Substring(nCurrFrameNumber * BYTES_IN_FRAME);
                    strLastStringAddon = "*" + Path.GetExtension(m_strFileName) + "*";
                }
                 

                // Add the number (with the 0 if it is only one digit) to the start of the image
                strOneFrame = string.Format("{0:00}", nCurrFrameNumber) + strLastStringAddon + " " + strOneFrame;
                */

                // Encode the string to QRCode
                //ByteMatrix btMatrix = qcCode.encode(strOneFrame, BarcodeFormat.DATAMATRIX, QR_WIDTH, QR_HIGHET);
                byte[] btBytesCurrFrame = new byte[BYTES_IN_FRAME];
                for (int i = 0; i < BYTES_IN_FRAME; i++)
			    {
                    btBytesCurrFrame[i] = btArray[i];
			    }
                
                

                DataMatrix dm = new DataMatrix(btBytesCurrFrame, QR_WIDTH, QR_HIGHET, EncodingType.Binary);
                dm.Image.Save(OUTPUT_FOLDER_BMPS + nCurrFrameNumber + ".bmp", ImageFormat.Bmp);

                
                
                //arrbpmOutput[nCurrFrameNumber] = btMatrix.ToBitmap();
            //    File.CreateText(OUTPUT_FOLDER_BMPS + nCurrFrameNumber + ".txt");
                //File.WriteAllText(OUTPUT_FOLDER_TXT + nCurrFrameNumber + ".txt", strOneFrame);

                //arrbpmOutput[nCurrFrameNumber].Save(OUTPUT_FOLDER_BMPS + nCurrFrameNumber + ".bmp");

                this.CreateAnimitedGif();
            }
        }

        private string Comprass(string startPath)
        {
            string strZipPath = GZipDecoder.Compress(new FileInfo(startPath));

            return (strZipPath);
        }

        private void CreateAnimitedGif()
        {
            /* create Gif */
            // you should replace filepath
            string[] imageFilePaths = Directory.GetFiles(OUTPUT_FOLDER_BMPS);

            String outputFilePath = OUTPUT_FOLDER_GIF + "output.gif";
            AnimatedGifEncoder ege = new AnimatedGifEncoder();
            ege.Start(outputFilePath);
            ege.SetDelay(500);
            //-1:no repeat,0:always repeat
            ege.SetRepeat(0);
            for (int i = 0, count = imageFilePaths.Length; i < count; i++)
            {
                ege.AddFrame(Image.FromFile(imageFilePaths[i]));
            }
            ege.Finish();
        }

        #endregion
    }
}
