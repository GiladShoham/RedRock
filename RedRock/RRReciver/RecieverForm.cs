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
using System.Threading;
using com.google.zxing.datamatrix.detector;
using Main;
using Gif.Components;
using System.Runtime.InteropServices;
using AForge.Video.DirectShow;
using AForge.Video;
using AForge.Controls;
using System.Diagnostics;


// namespace of imported WIA Scripting COM component

namespace RedSender
{
    public partial class RedRock : Form
    {
        private Boolean stop_executing = false;
        private FilterInfoCollection videoDevices;
        private FileProcessor mDoAction;
        private string m_status = string.Empty;

        private Hashtable htAllPictureParts = new Hashtable();
        private int nLastPicture = -1;
        private int nTotalLenght = 0;
        private string sFileType = string.Empty;

        private delegate void dlgCameraEvent();
        private delegate void dlgFrameAdded(int CurrFrameNumber, int LastFrameNumber);

        private event dlgFrameAdded eveFrameAdded;
        dlgCameraEvent eveCameraEvent;

        public string Status 
        { 
            get
            {
                return (this.m_status);
            } 
            set
            {
                m_status = value;
                this.lblStatus.Text = m_status;
            }
        }

        public RedRock()
        {
            InitializeComponent();

            this.mDoAction = new FileProcessor();
            this.mDoAction.stop_executing = false;
            this.eveFrameAdded += new dlgFrameAdded(UpdateLabelStatus);

           }

        private void btnStartRecieving_Click(object sender, EventArgs e)
        { /*
            Thread backgroundThread =
                new Thread(new ThreadStart(this.mDoAction.ProcessFiles));
            backgroundThread.Name = "BackgroundThread";
            backgroundThread.IsBackground = true;

            backgroundThread.Start();

            MessageBox.Show("מבוצעת קליטה");*/

            this.TakePicture();
            //this.ProcessFiles();

           // CheckForIllegalCrossThreadCalls = false;
            this.eveCameraEvent += new dlgCameraEvent(this.StopCamera);
        }

        private void StopCamera()
        {
           this.videoSourcePlayer1.SignalToStop();
           this.stop_executing = true;
        }

        public static void EndProcessAnnounce()
        {
            MessageBox.Show("קליטה בוצעה בהצלחה!");
        }

        private void CreateResultFile()
        {
            ByteArray arr = new ByteArray();

            byte[] rv = new byte[nTotalLenght];
            int offset = 0;
            for (int i = 0; i <= this.htAllPictureParts.Count - 1; i++)
            {
                Byte[] array = (Byte[])this.htAllPictureParts[i];
                System.Buffer.BlockCopy(array, 0, rv, offset, array.Length);
                offset += array.Length;
            }

            MessageBox.Show("פענוח בוצע!");
            int nFileNum = Directory.GetFiles(Path.GetTempPath()).Length;
            string sCompressedFileName = Path.GetTempPath() + "\\" + nFileNum + "temp.txt";
            string SOutFile = Path.GetTempPath() + "\\" + nFileNum + this.sFileType;

            File.WriteAllBytes(sCompressedFileName, rv);

            Gif.Components.Util.DecompressFileLZMA(sCompressedFileName, SOutFile);
            //File.Copy(SOutFile, @"d:\" + Path.GetFileName(SOutFile));
            

            Process.Start(SOutFile);

        }

        private string QRDecode(Bitmap image)
        {
            string sdecodedString = string.Empty;

            ClearImageNetFnc ciNetProc = new ClearImageNetFnc();
            string s = ciNetProc.ReadQR_Page(image);
		
            if (s != "NO BARCODES")
            {
                int nPerfixEnd = s.IndexOf("RAW BARCODE DATA:");
                s = s.Remove(0, nPerfixEnd);

                int nLastPerfix = s.IndexOf(":");
                s = s.Remove(0, nLastPerfix + 3);
                s = s.Replace("--------------", String.Empty);
            }

            return s;
        }

        private void TakePicture()
        {
            videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            string MonikerString = videoDevices[0].MonikerString;

            foreach (FilterInfo fi in videoDevices)
            {
                VideoCaptureDevice vd = new VideoCaptureDevice(fi.MonikerString);
                //vd.DesiredFrameSize = new Size(600, 800);
                
                this.videoSourcePlayer1.VideoSource = vd;
                // set NewFrame event handler
                this.videoSourcePlayer1.VideoSource.NewFrame += new NewFrameEventHandler(video_NewFrame);
                
                this.videoSourcePlayer1.Start();
                this.stop_executing = false;
                break;
            }
        }

        private void UpdateLabelStatus(int nCurrFrameNumber, int nLastFrameNumber)
        {
            this.m_status = "נקלטו: " + nCurrFrameNumber.ToString();

            if (nLastFrameNumber != -1)
            {
                this.m_status += "מתוך: " + (nLastFrameNumber + 1).ToString();
                

                foreach (int nCurrNum in htAllPictureParts.Keys)
                {
                    if ((nCurrNum % 30) == 0)
                    {
                        this.m_status += "\r\n";
                    }
                    this.m_status += " " + nCurrNum;
                }
            }

            
            

            this.lblStatus.Text = this.m_status;

            
        }

        private void video_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            if (!this.stop_executing)
            {
                // get new frame
                Bitmap bitmap = eventArgs.Frame;
                this.ProcessImage(bitmap);

                if ((this.htAllPictureParts.Count == this.nLastPicture + 1) &&
                    (this.nLastPicture != -1))
                {
                    //this.eveCameraEvent();

                    this.CreateResultFile();
                    this.BeginInvoke(eveCameraEvent, null);
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.StopCamera();
        }

        private string ProcessImage(Bitmap image)
        {
            string sFileResultType = String.Empty;
            string sDecodedPicture = this.QRDecode(image);

            if (sDecodedPicture != "NO BARCODES")
            {
                // Split the decoded string to the picture and picture index
                int nSpace = sDecodedPicture.IndexOf(' ');
                string sPicIndex = sDecodedPicture.Substring(0, nSpace);
                string sPicture = sDecodedPicture.Remove(0, nSpace + 1);

                // If it is the last file - it will have *GIF* ending
                int nStarIndex = sPicIndex.IndexOf('*');

                if (nStarIndex != -1)
                {
                    int nSecondStarIndex = sPicIndex.IndexOf('*', nStarIndex + 1);
                    this.sFileType = sPicIndex.Substring(nStarIndex + 1, nSecondStarIndex - nStarIndex - 1);
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
                    Byte[] btPic = Convert.FromBase64String(sPicture.Trim());

                    htAllPictureParts.Add(nPicIndex, btPic);

                    // Change the label
                    this.Invoke(eveFrameAdded, htAllPictureParts.Count, nLastPicture);

                    nTotalLenght = nTotalLenght + btPic.Length;
                }
            }

            return sFileResultType;
        }
    }
}
