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
    public partial class Product_Data : Form
    {
        public Product_Data()
        {
            InitializeComponent();
        }
        DataTable data2 = new DataTable("Production");
        private void Product_Data_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'factory_DatabaseDataSet2.Product' table. You can move, or remove it, as needed.
            this.productTableAdapter.Fill(this.factory_DatabaseDataSet2.Product);
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                DataTable t = new DataTable();
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = "Data Source=DESKTOP-9E272F5\\MSSQLSERVER01;Initial Catalog=Factory Database;Integrated Security=True";
                string sslect = string.Format("SELECT * FROM Product Where Product_Name like '%" + textBox2.Text + "%'");
                conn.Open();
                SqlDataAdapter data2 = new SqlDataAdapter(sslect, conn);
                data2.Fill(t);
                productDataGridView.DataSource = t;
                conn.Close();
            }
        }

        private void productBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.productBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.factory_DatabaseDataSet2);
            DataGridViewRow row = productDataGridView.Rows[selected1];
            int Material_ID = (int)row.Cells[0].Value;
            string Material_Name = row.Cells[1].Value.ToString();
            string Material_Descreption = row.Cells[2].Value.ToString();
            string Material_Quantity = row.Cells[3].Value.ToString();
            string Material_Cost = row.Cells[4].Value.ToString();
            string Material_Categoery= row.Cells[5].Value.ToString();
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = "Data Source=DESKTOP-9E272F5\\MSSQLSERVER01;Initial Catalog=Factory Database;Integrated Security=True";
            string sslect2 = string.Format("UPDATE Product SET Product_ID ='" + Material_ID + "', Product_Name = '" + Material_Name + "', Product_Descreption = '" + Material_Descreption + "', Product_Quantity = '" + Material_Quantity + "', Product_Cost = '" + Material_Cost + "' Product_Categoery = '" + Material_Categoery + "' WHERE Product_ID = '" + Material_ID + "'");
            SqlCommand comselect2 = new SqlCommand(sslect2, conn);
            conn.Open();
            comselect2.ExecuteNonQuery();
            conn.Close();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        int selected1 = 0;
        private void productDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            selected1 = e.RowIndex;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            New_Product i = new New_Product();
            i.Show();
        }
        private void toolStripButton1_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}