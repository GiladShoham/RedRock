using System;
using System.Collections.Generic;
using System.Text;
using Inlite.ClearImage;
using Inlite.ClearImageNet;
using System.Drawing;
using System.IO;
using System.Threading;

namespace Main
{
    internal class ClearImageFnc
    {
        internal System.Windows.Forms.TextBox txtRslt = null;
        #region ClearImage API demonstration
        internal string Read1DPro_Page_Zones(string fileName, int page)
        {
            string s = "";
            CiServer ci = Inlite.ClearImageNet.Server.GetThreadServer();
            CiBarcodePro reader = ci.CreateBarcodePro();
            // for faster reading specify only required direction
            reader.Directions = FBarcodeDirections.cibHorz | FBarcodeDirections.cibVert | FBarcodeDirections.cibDiag;
#if false
				// For faster processing specify expected types
				reader.Type = FBarcodeType.cibfCode128 | FBarcodeType.cibfCode39;
#else
            // Read all most popular barcode types
            reader.AutoDetect1D = true;
#endif
            CiImage image = ci.CreateImage();
            string st = image.FileName;
            image.Open(fileName, page);
            s = s + "======= Barcode in ZONE (upper half of the image) ===========" + Environment.NewLine;
            // Set zone to top half of the image
            reader.Image = image.CreateZone(0, 0, image.Width, image.Height / 2);
            reader.Find(0);
            int cnt = 0;
            foreach (CiBarcode bc in reader.Barcodes)
                { cnt++; AddBarcode(ref s, cnt, bc, image.FileName, image.PageNumber); }
            if (cnt == 0) { s = s + "NO BARCODES"; }
            s = s + Environment.NewLine;
            s = s + "======= Barcode in IMAGE ===========" + Environment.NewLine;
            // Disable zone
            reader.Image = image;
            reader.Find(0);
            cnt = 0;
            foreach (CiBarcode bc in reader.Barcodes)
            { cnt++; AddBarcode(ref s, cnt, bc, image.FileName, image.PageNumber); }
            if (cnt == 0) { s = s + "NO BARCODES"; }
            s = s + Environment.NewLine;
            return s;
        }


        internal string Read1DPro_File_WithEvents(string fileName)
        {
            throw new Exception("ClearImage class does not implment events");
        }


        long filesScanned = 0;
        static object _lockObject = new object();
        string[] filesToScan;
        private delegate void SetControlPropertyThreadSafeDelegate(System.Windows.Forms.Control control, string propertyName, object propertyValue);
        public static void SetControlPropertyThreadSafe(System.Windows.Forms.Control control, string propertyName, object propertyValue)
        {
            if (control.InvokeRequired)
            {
                control.Invoke(new SetControlPropertyThreadSafeDelegate(SetControlPropertyThreadSafe), new object[] { control, propertyName, propertyValue });
            }
            else
            {
                control.GetType().InvokeMember(propertyName, System.Reflection.BindingFlags.SetProperty, null, control, new object[] { propertyValue });
            }
        }


        private void Read1DPro_OnThread()
        {
            CiServer ci = Inlite.ClearImageNet.Server.GetThreadServer();
            CiBarcodePro reader = ci.CreateBarcodePro();
#if false
            reader.Directions = FBarcodeDirections.cibHorz | FBarcodeDirections.cibVert | FBarcodeDirections.cibDiag;
            reader.AutoDetect1D = true;
#else
            reader.Directions = FBarcodeDirections.cibHorz;
			reader.Type = FBarcodeType.cibfCode128 | FBarcodeType.cibfCode39;
#endif
            // Read
            while (true)
            {
                string fileName;
                // Obtain next file name
                lock (_lockObject)
                {
                    if (filesScanned >= filesToScan.Length)
                        break;
                    fileName = filesToScan[filesScanned];
                    filesScanned++;
                }
                //  Read images from file
                string s = "";
                try
                {
                    // Disable zone
                    int page = 1;
                    reader.Image.Open(fileName, 1);
                    while (reader.Image.IsValid)
                    {
                        reader.Find(0);
                        int cnt = 0;
                        foreach (CiBarcode bc in reader.Barcodes)
                            {
                            cnt++; AddBarcode(ref s, cnt, bc, reader.Image.FileName, page);
                            }
                        page++;
                        if (page > reader.Image.PageCount)
                            break;
                        reader.Image.Open(fileName, page);
                    }
                        // update display
                    lock (_lockObject)
                    {
                        if (s != "")
                        {
                            string s1 = txtRslt.Text +
                                "_OnBarcodeFound on Managed Thread " + System.Threading.Thread.CurrentThread.ManagedThreadId + " -> " +
                                Environment.NewLine  + s;
                            SetControlPropertyThreadSafe(txtRslt, "Text", s);
                        }
                    System.Windows.Forms.Application.DoEvents();
                    }
                }
                catch (Exception ex)
                {
                    string s1 = txtRslt.Text + ">>>>>>>> ERROR processing '" + fileName + "'" +
                        Environment.NewLine + ex.Message + Environment.NewLine;
                    lock (_lockObject)
                    {
                        SetControlPropertyThreadSafe(txtRslt, "Text", s1);
                        System.Windows.Forms.Application.DoEvents();
                    }
                }
            }
        }

        internal string Read1DPro_Directory_Threads(string directoryName)
        {
            filesToScan = Directory.GetFiles(directoryName, "*.*", SearchOption.TopDirectoryOnly);
            filesScanned = 0;
            // Start 2 threads
            Thread workerThread1 = new Thread(new ThreadStart(Read1DPro_OnThread));
            workerThread1.Start();
            Thread workerThread2 = new Thread(new ThreadStart(Read1DPro_OnThread));
            workerThread2.Start();
            // wait for both threads to exit
            while (workerThread1.IsAlive || workerThread2.IsAlive)
            {
                System.Windows.Forms.Application.DoEvents();
            }
            txtRslt.Text = txtRslt.Text + "DONE!!!";
            return txtRslt.Text;
        }

        internal string Read1DPro_File(string fileName)
        {
            CiServer ci = Inlite.ClearImageNet.Server.GetThreadServer();
            CiBarcodePro reader = ci.CreateBarcodePro();
#if false
            reader.Directions = FBarcodeDirections.cibHorz | FBarcodeDirections.cibVert | FBarcodeDirections.cibDiag;
            reader.AutoDetect1D = true;
#else
            reader.Directions = FBarcodeDirections.cibHorz;
			reader.Type = FBarcodeType.cibfCode128 | FBarcodeType.cibfCode39;
#endif

            //  Read images from file
            string s = "";
            // Disable zone
            int page = 1;
            reader.Image.Open(fileName, 1);
            int cnt = 0; 
            while (reader.Image.IsValid)
            {
            reader.Find(0);

            foreach (CiBarcode bc in reader.Barcodes)
                {
                cnt++; AddBarcode(ref s, cnt, bc, reader.Image.FileName, page);
                }
            page++;
            if (page > reader.Image.PageCount)
                break;
            reader.Image.Open(fileName, page);
            }
            if (cnt == 0) { s = s + "NO BARCODES"; }
            s = s + Environment.NewLine;
            return s;
            }


        internal string Read1DPro_Stream(string fileName)
        {
            MemoryStream ms = Utility.FileToStream(fileName);
            CiServer ci = Inlite.ClearImageNet.Server.GetThreadServer();
            CiBarcodePro reader = ci.CreateBarcodePro();
#if false
            reader.Directions = FBarcodeDirections.cibHorz | FBarcodeDirections.cibVert | FBarcodeDirections.cibDiag;
            reader.AutoDetect1D = true;
#else
            reader.Directions = FBarcodeDirections.cibHorz;
            reader.Type = FBarcodeType.cibfCode128 | FBarcodeType.cibfCode39;
#endif

            int page = 1;
            reader.Image.Open(ms, 1);
            int cnt = 0;
            string s = "";
            while (reader.Image.IsValid)
            {
                reader.Find(0);

                foreach (CiBarcode bc in reader.Barcodes)
                {
                    cnt++; AddBarcode(ref s, cnt, bc, reader.Image.FileName, page);
                }
                page++;
                if (page > reader.Image.PageCount)
                    break;
                reader.Image.OpenPage(page);
            }
            if (cnt == 0) { s = s + "NO BARCODES"; }
            s = s + Environment.NewLine;
            return s;

        }

        internal string ReadPdf417_Page(string fileName, int page)
        {
            CiServer ci = Inlite.ClearImageNet.Server.GetThreadServer();
            CiPdf417 reader = ci.CreatePdf417(); 
            // for faster reading specify only required direction
            reader.Directions = FBarcodeDirections.cibHorz | FBarcodeDirections.cibVert | FBarcodeDirections.cibDiag;

            reader.Image.Open(fileName, page);
            reader.Find(0);
            int cnt = 0; string s = "";
            foreach (CiBarcode bc in reader.Barcodes)
            { cnt++; AddBarcode(ref s, cnt, bc, reader.Image.FileName, reader.Image.PageNumber); }
            if (cnt == 0) { s = s + "NO BARCODES"; }
            s = s + Environment.NewLine;
            return s;
        }

				internal string ReadDrvLic_Page(string fileName, int page)
				{
					throw new Exception("ClearImage class does not implement Driver License Reading");
				}

        internal string ShowInfo(CiImage image, int nPage)
        {
            string s = "";
            if (nPage == 1)
                s +=
                    "File = " + image.FileName + Environment.NewLine +
                            "  PageCnt = " + image.PageCount.ToString() +  Environment.NewLine;
            if (image.PageNumber > 0) s = s +
"  Page=" + image.PageNumber.ToString() + "  Format=" + System.Enum.GetName(typeof(Inlite.ClearImage.EFileFormat), image.Format) +
                "  Size=" + image.Width.ToString() + "x" + image.Height.ToString() +
                "  Dpi=" + image.HorzDpi.ToString() + "x" + image.VertDpi.ToString() +
                "  Bpp=" + image.BitsPerPixel.ToString() + Environment.NewLine;
            else s = s +
                "  Page = " + nPage.ToString() + "   Format = " + System.Enum.GetName(typeof(Inlite.ClearImage.EFileFormat), image.Format)
                    + Environment.NewLine;
            return s;
        }

        internal string Info(string fileName)
        {
            CiServer ci = Inlite.ClearImageNet.Server.GetThreadServer();
            CiImage image = ci.CreateImage();
            
             int page = 1;
            image.Open(fileName, page);
            int pages = image.PageCount;
            txtRslt.Text = txtRslt.Text + ShowInfo(image, page);
            for (page = 2; page <= Math.Min(pages, 20); page++)
            {
                image.Open(fileName, page);
                txtRslt.Text = txtRslt.Text + ShowInfo(image, page);
                System.Windows.Forms.Application.DoEvents();
            }
            return txtRslt.Text;
        }

        internal string ReadDataMatrix_Page(string fileName, int page)
        {
            CiServer ci = Inlite.ClearImageNet.Server.GetThreadServer();
            CiDataMatrix reader = ci.CreateDataMatrix();
            // for faster reading specify only required direction
            reader.Directions = FBarcodeDirections.cibHorz | FBarcodeDirections.cibVert | FBarcodeDirections.cibDiag;

            reader.Image.Open(fileName, page);
            reader.Find(0);
            int cnt = 0; string s = "";
            foreach (CiBarcode bc in reader.Barcodes)
            { cnt++; AddBarcode(ref s, cnt, bc, reader.Image.FileName, reader.Image.PageNumber); }
            if (cnt == 0) { s = s + "NO BARCODES"; }
            s = s + Environment.NewLine;
            return s;
        }

        internal string ReadQR_Page(string fileName, int page)
        {
            CiServer ci = Inlite.ClearImageNet.Server.GetThreadServer();
            CiQR reader = ci.CreateQR();
            // for faster reading specify only required direction
            reader.Directions = FBarcodeDirections.cibHorz | FBarcodeDirections.cibVert | FBarcodeDirections.cibDiag;

            reader.Image.Open(fileName, page);
            reader.Find(0);
            int cnt = 0; string s = "";
            foreach (CiBarcode bc in reader.Barcodes)
            { cnt++; AddBarcode(ref s, cnt, bc, reader.Image.FileName, reader.Image.PageNumber); }
            if (cnt == 0) { s = s + "NO BARCODES"; }
            s = s + Environment.NewLine;
            return s;
        }

        private void SavePage (ref string s, CiImage image,
            string fileOut, EFileFormat format)
        {
                // SaveAs and Appned will change file name.
                //   OpenPage require original FileName.
                // To preserve  file name save Duplicate of the  image
     //       CiImage temp = image.Duplicate();
            if (fileOut != "")
            {
                if (File.Exists(fileOut))
                {
                    image.Append(fileOut, format);
                    s = s + "Append:" + fileOut + Environment.NewLine;
                }
                else
                {
                    image.SaveAs(fileOut, format);
                    s = s + "SaveAs:" + fileOut + Environment.NewLine;
                }
            }
            // temp.Close();
        }

        internal string Repair_Page(string fileName, int page, string fileOut, EFileFormat format)
        {
            string s = "";
            CiServer ci = Inlite.ClearImageNet.Server.GetThreadServer();
            CiRepair repair = ci.CreateRepair(); 
            repair.Image.Open(fileName, page);
            //  Demonstates basic 
            s = s + "File:" + fileName + "  Page:" + page.ToString() + Environment.NewLine;
            repair.AutoDeskew(); s = s + "AutoDeskew" + Environment.NewLine;
            repair.AutoCrop(50, 50, 50, 50); s = s + "AutoCrop (margins 50pix)" + Environment.NewLine;
            //  Save results
            SavePage(ref s, repair.Image, fileOut, format);
            s = s + Environment.NewLine + "--------------" + Environment.NewLine;
            return s;
        }


         internal string Repair_File(string fileName, string fileOut, EFileFormat format)
        {
            CiServer ci = Inlite.ClearImageNet.Server.GetThreadServer();
            CiRepair repair  = ci.CreateRepair();
            //  Read images from file
            string s = "";
            // Disable zone
            int page = 1;
            repair.Image.Open(fileName, 1);
            int cnt = 0;
            int  pages = repair.Image.PageCount;
            while (repair.Image.IsValid)
            {
                s = s + "File:" + fileName + "  Page:" + page.ToString() + Environment.NewLine;
                repair.AutoDeskew(); s = s + "AutoDeskew" + Environment.NewLine;
                repair.AutoCrop(50, 50, 50, 50); s = s + "AutoCrop (margins 50pix)" + Environment.NewLine;
                SavePage(ref s, repair.Image, fileOut, format);
                page++;
                if (page > pages)
                    break;
                repair.Image.Open(fileName, page);
            }
            s = s + Environment.NewLine;
            return s;
        }

        internal string Repair_Stream(string fileName, string fileOut, EFileFormat format)
        {
            MemoryStream ms = Utility.FileToStream(fileName);
            CiServer ci = Inlite.ClearImageNet.Server.GetThreadServer();
            CiRepair repair = ci.CreateRepair();
            //  Read images from file
            string s = "";
            // Disable zone
            int page = 1;
            repair.Image.Open(ms, 1);
            int cnt = 0;
            int pages = repair.Image.PageCount;
            while (repair.Image.IsValid)
            {
                s = s + "File:" + fileName + "  Page:" + page.ToString() + Environment.NewLine;
                repair.AutoDeskew(); s = s + "AutoDeskew" + Environment.NewLine;
                repair.AutoCrop(50, 50, 50, 50); s = s + "AutoCrop (margins 50pix)" + Environment.NewLine;
#if false  // to write to multipage file
               SavePage(ref s, repair.Image, fileOut, format);
#else       // save single page to file
                MemoryStream msOut = repair.Image.SaveToStream(format);
                if (msOut != null)
                    {
                        string pageOut =
                            Path.GetDirectoryName(fileOut) + @"\" +
                            Path.GetFileNameWithoutExtension(fileOut) +
                            ".page_" + page.ToString() +
                            Path.GetExtension(fileOut);

  
                    Utility.StreamToFile(msOut, pageOut); 
                    }
#endif
                page++;
                if (page > pages)
                    break;
                repair.Image.OpenPage(page);
            }
            s = s + Environment.NewLine;

           return s;
        }


        internal string Tools_Page(string fileName, int page)
        {
            CiServer ci = Inlite.ClearImageNet.Server.GetThreadServer();
            CiTools tools = ci.CreateTools();
            string s = ""; int cnt = 0;
            tools.Image.Open(fileName, page);
            double dSkew = tools.MeasureSkew(); s = s + string.Format("Skew {0:0.##} deg", dSkew) + Environment.NewLine;
            if (tools.Image.BitsPerPixel == 1)
            {
                CiObject obj = tools.FirstObject();
                while (obj != null)
                {
                    cnt++;
                    if (cnt < 10 && obj.Pixels > 1)
                        AddObject(ref s, cnt, obj);
                    obj = tools.NextObject();
                }
                s = s + string.Format("Object Count: {0}", cnt) + Environment.NewLine;
            }
            return s;
        }

        internal string ServerInfo()
        {
            CiServer ci = Inlite.ClearImageNet.Server.GetThreadServer();

            txtRslt.Text = txtRslt.Text + "ClearImageNet server v" + ci.VerMajor.ToString() + "." + 
                ci.VerMinor.ToString() + "." + ci.VerRelease.ToString();
            txtRslt.Text = txtRslt.Text + Environment.NewLine;
            txtRslt.Text = txtRslt.Text + ci.get_Info(EInfoType.ciModulesList, 0).Replace ("\n", Environment.NewLine);
            return txtRslt.Text;
        }


        internal string Tools_Page_WithEvents(string fileName, int page, bool bSaveResults)
        {
            throw new Exception("ClearImage class does not implment events");
        }


        private void AddBarcode(ref string s, long i, CiBarcode Bc, string File, int Page)
        {
            s = s + "Barcode#:" + i.ToString();
            if (File != "") s += "  File:" + File;
            s  = s + " Page:" + Page.ToString() + Environment.NewLine;
            s = s + " Type:" + System.Enum.GetName(Bc.Type.GetType(), Bc.Type);
            s = s + " Lng:" + Bc.Length.ToString();
            // s = s + Environment.NewLine + "   "; 
            s = s + " Rect:" + Bc.Rect.left.ToString() + ":" + Bc.Rect.top.ToString() + "-" + Bc.Rect.right.ToString() + ":" + Bc.Rect.bottom.ToString();
            s = s + " Rotation:" + System.Enum.GetName(Bc.Rotation.GetType(), Bc.Rotation);
            s = s + Environment.NewLine + Bc.Text;
            s = s + Environment.NewLine + "--------------" + Environment.NewLine;
        }
        
        private void AddObject(ref string s, long cnt, CiObject Obj)
        {
            s = s + "Object #" + cnt.ToString();
            s = s + " Pixels:" + Obj.Pixels.ToString() + " Intervals:" + Obj.Intervals.ToString();
            s = s + " Rect:" + Obj.Rect.left.ToString() + ":" + Obj.Rect.top.ToString() + "-" + Obj.Rect.right.ToString() + ":" + Obj.Rect.bottom.ToString();
            s = s + Environment.NewLine;
        }

        #endregion
    }
}
