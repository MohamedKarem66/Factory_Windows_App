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
    public partial class Employee_Data : Form
    {
        public Employee_Data()
        {
            InitializeComponent();
        }
        DataTable data2 = new DataTable("Customer");
        private void Employee_Data_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'factory_DatabaseDataSet.Employee' table. You can move, or remove it, as needed.
            this.employeeTableAdapter.Fill(this.factory_DatabaseDataSet.Employee);
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                DataTable t = new DataTable();
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = "Data Source=DESKTOP-9E272F5\\MSSQLSERVER01;Initial Catalog=Factory Database;Integrated Security=True";
                string sslect = string.Format("SELECT * FROM Employee Where Employee_Name like '%" + textBox1.Text + "%'");
                conn.Open();
                SqlDataAdapter data2 = new SqlDataAdapter(sslect, conn);
                data2.Fill(t);
                employeeDataGridView.DataSource = t;
                conn.Close();
            }
        }
        int selected1 = 0;
        private void employeeBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.employeeBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.factory_DatabaseDataSet);
            DataGridViewRow row = employeeDataGridView.Rows[selected1];
            int Supplier_ID = (int)row.Cells[0].Value;
            string Supplier_Name = row.Cells[2].Value.ToString();
            string Supplier_Mobile = row.Cells[3].Value.ToString();
            string Supplier_Email = row.Cells[4].Value.ToString();
            string Supplier_User_Name = row.Cells[5].Value.ToString();
            string Supplier_Password = row.Cells[6].Value.ToString();
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = "Data Source=DESKTOP-9E272F5\\MSSQLSERVER01;Initial Catalog=Factory Database;Integrated Security=True";
            string sslect2 = string.Format("UPDATE Employee SET Employee_ID ='" + Supplier_ID + "', Employee_Name = '" + Supplier_Name + "', Employee_Mobile = '" + Supplier_Mobile + "', [Employee_E-mail] = '" + Supplier_Email + "', Employee_User_Name = '" + Supplier_User_Name + "', Employee_Password = '" + Supplier_Password + "' WHERE Employee_ID = '" + Supplier_ID + "'");
            SqlCommand comselect2 = new SqlCommand(sslect2, conn);
            conn.Open();
            comselect2.ExecuteNonQuery();
            conn.Close();
        }

        private void employeeDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            selected1 = e.RowIndex;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            New_Employee i = new New_Employee();
            i.Show();
        }
    }
}
