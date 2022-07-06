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
    public partial class Production_Data : Form
    {
        public Production_Data()
        {
            InitializeComponent();
        }
        DataTable data4 = new DataTable("ProductionP_Bill");
        DataTable data2 = new DataTable("Production");
        DataTable data3 = new DataTable("ProductionM_Bill");
        string Production_ID = "";
        private void Production_Data_Load(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = "Data Source=DESKTOP-9E272F5\\MSSQLSERVER01;Initial Catalog=Factory Database;Integrated Security=True";
            string sslect = string.Format("SELECT * FROM Production");
            conn.Open();
            SqlDataAdapter data1 = new SqlDataAdapter(sslect, conn);
            data1.Fill(data2);
            dataGridView1.DataSource = data2;
            conn.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int selected1 = e.RowIndex;
            DataGridViewRow row = dataGridView1.Rows[selected1];
            Production_ID = row.Cells[0].Value.ToString();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                DataView data3 = data2.DefaultView;
                data3.RowFilter = string.Format("Production_Employee_ID = '" + textBox1.Text + "'");
                dataGridView1.DataSource = data3.ToTable();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count != -1)
            {
                data3 = new DataTable("ProductionM_Bill");
                dataGridView2.DataSource = data3;
                data4 = new DataTable("ProductionP_Bill");
                dataGridView3.DataSource = data4;
                SqlConnection conne = new SqlConnection();
                conne.ConnectionString = "Data Source=DESKTOP-9E272F5\\MSSQLSERVER01;Initial Catalog=Factory Database;Integrated Security=True";
                string display1 = string.Format("SELECT * FROM ProductionM_Bill WHERE Production_ID ='" + Production_ID + "'");
                conne.Open();
                SqlDataAdapter data1 = new SqlDataAdapter(display1, conne);
                data1.Fill(data3);
                dataGridView2.DataSource = data3;
                conne.Close();
                string display2 = string.Format("SELECT * FROM ProductionP_Bill WHERE Production_ID ='" + Production_ID + "'");
                conne.Open();
                SqlDataAdapter data2 = new SqlDataAdapter(display2, conne);
                data2.Fill(data4);
                dataGridView3.DataSource = data4;
                conne.Close();
            }
            else
            {
                MessageBox.Show("No Bill Selected !! Select a Bill, Then Click The Button Select", "Warning Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}