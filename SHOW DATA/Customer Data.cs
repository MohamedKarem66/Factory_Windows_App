using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Factory_Automation_Integrated_System
{
    public partial class Customer_Data : Form
    {
        public Customer_Data()
        {
            InitializeComponent();
        }
        private void Customer_Data_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'factory_DatabaseDataSet3.Customer' table. You can move, or remove it, as needed.
            this.customerTableAdapter.Fill(this.factory_DatabaseDataSet3.Customer);
            // TODO: This line of code loads data into the 'factory_DatabaseDataSet3.Customer' table. You can move, or remove it, as needed.
            this.customerTableAdapter.Fill(this.factory_DatabaseDataSet3.Customer);
            // TODO: This line of code loads data into the 'factory_DatabaseDataSet.Customer' table. You can move, or remove it, as needed.
            this.customerTableAdapter.Fill(this.factory_DatabaseDataSet3.Customer);
        }
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (char)13)
            {
                DataTable t = new DataTable();
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = "Data Source=DESKTOP-9E272F5\\MSSQLSERVER01;Initial Catalog=Factory Database;Integrated Security=True";
                string sslect = string.Format("SELECT * FROM Customer Where Customer_Name like '%"+textBox1.Text+"%'");
                conn.Open();
                SqlDataAdapter data2 = new SqlDataAdapter(sslect, conn);
                data2.Fill(t);
                customerDataGridView.DataSource = t;
                conn.Close();
            }
        }

        private void fillByToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.customerTableAdapter.Fill(this.factory_DatabaseDataSet3.Customer);
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }

        private void fillBy1ToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.customerTableAdapter.FillBy1(this.factory_DatabaseDataSet3.Customer);
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }
        int selected1 = 0;
        private void customerBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.customerBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.factory_DatabaseDataSet3);
            DataGridViewRow row = customerDataGridView.Rows[selected1];
            int Customer_ID = (int)row.Cells[0].Value;
            int Customer_Ballance = (int)row.Cells[1].Value;
            string Customer_Name = row.Cells[2].Value.ToString();
            string Customer_Mobile = row.Cells[3].Value.ToString();
            string Customer_Email = row.Cells[4].Value.ToString();
            string Customer_User_Name = row.Cells[5].Value.ToString();
            string Customer_Password = row.Cells[6].Value.ToString();
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = "Data Source=DESKTOP-9E272F5\\MSSQLSERVER01;Initial Catalog=Factory Database;Integrated Security=True";
            string sslect2 = string.Format("UPDATE Customer SET Customer_ID ='"+Customer_ID+ "', Customer_Ballance = '"+Customer_Ballance+ "', Customer_Name = '"+Customer_Name+ "', Customer_Mobile = '"+Customer_Mobile+ "', [Customer_E-mail] = '"+Customer_Email+ "', Customer_User_Name = '"+Customer_User_Name+ "', Customer_Password = '" + Customer_Password+ "' WHERE Customer_ID = '"+Customer_ID+"'");
            SqlCommand comselect2 = new SqlCommand(sslect2, conn);
            conn.Open();
            comselect2.ExecuteNonQuery();
            conn.Close();
        }

        private void customerDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            selected1 = e.RowIndex;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            New_Customer i = new New_Customer();
            i.Show();
        }

        private void customerBindingNavigatorSaveItem_Click_1(object sender, EventArgs e)
        {
            this.Validate();
            this.customerBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.factory_DatabaseDataSet3);

        }

        private void toolStripButton1_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}