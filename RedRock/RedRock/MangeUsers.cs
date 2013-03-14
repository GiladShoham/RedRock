using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using com.google.zxing;
using com.google.zxing.common;
using com.google.zxing.qrcode;


namespace RedRock
{
    public partial class MangeUsers : Form
    {
        private BindingList<User> users = new BindingList<User>();
        private BindingSource source = new BindingSource();
        ObjectToSerialize objectToSerialize = new ObjectToSerialize();
        Serializer serializer = new Serializer();

        public MangeUsers()
        {
            InitializeComponent();

            DataGridView _dgUsers = new DataGridView();


            //users.Add(new User(123, "gilad shoham", 0524219320, "shoham.gilad@gmail.com"));
            //users.Add(new User(456, "גלעד", 0524219320, "shoham.gilad@gmail.com"));
            //users.Add(new User(456, "גלעד", "0524219320", "shoham.gilad@gmail.com"));
            if (File.Exists("users.txt"))
            {
                objectToSerialize = serializer.DeSerializeObject("users.txt");
                users = new BindingList<User>(objectToSerialize.Users);
            }
            else
            {
                users = new BindingList<User>();
            }

            
            source.DataSource = users;

            //_dgUsers2 = new DataGridView();
            //_dgUsers2.AutoGenerateColumns = false;

            DataGridViewTextBoxColumn idColumn = new DataGridViewTextBoxColumn();
            idColumn.DataPropertyName = "MyID";
            idColumn.HeaderText = "תת.ז";

            DataGridViewTextBoxColumn nameColumn = new DataGridViewTextBoxColumn();
            nameColumn.DataPropertyName = "ColName";
            nameColumn.HeaderText = "שם";

            DataGridViewTextBoxColumn phoneColumn = new DataGridViewTextBoxColumn();
            phoneColumn.DataPropertyName = "ColPhone";
            phoneColumn.HeaderText = "טלפון";

            DataGridViewTextBoxColumn mailColumn = new DataGridViewTextBoxColumn();
            mailColumn.DataPropertyName = "ColMail";
            mailColumn.HeaderText = "מייל";

            DataGridViewTextBoxColumn keyColumn = new DataGridViewTextBoxColumn();
            keyColumn.DataPropertyName = "ColKey";
            keyColumn.HeaderText = "מפתח";

            /*
            _dgUsers2.Columns.Add(idColumn);
            _dgUsers2.Columns.Add(nameColumn);
            _dgUsers2.Columns.Add(phoneColumn);
            _dgUsers2.Columns.Add(mailColumn);
            _dgUsers2.Columns.Add(keyColumn);

            _dgUsers2.DataSource = source;
            */
            //_dgUsers.DataSource = source;

            dataGridView1.AutoGenerateColumns = false;
            //dataGridView1.DataSource = users;

            dataGridView1.DataSource = source;
            //_dgUsers.DataSource = users;

            //dataGridView1.ReadOnly = false;
            dataGridView1.Columns[0].DataPropertyName = "MyID";
            dataGridView1.Columns[1].DataPropertyName = "MyName";
            dataGridView1.Columns[2].DataPropertyName = "MyPhone";
            dataGridView1.Columns[3].DataPropertyName = "MyMail";
            dataGridView1.Columns[4].DataPropertyName = "MyKey";

            //dataGridView1.Columns[0].HeaderText = "תתת";
            //dataGridView1.Columns[0].ReadOnly = false;
            //dataGridView1.Columns[0].
            dataGridView1.AutoGenerateColumns = false;


            //this.Controls.Add(_dgUsers);
        }

        private void MangeUsers_Load(object sender, EventArgs e)
        {
        }

        private void addUserToTable(User user)
        {
            //this._dgUsers2.add
        }

        private void dataGridView1_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            // User user = new User(Convert.ToInt32(e.Row.Cells[0].Value), e.Row.Cells[1].Value.ToString(), Convert.ToInt32(e.Row.Cells[2].Value), e.Row.Cells[3].Value.ToString());
        }

        private void dataGridView1_RowLeave(object sender, DataGridViewCellEventArgs e)
        {
            //int id = Convert.ToInt32(this.dataGridView1.Rows[e.RowIndex].Cells["ColID"].Value);
            //string name = this.dataGridView1.Rows[e.RowIndex].Cells["ColName"].Value.ToString();
            //string phone = this.dataGridView1.Rows[e.RowIndex].Cells["ColPhone"].Value.ToString();
            //string mail = this.dataGridView1.Rows[e.RowIndex].Cells["ColMail"].Value.ToString();
            //e.RowIndex
            User user = ValidateRow(this.dataGridView1.Rows[e.RowIndex]);
            if (user != null)
            {
                AddUpdateUser(user);
                SaveTofile();
                //dataGridView1.DataSource = null;
                //dataGridView1.DataSource = source;
                dataGridView1.EndEdit();
                dataGridView1.Refresh();
            }
            
        }

        public void AddUpdateUser(User user)
        {
            
            int index = -1;
            bool equals = false;
            foreach (var user1 in users)
            {
                if (user1.MyID == user.MyID)
                {
                    if (user.Equals(user1))
                    {
                        equals = true;
                        break;
                    }
                    index = users.IndexOf(user1);
                    
                }
            }

            if (index != -1)
            {
                
                users.RemoveAt(index);
                
            }

            if (equals == false)
            {
                users.Add(user);
            }
            
            
            
        }

        private User ValidateRow(DataGridViewRow row)
        {
            User user = null;


            if (row.Cells["ColID"].Value != null &&
                row.Cells["ColName"].Value != null &&
                row.Cells["ColPhone"].Value != null &&
                row.Cells["ColMail"].Value != null)
            {
                int id = Convert.ToInt32(row.Cells["ColID"].Value);
                string name = row.Cells["ColName"].Value.ToString();
                string phone = row.Cells["ColPhone"].Value.ToString();
                string mail = row.Cells["ColMail"].Value.ToString();
                if (row.Cells["ColKey"].Value != null)
                {
                    string key = row.Cells["ColKey"].Value.ToString();
                    user = new User(id, name, phone, mail, key);
                } else 
                {
                    user = new User(id, name, phone, mail);  
                }
                
            }

            return user;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            
            //save the car list to a file
            
            objectToSerialize.Users = users.ToList();

            
            serializer.SerializeObject("users.txt", objectToSerialize);

            //the car list has been saved to outputFile.txt
            //read the file back from outputFile.txt

            
        }

        private void SaveTofile()
        {
            objectToSerialize.Users = users.ToList();


            serializer.SerializeObject("users.txt", objectToSerialize);
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = dataGridView1.SelectedCells[0].OwningRow;
            row.Cells["ColKey"].Value = null;

            User user = ValidateRow(row);
            if (user != null)
            {
                AddUpdateUser(user);
                SaveTofile();
                //dataGridView1.DataSource = null;
                //dataGridView1.DataSource = source;
                dataGridView1.EndEdit();
                dataGridView1.Refresh();
            }

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = dataGridView1.SelectedCells[0].OwningRow;
            string key = row.Cells["ColKey"].Value.ToString();

            QRCodeWriter qcCode = new QRCodeWriter();
            ByteMatrix btMatrix = qcCode.encode(key, BarcodeFormat.QR_CODE, 570, 570);
            Form keyQR = new KeyQR(btMatrix.ToBitmap());
            keyQR.Show();
            

        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {

        }

        private void btnSendMail_Click(object sender, EventArgs e)
        {
            RRReciver.Main QRsender = new RRReciver.Main();
            DataGridViewRow row = dataGridView1.SelectedCells[0].OwningRow;
            
            //string filePath = "C:/temp/lotem.png";
            //string filePath = "C:/temp/file.txt";
            string filePath = txtFilePath.Text;
            string qrFilePath = QRsender.FileToQRCode(filePath);
            string destination = row.Cells["ColMail"].Value.ToString();

            
            string userName = "Tomer Shohat";
            gmail.email_send(destination, userName, qrFilePath);
            //Send a message with one line of code
            //GmailMessage.SendFromGmail("shoham.gilad", "diablo17", "shoham.gilad@gmail.com", "working", "message body");

            ////Send a message with one line of code with a MailMessage object 
            //RC.Gmail.GmailMessage.SendMailMessageFromGmail("username", "password", mailMessageObject);

            ////Use the GmailMessage object to create and send your message 
            
            //RC.Gmail.GmailMessage gmailMsg = new RC.Gmail.GmailMessage("username", "password");
            //gmailMsg.To = "RCcode@gmail.com";
            //gmailMsg.From = "fromAddress@gmail.com";
            //gmailMsg.Subject = "C# Test Message";
            //gmailMsg.Body = "Test body";

            //MailAttachment attachment = new MailAttachment(@"c:\testfile.txt");
            //gmailMsg.Attachments.Add(attachment);

            //gmailMsg.Send();
        }

        private void btnFileUpload_Click(object sender, EventArgs e)
        {
            DialogResult dg = openFileDialog1.ShowDialog();
            string fileName = openFileDialog1.FileName;
            txtFilePath.Text = fileName;

        }
    }
}