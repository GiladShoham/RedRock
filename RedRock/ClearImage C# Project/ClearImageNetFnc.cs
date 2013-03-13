using System;
using System.Collections.Generic;
using System.Text;
using Inlite.ClearImageNet;
using System.Drawing;
using System.IO;
using System.Threading;

namespace Main
{
    internal class ClearImageNetFnc
    {
        internal System.Windows.Forms.TextBox txtRslt = null;
#region ClearImageNet API demonstration
	internal  string Read1DPro_Page_Zones  (string fileName, int page)
		{
				string s = "";
				BarcodeReader reader = new BarcodeReader(); 
				// for faster reading specify only required direction
				reader.Horizontal = true; reader.Vertical = true; reader.Diagonal = true;
#if false
				// For faster processing specify expected types
				reader.Code128 = true;
				reader.Code39 = true;
#else
				// Read all most popular barcode types
				reader.Auto1D = true; 
#endif
				ImageIO io = new ImageIO();
				ImageInfo info = io.Info(fileName, page);
				s = s + "======= Barcode in ZONE (upper half of the image) ===========" + Environment.NewLine;
				// Set zone to top half of the image
				reader.Zone = new Rectangle (0,0,info.Width, info.Height/2);
				Barcode[] barcodes = reader.Read (fileName, page); 
				int cnt = 0;
				foreach (Barcode bc in barcodes) 
					{cnt++; AddBarcode(ref s, cnt, bc); } 
				if (cnt == 0) 		{ s = s + "NO BARCODES"; 	} 
				s = s +  Environment.NewLine;
				s = s + "======= Barcode in IMAGE ===========" + Environment.NewLine;
				// Disable zone
				reader.Zone = new Rectangle ();
				barcodes = reader.Read (fileName, page); 
				cnt = 0;
				foreach (Barcode bc in barcodes) 
					{cnt++; AddBarcode(ref s, cnt, bc); } 
				if (cnt == 0) 		{ s = s + "NO BARCODES"; 	} 
				return  s; 
		}
		
	private void _OnBarcodeFound (object sender, BarcodeFoundEventArgs e)
		{	
			// e.cancel = (e.Count == 3);		// Cancel after 3 barcodes are found
            txtRslt.Text = txtRslt.Text + "_OnBarcodeFound -> ";
            string s = txtRslt.Text;
            AddBarcode(ref s, e.Count, e.Barcode);
            txtRslt.Text = s;
			System.Windows.Forms.Application.DoEvents();
		}
	
	internal  string Read1DPro_File_WithEvents (string fileName)
		{
				BarcodeReader reader = new BarcodeReader(); 
				// configure directions
				reader.Horizontal = true; 
				reader.Vertical = false; reader.Diagonal = false;
				//configure types
				reader.Auto1D = true; 
					// Configure events
				reader.BarcodeFoundEvent += new BarcodeReader.BarcodeFoundEventHandler (_OnBarcodeFound);
					// Read
				reader.Read (fileName);
                if ((txtRslt.Text == "")) { txtRslt.Text = "NO BARCODES"; }
                return txtRslt.Text; 
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

    private void _OnBarcodeFoundThread(object sender, BarcodeFoundEventArgs e)
    {
        lock (_lockObject)
        {
            string s = txtRslt.Text + "_OnBarcodeFound on Managed Thread " + System.Threading.Thread.CurrentThread.ManagedThreadId + " -> ";
            AddBarcode(ref s, e.Count, e.Barcode);
            SetControlPropertyThreadSafe (txtRslt, "Text", s);
            // txtRslt.Text = s;
            System.Windows.Forms.Application.DoEvents();
        }
    }

     private void Read1DPro_OnThread()
     {
         BarcodeReader reader = new BarcodeReader();
#if false
         // configure directions
         reader.Horizontal = true; reader.Vertical = true; reader.Diagonal = true;
         //configure types
         reader.Auto1D = true;
#else
         // configure directions
         reader.Horizontal = true; 
         //configure types
         reader.Code39 = true;
#endif
         // Configure events
         reader.BarcodeFoundEvent += new BarcodeReader.BarcodeFoundEventHandler(_OnBarcodeFoundThread);
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
             try
             {
#if false
                 reader.Read(fileName);     // Read all pages
#else
                 reader.Read(fileName, 1);  // Read only page 1
#endif
             }
             catch (Exception ex)
             {
                 string s = txtRslt.Text + ">>>>>>>> ERROR processing '" + fileName + "'" + 
                     Environment.NewLine + ex.Message + Environment.NewLine;
                 lock (_lockObject)
                     {SetControlPropertyThreadSafe (txtRslt, "Text", s);
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
            BarcodeReader reader = new BarcodeReader();
            // configure directions
            reader.Horizontal = true; reader.Vertical = true; reader.Diagonal = true;
            //configure types
            reader.Code39 = true;
            reader.Code128= true;
            // Read Code39 and Code128 barcodes in file
            Barcode[] barcodes = reader.Read(fileName);
            string s = "";int cnt = 0;
            foreach (Barcode bc in barcodes)
               { cnt++; AddBarcode(ref s, cnt, bc); }
            if (cnt == 0) { s = s + "NO BARCODES"; }
            s = s + Environment.NewLine;
            return s;
        }


        internal string Read1DPro_Stream(string fileName)
        {
            MemoryStream ms = Utility.FileToStream(fileName);
            BarcodeReader reader = new BarcodeReader();
            // Configure events
            //    reader.BarcodeFoundEvent += new BarcodeReader.BarcodeFoundEventHandler(_OnBarcodeFound);
            // configure directions
            reader.Horizontal = true; reader.Vertical = true; reader.Diagonal = true;
            //configure types
            reader.Auto1D = true;
            Barcode[] barcodes = reader.Read(ms);
            string s = ""; int cnt = 0;
            foreach (Barcode bc in barcodes)
             { cnt++; AddBarcode(ref s, cnt, bc); }
            if (cnt == 0) { s = s + "NO BARCODES"; }
            s = s + Environment.NewLine;
            return s;
        }

    internal string ReadPdf417_Page(string fileName, int page)
		{
				BarcodeReader reader = new BarcodeReader(); 
				// for faster reading specify only required direction
				reader.Horizontal = true; reader.Vertical = true; reader.Diagonal = true;
				// specify type
				reader.Pdf417 = true;
				Barcode[] barcodes = reader.Read (fileName, page); 
				string s = "";  int cnt = 0;
				foreach (Barcode bc in barcodes) 
					{cnt++; AddBarcode(ref s, cnt, bc); } 
				if (cnt == 0) 		{ s = "NO BARCODES"; 	} 
				return  s; 
		}

			internal string ReadDrvLic_Page(string fileName, int page)
			{
				BarcodeReader reader = new BarcodeReader();
				// for faster reading specify only required direction
				reader.Horizontal = true; reader.Vertical = true; reader.Diagonal = true;
				// specify type
				reader.DrvLicID = true;
				Barcode[] barcodes = reader.Read(fileName, page);
				string s = ""; int cnt = 0;
				foreach (Barcode bc in barcodes)
				{ cnt++;
					if (bc.Type == BarcodeType.Pdf417)
					{
						// Decode and display AAMVA data as XML
						string aamva = bc.Decode(BarcodeDecoding.aamva);
						if (aamva != "")
							s = s + "Driver License / ID Data: " + Environment.NewLine + aamva + Environment.NewLine;
					}
					AddBarcode(ref s, cnt, bc); 
					}
				if (cnt == 0) { s = "NO BARCODES"; }
				return s;
			}

	internal  string ShowInfo (ImageInfo info, int nPage)
		{
			string s = "";
			if (nPage == 1)
				s +=
					"File = " + info.FileName + Environment.NewLine + 
							"  PageCnt = " + info.PageCount.ToString() + "   Format = " +
							System.Enum.GetName(typeof (ImageFileFormat), info.FileFormat) + Environment.NewLine;
			if (info.Page > 0) s = s + 
				"  Page=" + info.Page.ToString() + "  Format=" + System.Enum.GetName(typeof (PageCompression), info.Compression) +
				"  Size=" + info.Width.ToString() + "x" + info.Height.ToString() +
				"  Dpi=" + info.HorizontalDpi.ToString() + "x" + info.VerticalDpi.ToString() +
				"  Bpp=" + info.BitsPerPixel.ToString() +  Environment.NewLine;
			else s = s + 
				"  Page = " + nPage.ToString() + "   Format = " + System.Enum.GetName(typeof (PageCompression), info.Compression)
					+  Environment.NewLine;
			return s;
		}
		
	internal  string Info (string fileName)
		{
			string s = "";
			ImageIO io1 = new ImageIO();
			int page = 1;
			ImageInfo oInfo;
			oInfo = io1.Info (fileName, page);
			int pages = oInfo.PageCount;
            txtRslt.Text = txtRslt.Text +  ShowInfo(oInfo, page); 
			for (page = 2; page <= Math.Min(pages, 20); page++)
				{
				oInfo = io1.Info (fileName, page);
                txtRslt.Text = txtRslt.Text + ShowInfo(oInfo, page);
                System.Windows.Forms.Application.DoEvents();
				}
                return txtRslt.Text;
		}

	internal  string ReadDataMatrix_Page (string fileName, int page)
		{
				BarcodeReader reader = new BarcodeReader(); 
				// for faster reading specify only required direction
				reader.Horizontal = true; reader.Vertical = true; reader.Diagonal = true;
				// specify type
				reader.DataMatrix = true;
				Barcode[] barcodes = reader.Read (fileName, page); 
				string s = "";  int cnt = 0;
				foreach (Barcode bc in barcodes) 
					{cnt++; AddBarcode(ref s, cnt, bc); } 
				if (cnt == 0) 		{ s = "NO BARCODES"; 	} 
				return  s; 
		}

	internal  string ReadQR_Page (string fileName, int page)
		{
				BarcodeReader reader = new BarcodeReader(); 
				// for faster reading specify only required direction
				reader.Horizontal = true; reader.Vertical = true; reader.Diagonal = true;
				// specify type
				reader.QR = true;
				Barcode[] barcodes = reader.Read (fileName, page); 
				string s = "";  int cnt = 0;
				foreach (Barcode bc in barcodes) 
					{cnt++; AddBarcode(ref s, cnt, bc); } 
				if (cnt == 0) 		{ s = "NO BARCODES"; 	} 
				return  s; 
		}

        internal string Repair_Page(string fileName, int page, string fileOut, ImageFileFormat format)
        {
            string s = "";
            ImageEditor repair = new ImageEditor();
            repair.Image.Open(fileName, page);
            //  Demonstates basic 
            s = s + "File:" + fileName + "  Page:" + page.ToString() + Environment.NewLine;
            repair.AutoDeskew(); s = s + "AutoDeskew" + Environment.NewLine;
            repair.AutoCrop(50, 50, 50, 50); s = s + "AutoCrop (margins 50pix)" + Environment.NewLine;
            //  Save results
            if (fileOut != "")
            {
                if (File.Exists(fileOut))
                {
                    repair.Image.Append(fileOut, Inlite.ClearImage.EFileFormat.ciEXT);
                    s = s + "Append:" + fileOut;
                }
                else
                {
                    repair.Image.SaveAs(fileOut, Inlite.ClearImage.EFileFormat.ciEXT);
                    s = s + "SaveAs:" + fileOut;
                }
            }
            s = s + Environment.NewLine + "--------------" + Environment.NewLine;
            return s;
        }


        private void _OnEditPage(object sender, EditPageEventArgs e)
        {
           // if (e.Editor.Image.PageNumber % 2 == 1) // skip odd pages
           //    { e.skipPage = true; return; }

            e.Editor.AutoDeskew(); 
            e.Editor.AutoCrop(50, 50, 50, 50);

           // if (e.Editor.Image.PageNumber % 4 == 0) // invert each 4th page
           //     { e.Editor.Image.Invert(); }

                // display progress
            string s = "_OnEditPage -> ";
            s = s + "File:" + e.Editor.Image.FileName + "  Page:" + e.Editor.Image.PageNumber + Environment.NewLine;
            s = s + "AutoDeskew" + Environment.NewLine;
            s = s + "AutoCrop (margins 50pix)" + Environment.NewLine;
            txtRslt.Text = txtRslt.Text + s;
            System.Windows.Forms.Application.DoEvents();
        }

        internal string Repair_File(string fileName, string fileOut, ImageFileFormat format)
        {
            ImageEditor repair = new ImageEditor();
            bool ret = repair.Edit(fileName, _OnEditPage, fileOut, format, true);
            return txtRslt.Text;
        }

        internal string Repair_Stream(string fileName, string fileOut, ImageFileFormat format)
        {
            MemoryStream ms = Utility.FileToStream(fileName); 
            ImageEditor repair = new ImageEditor();
            MemoryStream msOut = repair.Edit(ms, _OnEditPage, format);
            if (msOut != null)
                Utility.StreamToFile(msOut, fileOut);
            return txtRslt.Text;
        }

        
        internal string Tools_Page(string fileName, int page)
		{
				string s = "";
				ImageEditor tools = new ImageEditor();
                tools.Image.Open(fileName, page);
				double dSkew = tools.SkewAngle; s = s + string.Format("Skew {0:0.##} deg", dSkew) + Environment.NewLine;
				if (tools.BitsPerPixel == 1)
				{
					ImageObject[] objects = tools.GetObjects();
					s = s + string.Format("Object Count: {0}", objects.Length) + Environment.NewLine;
				}
				return  s; 
		}

	internal  string ServerInfo ()
		{
        txtRslt.Text = txtRslt.Text + "ClearImageNet Server " + Server.Major.ToString() + "." + Server.Minor.ToString() + "." + Server.Release.ToString() + "  " + Server.Edition;
        txtRslt.Text = txtRslt.Text + Environment.NewLine;
		string sFormat = "{0,-16} {1,-30} {2,-9} {3,-5}";
		string s1 = String.Format (sFormat, "MODULE", "PRODUCT", "LICENSED", "CALLS");
        txtRslt.Text = txtRslt.Text + s1 + Environment.NewLine;
		foreach (LicModule oModule in Server.Modules)
		{
		s1 = String.Format (sFormat, oModule.Name, oModule.Product, oModule.IsLicensed.ToString(), oModule.Calls.ToString());
        txtRslt.Text = txtRslt.Text + s1 + Environment.NewLine;
		}
		return txtRslt.Text;
		}

	private void _OnObjectFound (object sender, ObjectFoundEventArgs e)
		{	
			e.cancel = (e.Count == 20);		// Canel after 20 objects are found
			txtRslt.Text = txtRslt.Text + "_OnObjectFound -> ";
            string s = txtRslt.Text;
            AddObject(ref s, e.Count, e.ImageObject);
            txtRslt.Text = s;
            System.Windows.Forms.Application.DoEvents();
		}
	
	internal  string Tools_Page_WithEvents (string fileName, int page, bool bSaveResults)
		{
				ImageEditor tools = new ImageEditor();
                tools.Image.Open(fileName, page);
						// Configure events
				tools.ObjectFoundEvent += new ImageEditor.ObjectFoundEventHandler (_OnObjectFound);
				tools.GetObjects();
                return txtRslt.Text; 
		}

	private void AddBarcode(ref string s, long i, Barcode Bc) 
		{
            s = s + "Barcode#:" + i.ToString();
            if (Bc.File != "") s += "  File:" + Bc.File;
            s = s + " Page:" + Bc.Page.ToString() + Environment.NewLine;
			s = s + " Type:" + System.Enum.GetName(Bc.Type.GetType(), Bc.Type); 
			s = s + " Lng:" + Bc.Length.ToString(); 
			// s = s + Environment.NewLine + "   "; 
			s = s +  " Rect:" + Bc.Rectangle.Left.ToString() + ":" + Bc.Rectangle.Top.ToString() + "-" + Bc.Rectangle.Right.ToString() + ":" + Bc.Rectangle.Bottom.ToString(); 
			s = s + " Rotation:" + System.Enum.GetName(Bc.Rotation.GetType(), Bc.Rotation);
				// Try to decompress 2D Barcode data
			if (Bc.Type == BarcodeType.Pdf417 || Bc.Type == BarcodeType.DataMatrix || Bc.Type == BarcodeType.QR)
			{
				string decomp = Bc.Decode(BarcodeDecoding.compA);
				if (decomp != "")
					s = s + Environment.NewLine + Environment.NewLine + "DECOMPRESSED BARCODE DATA (A):" + Environment.NewLine + decomp + Environment.NewLine;
				decomp = Bc.Decode(BarcodeDecoding.compI);
				if (decomp != "")
					s = s + Environment.NewLine + Environment.NewLine + "DECOMPRESSED BARCODE DATA (I):" + Environment.NewLine + decomp + Environment.NewLine;
			}
				// Show raw  data
			s = s + Environment.NewLine + "RAW BARCODE DATA:" + Environment.NewLine + Bc.Text; 
			s = s + Environment.NewLine + "--------------" + Environment.NewLine; 
		} 

	private void AddObject(ref string s, long cnt, ImageObject Obj) 
		{ 
			s = s + "Object #" + cnt.ToString(); 
			s = s + " Pixels:" + Obj.Pixels.ToString() + " Intervals:" + Obj.Intervals.ToString(); 
			s = s + " Rect:" + Obj.Rectangle.Left.ToString() + ":" + Obj.Rectangle.Top.ToString() + "-" + Obj.Rectangle.Right.ToString() + ":" + Obj.Rectangle.Bottom.ToString(); 
			s = s + Environment.NewLine;
		} 
		
#endregion
    }
}
