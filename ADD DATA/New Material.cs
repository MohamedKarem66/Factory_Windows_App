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
    public partial class New_Material : Form
    {
        public New_Material()
        {
            InitializeComponent();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
                this.Close();
            
        }

        private void customerBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            Boolean i = true;
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                i = false;
                MessageBox.Show("ID is requred.", "Warning Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (string.IsNullOrWhiteSpace(textBox2.Text))
            {
                i = false;
                MessageBox.Show("Name is requred.", "Warning Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (string.IsNullOrWhiteSpace(textBox3.Text))
            {
                i = false;
                MessageBox.Show("Discreption is requred.", "Warning Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (string.IsNullOrWhiteSpace(textBox4.Text))
            {
                i = false;
                MessageBox.Show("Quantity is requred.", "Warning Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (string.IsNullOrWhiteSpace(textBox5.Text))
            {
                i = false;
                MessageBox.Show("Cost is requred.", "Warning Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            if (i == true)
            {
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = "Data Source=DESKTOP-9E272F5\\MSSQLSERVER01;Initial Catalog=Factory Database;Integrated Security=True";
                int id = Convert.ToInt32(textBox1.Text);
                int Quantity = Convert.ToInt32(textBox4.Text);
                int Cost = Convert.ToInt32(textBox5.Text);
                string strins = string.Format("INSERT INTO Material VALUES ('{0}','{1}','{2}','{3}','{4}')", id, textBox2.Text, textBox3.Text, Quantity, Cost);
                SqlCommand cmd = new SqlCommand(strins, conn);
                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("the Material " + textBox2.Text + " is Save Sucssesfuly !!", "Material Currant", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    textBox1.Text = "";
                    textBox2.Text = "";
                    textBox3.Text = "";
                    textBox4.Text = "";
                    textBox5.Text = "";
                    Material_Data p = new Material_Data();
                    p.Show();
                }
                catch (SqlException err)
                {
                    MessageBox.Show(err.Message, "Warning Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
    }
}
