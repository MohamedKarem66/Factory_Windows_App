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

namespace Factory_Automation_Integrated_System.SHOW_DATA
{
    public partial class Sale_Data : Form
    {
        public Sale_Data()
        {
            InitializeComponent();
        }
        DataTable data2 = new DataTable("Sales");
        DataTable data3 = new DataTable("Sale_Bill");
        string Sale_ID = "";
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count != -1)
            {
                data3 = new DataTable("Sale_Bill");
                dataGridView2.DataSource = data3;
                SqlConnection conne = new SqlConnection();
                conne.ConnectionString = "Data Source=DESKTOP-9E272F5\\MSSQLSERVER01;Initial Catalog=Factory Database;Integrated Security=True";
                string display1 = string.Format("SELECT * FROM Sale_Bill WHERE Sale_ID ='" + Sale_ID + "'");
                conne.Open();
                SqlDataAdapter data1 = new SqlDataAdapter(display1, conne);
                data1.Fill(data3);
                dataGridView2.DataSource = data3;
                conne.Close();
            }
            else
            {
                MessageBox.Show("No Bill Selected !! Select a Bill, Then Click The Button Select", "Warning Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void Sale_Data_Load(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = "Data Source=DESKTOP-9E272F5\\MSSQLSERVER01;Initial Catalog=Factory Database;Integrated Security=True";
            string sslect = string.Format("SELECT * FROM Sales");
            conn.Open();
            SqlDataAdapter data1 = new SqlDataAdapter(sslect, conn);
            data1.Fill(data2);
            dataGridView1.DataSource = data2;
            conn.Close();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                DataView data3 = data2.DefaultView;
                data3.RowFilter = string.Format("Sales_Customer_ID = '" + textBox1.Text + "'");
                dataGridView1.DataSource = data3.ToTable();
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int selected1 = e.RowIndex;
            DataGridViewRow row = dataGridView1.Rows[selected1];
            Sale_ID = row.Cells[0].Value.ToString();
        }
    }
}
