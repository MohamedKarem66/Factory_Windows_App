using Factory_Automation_Integrated_System;
using Factory_Automation_Integrated_System.SHOW_DATA;
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

namespace WindowsFormsApp1.MAIN_FUNCTION
{
    public partial class New_Sale : Form
    {
        public New_Sale()
        {
            InitializeComponent();
        }

        private void New_Sale_Load(object sender, EventArgs e)
        {
            textBox2.Text = DateTime.Now.ToString();
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = "Data Source=DESKTOP-9E272F5\\MSSQLSERVER01;Initial Catalog=Factory Database;Integrated Security=True";
            string sslect = string.Format("SELECT MAX(Sales_ID) FROM Sales");
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
            string s = Convert.ToString(id);
            textBox1.Text = s;
        }
        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = "Data Source=DESKTOP-9E272F5\\MSSQLSERVER01;Initial Catalog=Factory Database;Integrated Security=True";
                string sslect2 = string.Format("SELECT * FROM Customer Where Customer_ID ='" + textBox3.Text + "'");
                SqlCommand comselect2 = new SqlCommand(sslect2, conn);
                SqlDataReader reader2;
                conn.Open();
                reader2 = comselect2.ExecuteReader();
                if (reader2.Read())
                {
                    textBox3.Text = (string)reader2.GetValue(0).ToString();
                    textBox4.Text = (string)reader2.GetValue(1).ToString();
                    textBox6.Text = (string)reader2.GetValue(2).ToString();
                    textBox5.Text = (string)reader2.GetValue(3);
                    purchase_BillDataGridView.Visible = true;
                }
                else
                {
                    MessageBox.Show("Customer ID isn't True Please try Again !!", "Warning Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    New_Customer i = new New_Customer();
                    i.Show();
                }
                conn.Close();
            }
        }

        private void textBox8_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                Boolean i = true;
                if (string.IsNullOrWhiteSpace(textBox3.Text))
                {
                    i = false;
                    MessageBox.Show("Customer ID is requred.", "Warning Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (string.IsNullOrWhiteSpace(textBox14.Text))
                {
                    i = false;
                    MessageBox.Show("Product ID is requred.", "Warning Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                int cost = 0;
                int Quantity = 0;
                string name = "";
                int total = 0;
                int totalbill = Convert.ToInt32(textBox15.Text);
                if (i == true)
                {
                    SqlConnection conn = new SqlConnection();
                    conn.ConnectionString = "Data Source=DESKTOP-9E272F5\\MSSQLSERVER01;Initial Catalog=Factory Database;Integrated Security=True";
                    int id = Convert.ToInt32(textBox1.Text);
                    int qadd = Convert.ToInt32(textBox8.Text);
                    string sslect2 = string.Format("SELECT * FROM Product Where Product_ID ='" + textBox14.Text + "'");
                    SqlCommand comselect2 = new SqlCommand(sslect2, conn);
                    SqlDataReader reader2;
                    conn.Open();
                    reader2 = comselect2.ExecuteReader();
                    if (reader2.Read())
                    {
                        Quantity = (int)reader2.GetValue(3);
                        cost = (int)reader2.GetValue(4);
                        name = (string)reader2.GetValue(1);
                    }
                    else
                    {
                        MessageBox.Show("Product ID isn't true !!", "Warning Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    conn.Close();
                    total = qadd * cost;
                    Quantity = Quantity - qadd;
                    string sslect3 = string.Format("UPDATE Product SET Product_Quantity ='" + Quantity + "' Where Product_ID ='" + textBox14.Text + "'");
                    SqlCommand comselect3 = new SqlCommand(sslect3, conn);
                    conn.Open();
                    comselect3.ExecuteNonQuery();
                    conn.Close();
                    string strins = string.Format("INSERT INTO Sale_Bill VALUES ('{0}','{1}','{2}','{3}','{4}','{5}')", id, textBox14.Text, name, qadd, cost, total);
                    SqlCommand cmd = new SqlCommand(strins, conn);
                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        int id2 = Convert.ToInt32(textBox14.Text);
                        textBox14.Text = "";
                        textBox8.Text = "";
                        totalbill = totalbill + total;
                        textBox15.Text = Convert.ToString(totalbill);
                        textBox12.Text = Convert.ToString(totalbill);
                        purchase_BillDataGridView.Rows.Add(id2, name, qadd, cost, total);
                    }
                    catch (SqlException err)
                    {
                        MessageBox.Show(err.Message, "Warning Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Boolean i = true;
            if (string.IsNullOrWhiteSpace(textBox3.Text))
            {
                i = false;
                MessageBox.Show("Customer ID is requred.", "Warning Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            if (i == true)
            {
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = "Data Source=DESKTOP-9E272F5\\MSSQLSERVER01;Initial Catalog=Factory Database;Integrated Security=True";
                int id = Convert.ToInt32(textBox1.Text);
                string s = textBox12.Text;
                string strins = string.Format("INSERT INTO Sales VALUES ('{0}','{1}','{2}','{3}','{4}','{5}')", id, textBox2.Text, textBox10.Text, textBox3.Text, textBox13.Text, s);
                SqlCommand cmd = new SqlCommand(strins, conn);
                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    int t = Convert.ToInt32(textBox12.Text);
                    int t2 = Convert.ToInt32(textBox4.Text);
                    t = t + t2;
                    int t1 = Convert.ToInt32(textBox16.Text);
                    t = t - t1;
                    string sslect3 = string.Format("UPDATE Customer SET Customer_Ballance ='" + t + "' Where Customer_ID ='" + textBox3.Text + "'");
                    SqlCommand comselect3 = new SqlCommand(sslect3, conn);
                    conn.Open();
                    comselect3.ExecuteNonQuery();
                    conn.Close();
                    this.Close();
                    New_Sale p = new New_Sale();
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
            this.Close();
        }
        int selected1 = 0;
        private void purchase_BillDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            selected1 = e.RowIndex;
        }
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = purchase_BillDataGridView.Rows[selected1];
            string Material_ID = row.Cells[0].Value.ToString();
            int cost = (int)row.Cells[4].Value;
            int Q = (int)row.Cells[2].Value;
            int t = Convert.ToInt32(textBox15.Text);
            t = t - cost;
            textBox15.Text = "" + t;
            textBox16.Text = "" + t;
            textBox12.Text = "" + t;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = "Data Source=DESKTOP-9E272F5\\MSSQLSERVER01;Initial Catalog=Factory Database;Integrated Security=True";
            string sslect2 = string.Format("DELETE FROM Sale_Bill Where Product_ID ='" + Material_ID + "' AND Sale_ID = '" + textBox1.Text + "'");
            SqlCommand comselect2 = new SqlCommand(sslect2, conn);
            conn.Open();
            comselect2.ExecuteNonQuery();
            conn.Close();
            purchase_BillDataGridView.Rows.RemoveAt(row.Index);
            string sslect = string.Format("SELECT Product_Quantity FROM Product WHERE Product_ID = '" + Material_ID + "'");
            SqlCommand comselect = new SqlCommand(sslect, conn);
            SqlDataReader reader;
            conn.Open();
            reader = comselect.ExecuteReader();
            int Q1 = 0;
            if (reader.Read())
                Q1 = (int)reader.GetValue(0);
            reader.Close();
            conn.Close();
            Q1 = Q1 + Q;
            string sslect1 = string.Format("UPDATE Product SET Product_Quantity = '" + Q1 + "' WHERE Product_ID = '" + Material_ID + "'");
            SqlCommand comselect3 = new SqlCommand(sslect1, conn);
            conn.Open();
            comselect3.ExecuteNonQuery();
            conn.Close();
        }
        private void textBox13_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                int t = Convert.ToInt32(textBox15.Text);
                int t2 = Convert.ToInt32(textBox13.Text);
                t = t - t2;
                textBox12.Text = "" + t;
                textBox16.Text = "" + t;
            }
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            Sale_Data i = new Sale_Data();
            i.Show();
        }
    }
}
