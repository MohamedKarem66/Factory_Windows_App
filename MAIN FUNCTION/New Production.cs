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
using WindowsFormsApp1.SHOW_DATA;

namespace WindowsFormsApp1.MAIN_FUNCTION
{
    public partial class New_Production : Form
    {
        public New_Production()
        {
            InitializeComponent();
        }
        int selected1 = 0;
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = purchase_BillDataGridView.Rows[selected1];
            string Material_ID = row.Cells[0].Value.ToString();
            int cost = (int)row.Cells[4].Value;
            int Q = (int)row.Cells[2].Value;
            int t = Convert.ToInt32(textBox9.Text);
            t = t - cost;
            textBox9.Text = "" + t;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = "Data Source=DESKTOP-9E272F5\\MSSQLSERVER01;Initial Catalog=Factory Database;Integrated Security=True";
            string sslect2 = string.Format("DELETE FROM ProductionM_Bill Where Material_ID ='" + Material_ID + "' AND Production_ID = '" + textBox1.Text + "'");
            SqlCommand comselect2 = new SqlCommand(sslect2, conn);
            conn.Open();
            comselect2.ExecuteNonQuery();
            conn.Close();
            purchase_BillDataGridView.Rows.RemoveAt(row.Index);
            string sslect = string.Format("SELECT Material_Quantity FROM Material WHERE Material_ID = '" + Material_ID + "'");
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
            string sslect1 = string.Format("UPDATE Material SET Material_Quantity = '" + Q1 + "' WHERE Material_ID = '" + Material_ID + "'");
            SqlCommand comselect3 = new SqlCommand(sslect1, conn);
            conn.Open();
            comselect3.ExecuteNonQuery();
            conn.Close();
        }
        private void purchase_BillDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            selected1 = e.RowIndex;
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            Production_Data i = new Production_Data();
            i.Show();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            DialogResult iExit1;
            iExit1 = MessageBox.Show("Confirm if you want to Exit", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (iExit1 == DialogResult.Yes)
            {
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = "Data Source=DESKTOP-9E272F5\\MSSQLSERVER01;Initial Catalog=Factory Database;Integrated Security=True";
                string sslect2 = string.Format("DELETE FROM ProductionM_Bill Where Production_ID ='" + textBox1.Text + "'");
                SqlCommand comselect2 = new SqlCommand(sslect2, conn);
                try
                {
                    conn.Open();
                    comselect2.ExecuteNonQuery();
                    conn.Close();
                    string sslect = string.Format("DELETE FROM ProductionP_Bill Where Production_ID ='" + textBox1.Text + "'");
                    SqlCommand comselect = new SqlCommand(sslect, conn);
                    conn.Open();
                    comselect.ExecuteNonQuery();
                    conn.Close();
                }
                catch (SqlException err)
                {
                    MessageBox.Show(err.Message, "Warning Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                this.Close();
            }
        }

        private void New_Production_Load(object sender, EventArgs e)
        {
            textBox2.Text = DateTime.Now.ToString();
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = "Data Source=DESKTOP-9E272F5\\MSSQLSERVER01;Initial Catalog=Factory Database;Integrated Security=True";
            string sslect = string.Format("SELECT MAX(Production_ID) FROM Production");
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
                string sslect2 = string.Format("SELECT * FROM Employee Where Employee_Id ='" + textBox3.Text + "'");
                SqlCommand comselect2 = new SqlCommand(sslect2, conn);
                SqlDataReader reader2;
                conn.Open();
                reader2 = comselect2.ExecuteReader();
                if (reader2.Read())
                {
                    textBox3.Text = (string)reader2.GetValue(0).ToString();
                    textBox6.Text = (string)reader2.GetValue(1);
                    textBox5.Text = (string)reader2.GetValue(2).ToString();
                    textBox4.Text = (string)reader2.GetValue(3);
                    purchase_BillDataGridView.Visible = true;
                }
                else
                {
                    MessageBox.Show("Employee ID isn't True Please try Again !!", "Warning Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    New_Employee i = new New_Employee();
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
                    MessageBox.Show("Employee ID is requred.", "Warning Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (string.IsNullOrWhiteSpace(textBox14.Text))
                {
                    i = false;
                    MessageBox.Show("Material ID is requred.", "Warning Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                int cost = 0;
                int Quantity = 0;
                string name = "";
                int total = 0;
                int totalbill = Convert.ToInt32(textBox9.Text);
                if (i == true)
                {
                    SqlConnection conn = new SqlConnection();
                    conn.ConnectionString = "Data Source=DESKTOP-9E272F5\\MSSQLSERVER01;Initial Catalog=Factory Database;Integrated Security=True";
                    int id = Convert.ToInt32(textBox1.Text);
                    int qadd = Convert.ToInt32(textBox8.Text);
                    string sslect2 = string.Format("SELECT * FROM Material Where Material_Id ='" + textBox14.Text + "'");
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
                        MessageBox.Show("Material ID isn't true !!", "Warning Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    conn.Close();
                    total = qadd * cost;
                    Quantity = Quantity - qadd;
                    string sslect3 = string.Format("UPDATE Material SET Material_Quantity ='" + Quantity + "' Where Material_Id ='" + textBox14.Text + "'");
                    SqlCommand comselect3 = new SqlCommand(sslect3, conn);
                    conn.Open();
                    comselect3.ExecuteNonQuery();
                    conn.Close();
                    string strins = string.Format("INSERT INTO ProductionM_Bill VALUES ('{0}','{1}','{2}','{3}','{4}','{5}')", id, textBox14.Text, name, qadd, cost, total);
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
                        textBox9.Text = Convert.ToString(totalbill);
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
            panel3.Visible = true;
            textBox14.Enabled = false;
            textBox8.Enabled = false;
        }

        private void textBox7_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                Boolean i = true;
                if (string.IsNullOrWhiteSpace(textBox11.Text))
                {
                    i = false;
                    MessageBox.Show("Product ID is requred.", "Warning Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                if (i == true)
                {
                    textBox11.Enabled = false;
                    textBox7.Enabled = false;
                    panel5.Visible = true;
                    int cost = 0;
                    int Quantity = 0;
                    int Catogoery = 0;
                    string name = "";
                    int id = Convert.ToInt32(textBox11.Text);
                    int qadd = Convert.ToInt32(textBox7.Text);
                    int totalbill = Convert.ToInt32(textBox9.Text);
                    cost = totalbill / qadd;
                    SqlConnection conn = new SqlConnection();
                    conn.ConnectionString = "Data Source=DESKTOP-9E272F5\\MSSQLSERVER01;Initial Catalog=Factory Database;Integrated Security=True";
                    string sslect2 = string.Format("SELECT * FROM Product Where Product_Id ='" + id + "'");
                    SqlCommand comselect2 = new SqlCommand(sslect2, conn);
                    SqlDataReader reader2;
                    conn.Open();
                    reader2 = comselect2.ExecuteReader();
                    if (reader2.Read())
                    {
                        Quantity = (int)reader2.GetValue(3);
                        Catogoery = (int)reader2.GetValue(5);
                        name = (string)reader2.GetValue(1);
                    }
                    else
                    {
                        MessageBox.Show("Product ID isn't true !!", "Warning Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    conn.Close();
                    Quantity = Quantity + qadd;
                    string sslect3 = string.Format("UPDATE Product SET Product_Quantity ='" + Quantity + "' Where Product_Id ='" + id + "'");
                    SqlCommand comselect3 = new SqlCommand(sslect3, conn);
                    conn.Open();
                    comselect3.ExecuteNonQuery();
                    conn.Close();
                    string sslect4 = string.Format("UPDATE Product SET Product_Cost ='" + cost + "' Where Product_Id ='" + id + "'");
                    SqlCommand comselect4 = new SqlCommand(sslect4, conn);
                    conn.Open();
                    comselect4.ExecuteNonQuery();
                    conn.Close();
                    textBox13.Text = "" + name;
                    textBox12.Text = "" + cost;
                    textBox15.Text = "" + Catogoery;
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Boolean i = true;
            if (string.IsNullOrWhiteSpace(textBox3.Text))
            {
                i = false;
                MessageBox.Show("Employee ID is requred.", "Warning Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            if (i == true)
            {
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = "Data Source=DESKTOP-9E272F5\\MSSQLSERVER01;Initial Catalog=Factory Database;Integrated Security=True";
                int id = Convert.ToInt32(textBox1.Text);
                string strins = string.Format("INSERT INTO Production VALUES ('{0}','{1}','{2}','{3}')", id, textBox2.Text, textBox10.Text, textBox3.Text);
                SqlCommand cmd = new SqlCommand(strins, conn);
                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    string sslect4 = string.Format("UPDATE Product SET Product_Categoery ='" + textBox15.Text + "' Where Product_Id ='" + textBox11.Text + "'");
                    SqlCommand comselect4 = new SqlCommand(sslect4, conn);
                    conn.Open();
                    comselect4.ExecuteNonQuery();
                    conn.Close();
                    string sslect = string.Format("INSERT INTO ProductionP_Bill VALUES ('{0}','{1}','{2}','{3}','{4}','{5}')", textBox1.Text, textBox11.Text, textBox13.Text, textBox12.Text, textBox7.Text, textBox15.Text);
                    SqlCommand comselect = new SqlCommand(sslect, conn);
                    conn.Open();
                    comselect.ExecuteNonQuery();
                    conn.Close();
                    this.Close();
                    New_Production p = new New_Production();
                    p.Show();
                }
                catch (SqlException err)
                {
                    MessageBox.Show(err.Message, "Warning Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void textBox15_KeyPress(object sender, KeyPressEventArgs e)
        {
            button3.Visible = true;
        }
    }
}