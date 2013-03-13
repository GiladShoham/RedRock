using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RedRock
{
    public partial class MangeUsers : Form
    {
        BindingList<User> users = new BindingList<User>();

        public MangeUsers()
        {
            InitializeComponent();
        }

        private void MangeUsers_Load(object sender, EventArgs e)
        {
            users.Add(new User(123, "gilad shoham", 0524219320, "shoham.gilad@gmail.com"));
            
            dataGridView1 = new DataGridView();
            dataGridView1.AutoGenerateColumns = false;

            DataGridViewTextBoxColumn idColumn = new DataGridViewTextBoxColumn();
            idColumn.DataPropertyName = "ColID";
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

            dataGridView1.Columns.Add(idColumn);
            dataGridView1.Columns.Add(nameColumn);
            dataGridView1.Columns.Add(phoneColumn);
            dataGridView1.Columns.Add(mailColumn);
            dataGridView1.Columns.Add(keyColumn);

            dataGridView1.DataSource = users;
        }

        private void addUserToTable(User user)
        {
            //this.dataGridView1.add
        }

        private void dataGridView1_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {

           // User user = new User(Convert.ToInt32(e.Row.Cells[0].Value), e.Row.Cells[1].Value.ToString(), Convert.ToInt32(e.Row.Cells[2].Value), e.Row.Cells[3].Value.ToString());
        }

        private void dataGridView1_RowLeave(object sender, DataGridViewCellEventArgs e)
        {
            int id = Convert.ToInt32(this.dataGridView1.Rows[e.RowIndex].Cells["ColID"].Value);
            string name = this.dataGridView1.Rows[e.RowIndex].Cells["ColName"].Value.ToString();
            int phone = Convert.ToInt32(this.dataGridView1.Rows[e.RowIndex].Cells["ColPhone"].Value);
            string mail = this.dataGridView1.Rows[e.RowIndex].Cells["ColMail"].Value.ToString();
            //e.RowIndex
            User user = new User(id, name, phone, mail);
        }

        private void ValidateRow(DataGridViewRow row)
        {
            bool result = false;
            //if 

        }
    }
}
