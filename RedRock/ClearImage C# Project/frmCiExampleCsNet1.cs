using System;
using System.Drawing;
using System.Windows.Forms;

using Inlite.ClearImageNet;

namespace Main
{
	public class Form1 : System.Windows.Forms.Form
    {
		internal System.Windows.Forms.PictureBox PictureBox1;
		internal System.Windows.Forms.TextBox txtRslt;
		internal System.Windows.Forms.TextBox txtFileIn_;
		public System.Windows.Forms.OpenFileDialog OpenFileDialog1;
		internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.Button cmdBrowseFileIn;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.NumericUpDown numPage;
        private System.Windows.Forms.Button cmdImageInfo;
        private System.Windows.Forms.Button cmdServerInfo;
        private GroupBox groupBox1;
        private RadioButton optClearImage;
        private RadioButton optClearImageNet;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private GroupBox groupBox4;
        internal Button cmdQR;
        internal Button cmdPDF417;
        internal Button cmdDataMatrix;
        private Button cmdRepairStream;
        private Button cmdRepair;
        private GroupBox groupBox5;
        private Button cmdTools;
        private Button cmdToolsEvents;
        internal Button cmdBc;
        internal Button cmdBcThreads;
        private Button cmdBcFromStream;
        internal Button cmdBcProZones;
        internal Button cmdBcEvents;
        private Button cmdRepairPage;
        internal Label label3;
        private ComboBox cmbOutFormat;
		internal Button cmdDrvLic;
		private System.ComponentModel.Container components = null;

		public Form1()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.PictureBox1 = new System.Windows.Forms.PictureBox();
			this.txtRslt = new System.Windows.Forms.TextBox();
			this.txtFileIn_ = new System.Windows.Forms.TextBox();
			this.OpenFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.Label1 = new System.Windows.Forms.Label();
			this.cmdBrowseFileIn = new System.Windows.Forms.Button();
			this.cmdImageInfo = new System.Windows.Forms.Button();
			this.numPage = new System.Windows.Forms.NumericUpDown();
			this.label2 = new System.Windows.Forms.Label();
			this.cmdServerInfo = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.optClearImage = new System.Windows.Forms.RadioButton();
			this.optClearImageNet = new System.Windows.Forms.RadioButton();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.cmdBc = new System.Windows.Forms.Button();
			this.cmdBcThreads = new System.Windows.Forms.Button();
			this.cmdBcFromStream = new System.Windows.Forms.Button();
			this.cmdBcProZones = new System.Windows.Forms.Button();
			this.cmdBcEvents = new System.Windows.Forms.Button();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.cmdQR = new System.Windows.Forms.Button();
			this.cmdPDF417 = new System.Windows.Forms.Button();
			this.cmdDataMatrix = new System.Windows.Forms.Button();
			this.groupBox4 = new System.Windows.Forms.GroupBox();
			this.label3 = new System.Windows.Forms.Label();
			this.cmbOutFormat = new System.Windows.Forms.ComboBox();
			this.cmdRepairPage = new System.Windows.Forms.Button();
			this.cmdRepairStream = new System.Windows.Forms.Button();
			this.cmdRepair = new System.Windows.Forms.Button();
			this.groupBox5 = new System.Windows.Forms.GroupBox();
			this.cmdTools = new System.Windows.Forms.Button();
			this.cmdToolsEvents = new System.Windows.Forms.Button();
			this.cmdDrvLic = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numPage)).BeginInit();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.groupBox4.SuspendLayout();
			this.groupBox5.SuspendLayout();
			this.SuspendLayout();
			// 
			// PictureBox1
			// 
			this.PictureBox1.Location = new System.Drawing.Point(641, 12);
			this.PictureBox1.Name = "PictureBox1";
			this.PictureBox1.Size = new System.Drawing.Size(184, 240);
			this.PictureBox1.TabIndex = 13;
			this.PictureBox1.TabStop = false;
			// 
			// txtRslt
			// 
			this.txtRslt.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtRslt.Location = new System.Drawing.Point(16, 72);
			this.txtRslt.Multiline = true;
			this.txtRslt.Name = "txtRslt";
			this.txtRslt.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtRslt.Size = new System.Drawing.Size(619, 180);
			this.txtRslt.TabIndex = 12;
			// 
			// txtFileIn_
			// 
			this.txtFileIn_.Location = new System.Drawing.Point(64, 12);
			this.txtFileIn_.Name = "txtFileIn_";
			this.txtFileIn_.Size = new System.Drawing.Size(417, 20);
			this.txtFileIn_.TabIndex = 9;
			// 
			// Label1
			// 
			this.Label1.Location = new System.Drawing.Point(8, 12);
			this.Label1.Name = "Label1";
			this.Label1.Size = new System.Drawing.Size(64, 24);
			this.Label1.TabIndex = 11;
			this.Label1.Text = "Image File";
			// 
			// cmdBrowseFileIn
			// 
			this.cmdBrowseFileIn.Location = new System.Drawing.Point(487, 12);
			this.cmdBrowseFileIn.Name = "cmdBrowseFileIn";
			this.cmdBrowseFileIn.Size = new System.Drawing.Size(24, 20);
			this.cmdBrowseFileIn.TabIndex = 10;
			this.cmdBrowseFileIn.Text = "...";
			this.cmdBrowseFileIn.Click += new System.EventHandler(this.cmdBrowseFileIn_Click_1);
			// 
			// cmdImageInfo
			// 
			this.cmdImageInfo.Location = new System.Drawing.Point(99, 40);
			this.cmdImageInfo.Name = "cmdImageInfo";
			this.cmdImageInfo.Size = new System.Drawing.Size(72, 24);
			this.cmdImageInfo.TabIndex = 40;
			this.cmdImageInfo.Tag = "Information about image file and it\'s pages";
			this.cmdImageInfo.Text = "Image Info";
			this.cmdImageInfo.Click += new System.EventHandler(this.cmdImageInfo_Click);
			// 
			// numPage
			// 
			this.numPage.Location = new System.Drawing.Point(552, 13);
			this.numPage.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.numPage.Name = "numPage";
			this.numPage.Size = new System.Drawing.Size(40, 20);
			this.numPage.TabIndex = 41;
			this.numPage.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(520, 13);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(32, 16);
			this.label2.TabIndex = 42;
			this.label2.Text = "Page";
			// 
			// cmdServerInfo
			// 
			this.cmdServerInfo.Location = new System.Drawing.Point(16, 41);
			this.cmdServerInfo.Name = "cmdServerInfo";
			this.cmdServerInfo.Size = new System.Drawing.Size(72, 24);
			this.cmdServerInfo.TabIndex = 44;
			this.cmdServerInfo.Tag = "Information ClearImage Server";
			this.cmdServerInfo.Text = "Server Info";
			this.cmdServerInfo.Click += new System.EventHandler(this.cmdServerInfo_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.optClearImage);
			this.groupBox1.Controls.Add(this.optClearImageNet);
			this.groupBox1.Location = new System.Drawing.Point(190, 33);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(269, 33);
			this.groupBox1.TabIndex = 49;
			this.groupBox1.TabStop = false;
			// 
			// optClearImage
			// 
			this.optClearImage.AutoSize = true;
			this.optClearImage.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.optClearImage.Location = new System.Drawing.Point(144, 10);
			this.optClearImage.Name = "optClearImage";
			this.optClearImage.Size = new System.Drawing.Size(112, 17);
			this.optClearImage.TabIndex = 1;
			this.optClearImage.Text = "ClearImage API";
			this.optClearImage.UseVisualStyleBackColor = true;
			// 
			// optClearImageNet
			// 
			this.optClearImageNet.AutoSize = true;
			this.optClearImageNet.Checked = true;
			this.optClearImageNet.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.optClearImageNet.Location = new System.Drawing.Point(6, 11);
			this.optClearImageNet.Name = "optClearImageNet";
			this.optClearImageNet.Size = new System.Drawing.Size(132, 17);
			this.optClearImageNet.TabIndex = 0;
			this.optClearImageNet.TabStop = true;
			this.optClearImageNet.Text = "ClearImageNet API";
			this.optClearImageNet.UseVisualStyleBackColor = true;
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.cmdBc);
			this.groupBox2.Controls.Add(this.cmdBcThreads);
			this.groupBox2.Controls.Add(this.cmdBcFromStream);
			this.groupBox2.Controls.Add(this.cmdBcProZones);
			this.groupBox2.Controls.Add(this.cmdBcEvents);
			this.groupBox2.Location = new System.Drawing.Point(16, 258);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(536, 59);
			this.groupBox2.TabIndex = 50;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Read 1D Barcodes";
			// 
			// cmdBc
			// 
			this.cmdBc.Location = new System.Drawing.Point(8, 19);
			this.cmdBc.Name = "cmdBc";
			this.cmdBc.Size = new System.Drawing.Size(103, 24);
			this.cmdBc.TabIndex = 51;
			this.cmdBc.Tag = "Read Code39 and Code128 Barcodes from all file Pages";
			this.cmdBc.Text = "Code 39 and 128";
			this.cmdBc.Click += new System.EventHandler(this.cmdBc_Click);
			// 
			// cmdBcThreads
			// 
			this.cmdBcThreads.Location = new System.Drawing.Point(117, 19);
			this.cmdBcThreads.Name = "cmdBcThreads";
			this.cmdBcThreads.Size = new System.Drawing.Size(103, 24);
			this.cmdBcThreads.TabIndex = 50;
			this.cmdBcThreads.Tag = "Read 1D Barcodes from all files in a Folder  using Threads";
			this.cmdBcThreads.Text = "With Threads";
			this.cmdBcThreads.Click += new System.EventHandler(this.cmdBcThreads_Click);
			// 
			// cmdBcFromStream
			// 
			this.cmdBcFromStream.Location = new System.Drawing.Point(321, 19);
			this.cmdBcFromStream.Name = "cmdBcFromStream";
			this.cmdBcFromStream.Size = new System.Drawing.Size(94, 24);
			this.cmdBcFromStream.TabIndex = 49;
			this.cmdBcFromStream.Tag = "Read 1D Barcodes from all pages in a Stream";
			this.cmdBcFromStream.Text = "From Stream";
			this.cmdBcFromStream.Click += new System.EventHandler(this.cmdBcFromStream_Click);
			// 
			// cmdBcProZones
			// 
			this.cmdBcProZones.Location = new System.Drawing.Point(421, 19);
			this.cmdBcProZones.Name = "cmdBcProZones";
			this.cmdBcProZones.Size = new System.Drawing.Size(102, 24);
			this.cmdBcProZones.TabIndex = 47;
			this.cmdBcProZones.Tag = "Read Code39 and Code128 Barcodes on a Page and Portion of a Page";
			this.cmdBcProZones.Text = "With Zones";
			this.cmdBcProZones.Click += new System.EventHandler(this.cmdBcProZones_Click);
			// 
			// cmdBcEvents
			// 
			this.cmdBcEvents.Location = new System.Drawing.Point(226, 19);
			this.cmdBcEvents.Name = "cmdBcEvents";
			this.cmdBcEvents.Size = new System.Drawing.Size(89, 24);
			this.cmdBcEvents.TabIndex = 46;
			this.cmdBcEvents.Tag = "Read 1D Barcodes from all pages in a File .  Raise Event when Barcode is found";
			this.cmdBcEvents.Text = "With Events";
			this.cmdBcEvents.Click += new System.EventHandler(this.cmdBcEvents_Click);
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.cmdDrvLic);
			this.groupBox3.Controls.Add(this.cmdQR);
			this.groupBox3.Controls.Add(this.cmdPDF417);
			this.groupBox3.Controls.Add(this.cmdDataMatrix);
			this.groupBox3.Location = new System.Drawing.Point(16, 323);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(325, 54);
			this.groupBox3.TabIndex = 51;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Read 2D Barcodes";
			// 
			// cmdQR
			// 
			this.cmdQR.Location = new System.Drawing.Point(164, 19);
			this.cmdQR.Name = "cmdQR";
			this.cmdQR.Size = new System.Drawing.Size(72, 24);
			this.cmdQR.TabIndex = 49;
			this.cmdQR.Tag = "Read QR Barcodes on a Page";
			this.cmdQR.Text = "QR";
			this.cmdQR.Click += new System.EventHandler(this.cmdQR_Click);
			// 
			// cmdPDF417
			// 
			this.cmdPDF417.Location = new System.Drawing.Point(8, 19);
			this.cmdPDF417.Name = "cmdPDF417";
			this.cmdPDF417.Size = new System.Drawing.Size(72, 24);
			this.cmdPDF417.TabIndex = 47;
			this.cmdPDF417.Tag = "Read PDF417 Barcodes on a Page";
			this.cmdPDF417.Text = "PDF417";
			this.cmdPDF417.Click += new System.EventHandler(this.cmdPDF417_Click);
			// 
			// cmdDataMatrix
			// 
			this.cmdDataMatrix.Location = new System.Drawing.Point(86, 19);
			this.cmdDataMatrix.Name = "cmdDataMatrix";
			this.cmdDataMatrix.Size = new System.Drawing.Size(72, 24);
			this.cmdDataMatrix.TabIndex = 48;
			this.cmdDataMatrix.Tag = "Read DataMatrix Barcodes on a Page";
			this.cmdDataMatrix.Text = "DataMatrix";
			this.cmdDataMatrix.Click += new System.EventHandler(this.cmdDataMatrix_Click);
			// 
			// groupBox4
			// 
			this.groupBox4.Controls.Add(this.label3);
			this.groupBox4.Controls.Add(this.cmbOutFormat);
			this.groupBox4.Controls.Add(this.cmdRepairPage);
			this.groupBox4.Controls.Add(this.cmdRepairStream);
			this.groupBox4.Controls.Add(this.cmdRepair);
			this.groupBox4.Location = new System.Drawing.Point(360, 323);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new System.Drawing.Size(465, 54);
			this.groupBox4.TabIndex = 52;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "Repair Image";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(287, 19);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(45, 32);
			this.label3.TabIndex = 51;
			this.label3.Text = "Output format:";
			// 
			// cmbOutFormat
			// 
			this.cmbOutFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbOutFormat.FormattingEnabled = true;
			this.cmbOutFormat.Items.AddRange(new object[] {
            "Input Format",
            "PDF",
            "TIFF",
            "JPEG"});
			this.cmbOutFormat.Location = new System.Drawing.Point(338, 19);
			this.cmbOutFormat.Name = "cmbOutFormat";
			this.cmbOutFormat.Size = new System.Drawing.Size(106, 21);
			this.cmbOutFormat.TabIndex = 50;
			// 
			// cmdRepairPage
			// 
			this.cmdRepairPage.Location = new System.Drawing.Point(15, 19);
			this.cmdRepairPage.Name = "cmdRepairPage";
			this.cmdRepairPage.Size = new System.Drawing.Size(74, 24);
			this.cmdRepairPage.TabIndex = 49;
			this.cmdRepairPage.Tag = "AutoDeskew and Crop a Page";
			this.cmdRepairPage.Text = "One page";
			this.cmdRepairPage.Click += new System.EventHandler(this.cmdRepairPage_Click);
			// 
			// cmdRepairStream
			// 
			this.cmdRepairStream.Location = new System.Drawing.Point(178, 19);
			this.cmdRepairStream.Name = "cmdRepairStream";
			this.cmdRepairStream.Size = new System.Drawing.Size(103, 24);
			this.cmdRepairStream.TabIndex = 48;
			this.cmdRepairStream.Tag = "Autodeskew and Crop all pages in a Stream";
			this.cmdRepairStream.Text = "From/To Stream";
			this.cmdRepairStream.Click += new System.EventHandler(this.cmdRepairStream_Click);
			// 
			// cmdRepair
			// 
			this.cmdRepair.Location = new System.Drawing.Point(95, 19);
			this.cmdRepair.Name = "cmdRepair";
			this.cmdRepair.Size = new System.Drawing.Size(77, 24);
			this.cmdRepair.TabIndex = 22;
			this.cmdRepair.Tag = "AutoDeskew and Crop all pages in a File";
			this.cmdRepair.Text = "All pages";
			this.cmdRepair.Click += new System.EventHandler(this.cmdRepair_Click);
			// 
			// groupBox5
			// 
			this.groupBox5.Controls.Add(this.cmdTools);
			this.groupBox5.Controls.Add(this.cmdToolsEvents);
			this.groupBox5.Location = new System.Drawing.Point(577, 258);
			this.groupBox5.Name = "groupBox5";
			this.groupBox5.Size = new System.Drawing.Size(197, 59);
			this.groupBox5.TabIndex = 54;
			this.groupBox5.TabStop = false;
			this.groupBox5.Text = "Tools_Page";
			// 
			// cmdTools
			// 
			this.cmdTools.Location = new System.Drawing.Point(16, 19);
			this.cmdTools.Name = "cmdTools";
			this.cmdTools.Size = new System.Drawing.Size(72, 24);
			this.cmdTools.TabIndex = 24;
			this.cmdTools.Tag = "Measure Skew and count Objects on a Page";
			this.cmdTools.Text = "One Page";
			this.cmdTools.Click += new System.EventHandler(this.cmdTools_Click);
			// 
			// cmdToolsEvents
			// 
			this.cmdToolsEvents.Location = new System.Drawing.Point(94, 19);
			this.cmdToolsEvents.Name = "cmdToolsEvents";
			this.cmdToolsEvents.Size = new System.Drawing.Size(89, 24);
			this.cmdToolsEvents.TabIndex = 23;
			this.cmdToolsEvents.Tag = "List Objects on a Page.  Raise Event when Object is found";
			this.cmdToolsEvents.Text = "With Events";
			this.cmdToolsEvents.Click += new System.EventHandler(this.cmdToolsEvents_Click);
			// 
			// cmdDrvLic
			// 
			this.cmdDrvLic.Location = new System.Drawing.Point(243, 19);
			this.cmdDrvLic.Name = "cmdDrvLic";
			this.cmdDrvLic.Size = new System.Drawing.Size(72, 24);
			this.cmdDrvLic.TabIndex = 50;
			this.cmdDrvLic.Tag = "Read Driver License Barcodes on a Page";
			this.cmdDrvLic.Text = "Drv Lic";
			this.cmdDrvLic.Click += new System.EventHandler(this.cmdDrvLic_Click);
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(837, 389);
			this.Controls.Add(this.groupBox5);
			this.Controls.Add(this.groupBox4);
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.cmdServerInfo);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.numPage);
			this.Controls.Add(this.cmdImageInfo);
			this.Controls.Add(this.PictureBox1);
			this.Controls.Add(this.txtRslt);
			this.Controls.Add(this.txtFileIn_);
			this.Controls.Add(this.Label1);
			this.Controls.Add(this.cmdBrowseFileIn);
			this.Name = "Form1";
			this.Text = "ClearImage Example (C#)";
			this.Load += new System.EventHandler(this.Form1_Load);
			((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numPage)).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			this.groupBox4.ResumeLayout(false);
			this.groupBox5.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion
		[STAThread]
		static void Main() 
		{
			Application.Run(new Form1());
		}

#region Commands processing
        ClearImageNetFnc ciNetProc = new ClearImageNetFnc();
        ClearImageFnc ciProc =  new ClearImageFnc();
		private void Form1_Load(object sender, System.EventArgs e)
		{
			Microsoft.Win32.RegistryKey key = GetRegKey();
			if (key.ValueCount > 0) txtFileIn_.Text =  key.GetValue(txtFileIn_.Name).ToString();
            cmbOutFormat.SelectedIndex = 0;
            ciNetProc.txtRslt = txtRslt;
            ciProc.txtRslt = txtRslt;
			txtFileIn_.SelectionLength = 0;
			DisplayImage (txtFileIn_.Text);
			CmdEnbDis(true);
			// Instantiate ClearImage
      txtRslt.Text = AboutText();
		}
		
		private void cmdBcProZones_Click(object sender, System.EventArgs e)
		{
			try 
			{
                if (!OpStart(sender)) return;
			//    Do processing
             string s = "";
             if  (optClearImageNet.Checked) 
               s = ciNetProc.Read1DPro_Page_Zones(txtFileIn_.Text, (int)numPage.Value);
	            else
               s = ciProc.Read1DPro_Page_Zones(txtFileIn_.Text, (int)numPage.Value);
			// Display results
			OpDone(s);
			} 
			catch (Exception ex) 
			{ 
				ShowError(ex); 
			} 		
		}
		
		private void cmdPDF417_Click(object sender, System.EventArgs e)
		{ 
			try 
			{
                if (!OpStart(sender)) return;
			//    Do processing
             string s = "";
             if  (optClearImageNet.Checked) 
               s = ciNetProc.ReadPdf417_Page(txtFileIn_.Text, (int)numPage.Value);
	            else
               s = ciProc.ReadPdf417_Page(txtFileIn_.Text, (int)numPage.Value);

			// Display results
			OpDone(s);
			} 
			catch (Exception ex) 
			{ 
				ShowError(ex); 
			} 
		} 

		private void cmdDataMatrix_Click(object sender, System.EventArgs e)
		{ 
			try 
			{
                if (!OpStart(sender)) return;
			//    Do processing
             string s = "";
             if  (optClearImageNet.Checked) 
               s = ciNetProc.ReadDataMatrix_Page(txtFileIn_.Text, (int)numPage.Value);
	         else
               s = ciProc.ReadDataMatrix_Page(txtFileIn_.Text, (int)numPage.Value);
			// Display results
			OpDone(s);
			} 
			catch (Exception ex) 
			{ 
				ShowError(ex); 
			} 
		} 
					
        private void cmdQR_Click(object sender, EventArgs e)
        {
			try 
			{
                if (!OpStart(sender)) return;
			//    Do processing
             string s = "";
             if  (optClearImageNet.Checked) 
               s = ciNetProc.ReadQR_Page(txtFileIn_.Text, (int)numPage.Value);
	         else
               s = ciProc.ReadQR_Page(txtFileIn_.Text, (int)numPage.Value);
            	// Display results
			OpDone(s);
			} 
			catch (Exception ex) 
			{ 
				ShowError(ex); 
			} 

        }		
        
        private void cmdBcEvents_Click(object sender, System.EventArgs e)
		{
			try 
			{
                if (!OpStart(sender)) return;
			//    Do processing
             string s = "";
             if  (optClearImageNet.Checked) 
               s = ciNetProc.Read1DPro_File_WithEvents(txtFileIn_.Text);
	         else
               s = ciProc.Read1DPro_File_WithEvents(txtFileIn_.Text);
			// Display results
			OpDone(s);
			} 
			catch (Exception ex) 
			{ 
				ShowError(ex); 
			} 
		
		}

		private void cmdRepair_Click(object sender, System.EventArgs e)
		{
			try 
			{
                if (!OpStart(sender)) return;
                ImageFileFormat format = ImageFileFormat.inputFileFormat;
                Inlite.ClearImage.EFileFormat ciFormat = Inlite.ClearImage.EFileFormat.ciEXT;
                string fileOut = GetRepairFile(txtFileIn_.Text, ref format, ref ciFormat, true);
                //    Do processing
                 string s = "";
                 if  (optClearImageNet.Checked) 
                   s = ciNetProc.Repair_File(txtFileIn_.Text,  fileOut, format);
	             else
                   s = ciProc.Repair_File(txtFileIn_.Text, fileOut, ciFormat);
			// Display results
			    OpDone(s);
			    if (s != "")
                    DisplayImage(fileOut);
			} 
			catch (Exception ex) 
			{ 
				ShowError(ex); 
			} 
		}

		private void cmdTools_Click(object sender, System.EventArgs e)
		{
			try 
			{
                if (!OpStart(sender)) return;
			//    Do processing
             string s = "";
             if  (optClearImageNet.Checked) 
               s =  ciNetProc.Tools_Page(txtFileIn_.Text, (int)numPage.Value);
             else
               s = ciProc.Tools_Page(txtFileIn_.Text, (int)numPage.Value);
			// Display results
			OpDone(s);
			// Display modified image
			long n = s.LastIndexOf ("SaveAs:");
			if (n >= 0)
				{
				string a  = s.Substring((Int32)(n+7));
				DisplayImage(a);
				System.IO.File.Delete(a);
				}
			} 
			catch (Exception ex) 
			{ 
				ShowError(ex); 
			} 
		}
		
		private void cmdToolsEvents_Click(object sender, System.EventArgs e)
		{
			try 
			{
                if (!OpStart(sender)) return;
			//    Do processing
             string s = "";
             if  (optClearImageNet.Checked) 
               s = ciNetProc.Tools_Page_WithEvents(txtFileIn_.Text, (int)numPage.Value, true);
	         else
               s = ciProc.Tools_Page_WithEvents(txtFileIn_.Text, (int)numPage.Value, true);
			// Display results
			OpDone(s);
			} 
			catch (Exception ex) 
			{ 
				ShowError(ex); 
			} 
		}

		private void cmdAbout_Click_1(object sender, System.EventArgs e)
		{
		txtRslt.Text = AboutText();
		}

		private void cmdImageInfo_Click(object sender, System.EventArgs e)
		{
		try 
			{
                if (!OpStart(sender)) return;
			//    Do processing
             string s = "";
             if  (optClearImageNet.Checked) 
               s = ciNetProc.Info(txtFileIn_.Text);
	         else
               s = ciProc.Info(txtFileIn_.Text);
			// Display results
			OpDone("");
			} 
			catch (Exception ex) 
			{ 
				ShowError(ex); 
			} 
		}

		private void cmdBrowseFileIn_Click_1(object sender, System.EventArgs e)
		{
			OpenFileDialog1.FileName = txtFileIn_.Text; 
			OpenFileDialog1.Filter = "Image Files files (tif, jpg, bmp, pdf)|*.tif;*.tiff;*.jpg;*.jpeg;*.bmp;*.pdf|All files (*.*)|*.*"; 
			OpenFileDialog1.FilterIndex = 1; 
			OpenFileDialog1.RestoreDirectory = true; 
			OpenFileDialog1.CheckFileExists = true; 
			OpenFileDialog1.Multiselect = false; 
			if (OpenFileDialog1.ShowDialog() == DialogResult.OK) 
			{ 
				txtRslt.Text = ""; 
				txtFileIn_.Text = OpenFileDialog1.FileName; 
				Microsoft.Win32.RegistryKey key = GetRegKey();
				key.SetValue(txtFileIn_.Name, txtFileIn_.Text);  
				DisplayImage(txtFileIn_.Text); 
				Application.DoEvents();
				cmdImageInfo_Click(null, null);
			} 
		}

		private void cmdServerInfo_Click(object sender, System.EventArgs e)
		{
			try 
			{
                if (!OpStart(sender)) return;
			//    Do processing
             string s = "";
             if  (optClearImageNet.Checked) 
               s = ciNetProc.ServerInfo();
	         else
               s = ciProc.ServerInfo();
			// Display results
			OpDone(s);
			} 
			catch (Exception ex) 
			{ 
				ShowError(ex); 
			} 
		}
#endregion


#region Utility Functions
		private void CmdEnbDis (bool bEnb)
		{
			cmdBcProZones.Enabled = bEnb;
      txtFileIn_.Enabled = bEnb;
			cmdPDF417.Enabled = bEnb;
      cmdBrowseFileIn.Enabled = bEnb;
			cmdDataMatrix.Enabled = bEnb;
			cmdRepair.Enabled = bEnb;
			cmdTools.Enabled = bEnb;
			this.cmdImageInfo.Enabled = bEnb;
		}
		private bool OpStart (object sender)
		{
            string text = "";
			txtRslt.Text = "";
            if (sender != null && sender.GetType().Name == "Button")
            {
                Button btn = (Button) sender;
                if (btn.Tag != null) text = btn.Tag.ToString();
            }
            if (sender != null && sender.GetType().Name == "String")
            {
                text = (string) sender;
            }
            if (text != "")
                txtRslt.Text = "### " + text + Environment.NewLine + "#######################" + Environment.NewLine;
			if ((txtFileIn_.Text == "")) { MessageBox.Show("No File specified"); return false; }
            Application.DoEvents();
			return true;
		}


		private void OpDone (string sRslt)
		{
            if (sRslt.StartsWith("### "))
                txtRslt.Text = sRslt;
            else
			    txtRslt.Text = txtRslt.Text + sRslt;
		}

		private bool GetThumbnailImageAbort() 
		{ 
			return false; 
		} 

		private void DisplayImage(string fileName) 
		{ 
			try 
			{ 
				PictureBox1.Image = null; 
				if (!System.IO.File.Exists(fileName)) return;
				ImageIO io = new ImageIO();
				Bitmap newImage = io.Open(fileName);
				double scaleX = (double) PictureBox1.Width / (double) newImage.Width; 
				double scaleY = (double) PictureBox1.Height / (double) newImage.Height; 
				double Scale = Math.Min(scaleX, scaleY); 
				int w = (int) (newImage.Width * Scale); 
				int h = (int) (newImage.Height * Scale); 
				PictureBox1.Image = newImage.GetThumbnailImage(w, h, new System.Drawing.Image.GetThumbnailImageAbort(GetThumbnailImageAbort), IntPtr.Zero); 
			} 
			catch (Exception ex) 
			{
                MessageBox.Show("Image is not displayed because:" + Environment.NewLine + ex.Message);
            } 
		} 

		private void ShowError(Exception ex) 
		{ 
			txtRslt.Text +=  "ERROR: " + ex.Message.ToString(); 
		} 

		private string AboutText()
		{
			string s = "";
			s = "ClearImageNet Server " + Server.Major.ToString() +"." + Server.Minor.ToString() + "." + Server.Release.ToString() + " " + Server.Edition;
			s = s + Environment.NewLine;
			s = s + Environment.NewLine + "This program demonstartes basic use of ClearImageNet asseembly C#";
			s = s + Environment.NewLine + "   Full assembly reference is in ClearImageNet API Help.";
			s = s + Environment.NewLine + "Use ClearImage Demo to evaluate functionality without writing code";
			s = s + Environment.NewLine + "";
			s = s + Environment.NewLine + "For additional support, send your images and code";
			s = s + Environment.NewLine + "    snippets to 'support@inliteresearch.com'";
			return s;
		}

		private Microsoft.Win32.RegistryKey GetRegKey()
			{
				// Create key in HKLM without Release
			Microsoft.Win32.RegistryKey key;
			key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey("Software\\Inlite\\" + Application.ProductName);
			return key;
			}
#endregion

        private void cmdBcFromStream_Click(object sender, EventArgs e)
        {
            try
            {
                if (!OpStart(sender)) return;
                //    Do processing
                 string s = "";
                 if  (optClearImageNet.Checked) 
                   s = ciNetProc.Read1DPro_Stream (txtFileIn_.Text);
	             else
                   s = ciProc.Read1DPro_Stream(txtFileIn_.Text);
                // Display results
                OpDone(s);
            }
            catch (Exception ex)
            {
                ShowError(ex);
            } 
		
        }

        private void cmdBc_Click(object sender, EventArgs e)
        {
            try
            {
                if (!OpStart(sender)) return;
                //    Do processing
                 string s = "";
                 if  (optClearImageNet.Checked) 
                   s = ciNetProc.Read1DPro_File(txtFileIn_.Text);
	             else
                   s = ciProc.Read1DPro_File(txtFileIn_.Text);
                // Display results
                OpDone(s);
            }
            catch (Exception ex)
            {
                ShowError(ex);
            } 
		
        }

        private void cmdBcThreads_Click(object sender, EventArgs e)
        {
            try
            {
                if (!OpStart(sender)) return;
                //    Do processing
                 string s = "";
                 if  (optClearImageNet.Checked) 
                   s = ciNetProc.Read1DPro_Directory_Threads(System.IO.Path.GetDirectoryName(txtFileIn_.Text));
	             else
                   s = ciProc.Read1DPro_Directory_Threads(System.IO.Path.GetDirectoryName(txtFileIn_.Text));
                // Display results
                OpDone(s);
            }
            catch (Exception ex)
            {
                ShowError(ex);
            } 
		 }

        private void cmdRepairPage_Click(object sender, EventArgs e)
        {
            try
            {
                if (!OpStart(sender)) return;
                //
                ImageFileFormat format = ImageFileFormat.inputFileFormat;
                Inlite.ClearImage.EFileFormat ciFormat = Inlite.ClearImage.EFileFormat.ciEXT;
                string fileOut = GetRepairFile(txtFileIn_.Text, ref format, ref ciFormat, true);
                //    Do processing
                 string s = "";
                 if  (optClearImageNet.Checked) 
                   s = ciNetProc.Repair_Page(txtFileIn_.Text, (int)numPage.Value, fileOut, format);
	             else
                   s = ciProc.Repair_Page(txtFileIn_.Text, (int)numPage.Value, fileOut, ciFormat);
                // Display results
                OpDone(s);
                if (s != "")
                    DisplayImage(fileOut);
            }
            catch (Exception ex)
            {
                ShowError(ex);
            } 
        }

        private string GetRepairFile(string inpFile, ref ImageFileFormat format,
            ref Inlite.ClearImage.EFileFormat ciFormat, bool delete)
        {
            string ext = ""; 
            switch (cmbOutFormat.SelectedIndex)
            {
                case 0:
                    ext = System.IO.Path.GetExtension(inpFile);
                    format = ImageFileFormat.inputFileFormat;
                    ciFormat = Inlite.ClearImage.EFileFormat.ciEXT;
                    break;
                case 1:
                    ext = ".pdf";
                    format = ImageFileFormat.pdf;
                    ciFormat = Inlite.ClearImage.EFileFormat.cifPDF;
                    break;
                case 2:
                    ext = ".tif";
                    format = ImageFileFormat.tiff;
                    ciFormat = Inlite.ClearImage.EFileFormat.ciTIFF;
                    break;
                case 3:
                    ext = ".jpg";
                    format = ImageFileFormat.jpeg;
                    ciFormat = Inlite.ClearImage.EFileFormat.ciJPG;
                    break;
               }
            string fileOut = System.IO.Path.GetTempPath() + @"CiRepair" + ext;
            if (delete)
                System.IO.File.Delete(fileOut);
            txtRslt.Text  = txtRslt.Text + "Output in: " + fileOut + Environment.NewLine;
            txtRslt.Text  = txtRslt.Text + "------------------------" + Environment.NewLine;
            return fileOut;
        }

        private void cmdRepairStream_Click(object sender, EventArgs e)
        {
            try
            {
                if (!OpStart(sender)) return;
                //
                ImageFileFormat format = ImageFileFormat.inputFileFormat;
                Inlite.ClearImage.EFileFormat ciFormat = Inlite.ClearImage.EFileFormat.ciEXT;
                string fileOut = GetRepairFile(txtFileIn_.Text, ref format, ref ciFormat, true);
                //    Do processing
                 string s = "";
                 if  (optClearImageNet.Checked) 
                   s = ciNetProc.Repair_Stream(txtFileIn_.Text, fileOut, format);
	             else
                   s = ciProc.Repair_Stream (txtFileIn_.Text, fileOut, ciFormat);
                // Display results
                OpDone(s);
                if (s != "")
                    DisplayImage(fileOut);
            }
            catch (Exception ex)
            {
                ShowError(ex);
            } 
        }

		private void cmdDrvLic_Click(object sender, EventArgs e)
		{
			try
			{
				if (!OpStart(sender)) return;
				//    Do processing
				string s = "";
				if (optClearImageNet.Checked)
					s = ciNetProc.ReadDrvLic_Page(txtFileIn_.Text, (int)numPage.Value);
				else
					s = ciProc.ReadDrvLic_Page(txtFileIn_.Text, (int)numPage.Value);

				// Display results
				OpDone(s);
			}
			catch (Exception ex)
			{
				ShowError(ex);
			} 
		}
	}
}
