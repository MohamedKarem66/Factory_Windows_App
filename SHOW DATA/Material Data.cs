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
    public partial class Material_Data : Form
    {
        public Material_Data()
        {
            InitializeComponent();
        }
        private void Material_Data_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'factory_DatabaseDataSet.Material' table. You can move, or remove it, as needed.
            this.materialTableAdapter.Fill(this.factory_DatabaseDataSet.Material);
        }
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                DataTable t = new DataTable();
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = "Data Source=DESKTOP-9E272F5\\MSSQLSERVER01;Initial Catalog=Factory Database;Integrated Security=True";
                string sslect = string.Format("SELECT * FROM Material Where Material_Name like '%" + textBox1.Text + "%'");
                conn.Open();
                SqlDataAdapter data2 = new SqlDataAdapter(sslect, conn);
                data2.Fill(t);
                materialDataGridView.DataSource = t;
                conn.Close();
            }
        }
        int selected1 = 0;
        private void materialBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.materialBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.factory_DatabaseDataSet);
            DataGridViewRow row = materialDataGridView.Rows[selected1];
            int Material_ID = (int)row.Cells[0].Value;
            string Material_Name = row.Cells[1].Value.ToString();
            string Material_Descreption = row.Cells[2].Value.ToString();
            string Material_Quantity = row.Cells[3].Value.ToString();
            string Material_Cost = row.Cells[4].Value.ToString();
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = "Data Source=DESKTOP-9E272F5\\MSSQLSERVER01;Initial Catalog=Factory Database;Integrated Security=True";
            string sslect2 = string.Format("UPDATE Material SET Material_ID ='" + Material_ID + "', Material_Name = '" + Material_Name + "', Material_Descreption = '" + Material_Descreption + "', Material_Quantity = '" + Material_Quantity + "', Material_Cost = '" + Material_Cost + "' WHERE Material_ID = '" + Material_ID + "'");
            SqlCommand comselect2 = new SqlCommand(sslect2, conn);
            conn.Open();
            comselect2.ExecuteNonQuery();
            conn.Close();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void materialDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            selected1 = e.RowIndex;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            New_Material i = new New_Material();
            i.Show();
        }
    }
}