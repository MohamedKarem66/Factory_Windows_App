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
    public partial class New_Purchase : Form
    {
        public New_Purchase()
        {
            InitializeComponent();
        }
        private void New_Purchase_Load(object sender, EventArgs e)
        {
            textBox2.Text = DateTime.Now.ToString();
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = "Data Source=DESKTOP-9E272F5\\MSSQLSERVER01;Initial Catalog=Factory Database;Integrated Security=True";
            string sslect = string.Format("SELECT MAX(Purchases_ID) FROM Purchases");
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
                string sslect2 = string.Format("SELECT * FROM Supplier Where Supplier_Id ='" + textBox3.Text + "'");
                SqlCommand comselect2 = new SqlCommand(sslect2, conn);
                SqlDataReader reader2;
                conn.Open();
                reader2 = comselect2.ExecuteReader();
                if (reader2.Read())
                {
                    textBox3.Text = (string)reader2.GetValue(0).ToString();
                    textBox4.Text = (string)reader2.GetValue(1);
                    textBox6.Text = (string)reader2.GetValue(2).ToString();
                    textBox5.Text = (string)reader2.GetValue(3);
                    purchase_BillDataGridView.Visible = true;
                }
                else
                {
                    MessageBox.Show("Supplier ID isn't True Please try Again !!", "Warning Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    New_Supplier i = new New_Supplier();
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
                    MessageBox.Show("Supplier ID is requred.", "Warning Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                    conn.ConnectionString = "Data Source=DESKTOP-9E272F5\\MSSQLSERVER01;Initial Catalog=Factory Database;Integrated Security=True";
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
                    Quantity = Quantity + qadd;
                    string sslect3 = string.Format("UPDATE Material SET Material_Quantity ='"+Quantity+"' Where Material_Id ='" + textBox14.Text + "'");
                    SqlCommand comselect3 = new SqlCommand(sslect3, conn);
                    conn.Open();
                    comselect3.ExecuteNonQuery();
                    conn.Close();
                    string strins = string.Format("INSERT INTO Purchase_Bill VALUES ('{0}','{1}','{2}','{3}','{4}','{5}')", id, textBox14.Text, name, qadd, cost, total);
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
                        textBox7.Text = Convert.ToString(totalbill);
                        textBox12.Text = textBox7.Text;
                        purchase_BillDataGridView.Rows.Add(id2, name, qadd, cost, total);
                    }
                    catch (SqlException err)
                    {
                        MessageBox.Show(err.Message, "Warning Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Boolean i = true;
            if (string.IsNullOrWhiteSpace(textBox3.Text))
            {
                i = false;
                MessageBox.Show("Supplier ID is requred.", "Warning Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            if (i == true)
            {
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = "Data Source=DESKTOP-9E272F5\\MSSQLSERVER01;Initial Catalog=Factory Database;Integrated Security=True";
                int id = Convert.ToInt32(textBox1.Text);
                string strins = string.Format("INSERT INTO Purchases VALUES ('{0}','{1}','{2}','{3}','{4}','{5}')", id, textBox2.Text, textBox10.Text, textBox3.Text, textBox11.Text,textBox7.Text);
                SqlCommand cmd = new SqlCommand(strins, conn);
                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    int t = Convert.ToInt32(textBox7.Text);
                    int t2 = Convert.ToInt32(textBox4.Text);
                    t = t + t2;
                    int t1 = Convert.ToInt32(textBox12.Text);
                    t = t - t1;
                    string sslect3 = string.Format("UPDATE Supplier SET Supplier_Ballance ='" + t + "' Where Supplier_ID ='" + textBox3.Text + "'");
                    SqlCommand comselect3 = new SqlCommand(sslect3, conn);
                    conn.Open();
                    comselect3.ExecuteNonQuery();
                    conn.Close();
                    this.Close();
                    New_Purchase p = new New_Purchase();
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
            DialogResult iExit1;
            iExit1 = MessageBox.Show("Confirm if you want to Exit","Exit",MessageBoxButtons.YesNo,MessageBoxIcon.Information);
            if(iExit1 == DialogResult.Yes)
            {
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = "Data Source=DESKTOP-9E272F5\\MSSQLSERVER01;Initial Catalog=Factory Database;Integrated Security=True";
                string sslect2 = string.Format("DELETE FROM Purchase_Bill Where Purchase_ID ='" + textBox1.Text + "'");
                SqlCommand comselect2 = new SqlCommand(sslect2, conn);
                try
                {
                    conn.Open();
                    comselect2.ExecuteNonQuery();
                    conn.Close();
                }
                catch (SqlException err)
                {
                    MessageBox.Show(err.Message, "Warning Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                this.Close();
            }
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
            int t = Convert.ToInt32(textBox9.Text);
            t = t - cost;
            textBox9.Text = "" + t;
            textBox7.Text = "" + t;
            textBox12.Text = "" + t;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = "Data Source=DESKTOP-9E272F5\\MSSQLSERVER01;Initial Catalog=Factory Database;Integrated Security=True";
            string sslect2 = string.Format("DELETE FROM Purchase_Bill Where Material_ID ='" + Material_ID + "' AND Purchase_ID = '"+textBox1.Text+"'");
            SqlCommand comselect2 = new SqlCommand(sslect2, conn);
            conn.Open();
            comselect2.ExecuteNonQuery();
            conn.Close();
            purchase_BillDataGridView.Rows.RemoveAt(row.Index);
            string sslect = string.Format("SELECT Material_Quantity FROM Material WHERE Material_ID = '"+Material_ID+"'");
            SqlCommand comselect = new SqlCommand(sslect, conn);
            SqlDataReader reader;
            conn.Open();
            reader = comselect.ExecuteReader();
            int Q1 = 0;
            if (reader.Read())
                Q1 = (int)reader.GetValue(0);
            reader.Close();
            conn.Close();
            Q1 = Q1 - Q;
            string sslect1 = string.Format("UPDATE Material SET Material_Quantity = '" + Q1 + "' WHERE Material_ID = '" + Material_ID + "'");
            SqlCommand comselect3 = new SqlCommand(sslect1, conn);
            conn.Open();
            comselect3.ExecuteNonQuery();
            conn.Close();
        }
        private void textBox11_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                int t = Convert.ToInt32(textBox9.Text);
                int t2 = Convert.ToInt32(textBox11.Text);
                t = t - t2;
                textBox7.Text = ""+t;
                textBox12.Text = "" + t;
            }
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            Purchase_Data i = new Purchase_Data();
            i.Show();
        }
    }
}