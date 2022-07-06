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

namespace WindowsFormsApp1
{
    public partial class Log_In : Form
    {
        public Log_In()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = "Data Source=DESKTOP-9E272F5\\MSSQLSERVER01;Initial Catalog=Factory Database;Integrated Security=True";
            string select = "SELECT * FROM [Log_in] " + "WHERE User_Name = '" + textBox1.Text + "' AND Password = '" + textBox2.Text + "'";
            SqlCommand cmdselect = new SqlCommand(select, conn);
            SqlDataReader reader;
            try
            {
                conn.Open();
                reader = cmdselect.ExecuteReader();
                if (reader.Read())
                {
                    Index i = new Index();
                    i.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("User Name and/or Password is Incorrect, Please try again !!", "Warning Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                conn.Close();
            }
            catch (SqlException err)
            {
                MessageBox.Show(err.Message, "Warning Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
