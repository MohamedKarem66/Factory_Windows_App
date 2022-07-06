using Factory_Automation_Integrated_System;
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

namespace WindowsFormsApp1.SHOW_DATA
{
    public partial class Supplier_Data : Form
    {
        public Supplier_Data()
        {
            InitializeComponent();
        }

        private void supplierBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.supplierBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.factory_DatabaseDataSet);
            DataGridViewRow row = supplierDataGridView.Rows[selected1];
            int Supplier_ID = (int)row.Cells[0].Value;
            int Supplier_Ballance = (int)row.Cells[1].Value;
            string Supplier_Name = row.Cells[2].Value.ToString();
            string Supplier_Mobile = row.Cells[3].Value.ToString();
            string Supplier_Email = row.Cells[4].Value.ToString();
            string Supplier_User_Name = row.Cells[5].Value.ToString();
            string Supplier_Password = row.Cells[6].Value.ToString();
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = "Data Source=DESKTOP-9E272F5\\MSSQLSERVER01;Initial Catalog=Factory Database;Integrated Security=True";
            string sslect2 = string.Format("UPDATE Supplier SET Supplier_ID ='" + Supplier_ID + "', Supplier_Ballance = '" + Supplier_Ballance + "', Customer_Name = '" + Supplier_Name + "', Supplier_Mobile = '" + Supplier_Mobile + "', [Customer_E-mail] = '" + Supplier_Email + "', Supplier_User_Name = '" + Supplier_User_Name + "', Supplier_Password = '" + Supplier_Password + "' WHERE Supplier_ID = '" + Supplier_ID + "'");
            SqlCommand comselect2 = new SqlCommand(sslect2, conn);
            conn.Open();
            comselect2.ExecuteNonQuery();
            conn.Close();
        }

        private void Supplier_Data_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'factory_DatabaseDataSet.Supplier' table. You can move, or remove it, as needed.
            this.supplierTableAdapter.Fill(this.factory_DatabaseDataSet.Supplier);
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                DataTable t = new DataTable();
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = "Data Source=DESKTOP-9E272F5\\MSSQLSERVER01;Initial Catalog=Factory Database;Integrated Security=True";
                string sslect = string.Format("SELECT * FROM Supplier Where Supplier_Name like '%" + textBox1.Text + "%'");
                conn.Open();
                SqlDataAdapter data2 = new SqlDataAdapter(sslect, conn);
                data2.Fill(t);
                supplierDataGridView.DataSource = t;
                conn.Close();
            }
        }
        int selected1 = 0;

        private void supplierDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            selected1 = e.RowIndex;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            New_Supplier i = new New_Supplier();
            i.Show();
        }
    }
}