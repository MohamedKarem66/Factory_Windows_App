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
using WindowsFormsApp1.SHOW_DATA;

namespace Factory_Automation_Integrated_System
{
    public partial class New_Supplier : Form
    {
        public New_Supplier()
        {
            InitializeComponent();
        }

        private void New_Supplier_Load(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = "Data Source=DESKTOP-9E272F5\\MSSQLSERVER01;Initial Catalog=Factory Database;Integrated Security=True";
            string sslect = string.Format("SELECT MAX(Supplier_ID) FROM Supplier");
            SqlCommand comselect = new SqlCommand(sslect, conn);
            SqlDataReader reader;
            conn.Open();
            reader = comselect.ExecuteReader();
            int id = 0;
            if (reader.Read())
                id = (int)reader.GetValue(0);
            reader.Close();
            conn.Close();
            id = id + 1;
            label9.Text = "" + id;
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
        }

        private void customerBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            Boolean i = true;
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                i = false;
                MessageBox.Show("Name is requred.", "Warning Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (string.IsNullOrWhiteSpace(textBox2.Text))
            {
                i = false;
                MessageBox.Show("Mobile is requred.", "Warning Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (string.IsNullOrWhiteSpace(textBox3.Text))
            {
                i = false;
                MessageBox.Show("E-Mail is requred.", "Warning Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (string.IsNullOrWhiteSpace(textBox4.Text))
            {
                i = false;
                MessageBox.Show("User Name is requred.", "Warning Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (string.IsNullOrWhiteSpace(textBox5.Text))
            {
                i = false;
                MessageBox.Show("Password is requred.", "Warning Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (string.IsNullOrWhiteSpace(textBox6.Text))
            {
                i = false;
                MessageBox.Show("Password Conferm is requred.", "Warning Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (textBox5.Text != textBox6.Text)
            {
                i = false;
                MessageBox.Show("The Passwored didn't Match.", "Warning Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (string.IsNullOrWhiteSpace(textBox7.Text))
            {
                i = false;
                MessageBox.Show("Balance is requred.", "Warning Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            if (i == true)
            {
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = "Data Source=DESKTOP-9E272F5\\MSSQLSERVER01;Initial Catalog=Factory Database;Integrated Security=True";
                int id = Convert.ToInt32(label9.Text);
                string strins = string.Format("INSERT INTO Supplier VALUES ('{0}','{1}','{2}','{3}','{4}','{5}','{6}')", id, textBox7.Text, textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text);
                SqlCommand cmd = new SqlCommand(strins, conn);
                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    id += 1;
                    label9.Text = "" + id;
                    MessageBox.Show("the Supplier " + textBox1.Text + " is Save Sucssesfuly !!", "Spplier Currant", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    textBox1.Text = "";
                    textBox2.Text = "";
                    textBox3.Text = "";
                    textBox4.Text = "";
                    textBox5.Text = "";
                    textBox6.Text = "";
                    textBox7.Text = "";
                    Supplier_Data p = new Supplier_Data();
                    p.Show();
                }
                catch (SqlException err)
                {
                    MessageBox.Show(err.Message, "Warning Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
                this.Close();
            
        }
    }
}