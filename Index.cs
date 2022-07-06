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
using WindowsFormsApp1.MAIN_FUNCTION;
using WindowsFormsApp1.SHOW_DATA;

namespace WindowsFormsApp1
{
    public partial class Index : Form
    {
        public Index()
        {
            InitializeComponent();
        }

        private void Index_Load(object sender, EventArgs e)
        {
            DataTable data1 = new DataTable("Material");
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = "Data Source=DESKTOP-9E272F5\\MSSQLSERVER01;Initial Catalog=Factory Database;Integrated Security=True";
            string sslect = string.Format("SELECT * FROM Material Where Material_Quantity < 100");
            conn.Open();
            SqlDataAdapter data2 = new SqlDataAdapter(sslect, conn);
            data2.Fill(data1);
            dataGridView1.DataSource = data1;
            conn.Close();
            DataTable data4 = new DataTable("Product");
            string sslect1 = string.Format("SELECT * FROM Product Where Product_Quantity < 100");
            conn.Open();
            SqlDataAdapter data3 = new SqlDataAdapter(sslect1, conn);
            data3.Fill(data4);
            dataGridView2.DataSource = data4;
            conn.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            Index i = new Index();
            i.Show();
        }
        private void newCustomerToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            New_Customer i = new New_Customer();
            i.Show();
        }

        private void newSupplierToolStripMenuItem_Click(object sender, EventArgs e)
        {
            New_Supplier i = new New_Supplier();
            i.Show();
        }

        private void newEmployeeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            New_Employee i = new New_Employee();
            i.Show();
        }

        private void customerDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Customer_Data i = new Customer_Data();
            i.Show();
        }

        private void supplierDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Supplier_Data i = new Supplier_Data();
            i.Show();
        }

        private void employeeDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Employee_Data i = new Employee_Data();
            i.Show();
        }

        private void newMaterialToolStripMenuItem_Click(object sender, EventArgs e)
        {
            New_Material i = new New_Material();
            i.Show();
        }

        private void materialDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Material_Data i = new Material_Data();
            i.Show();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult iExit1;
            iExit1 = MessageBox.Show("Confirm if you want to Exit", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (iExit1 == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void purchaseDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Purchase_Data i = new Purchase_Data();
            i.Show();
        }

        private void newPurchaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            New_Purchase i = new New_Purchase();
            i.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            New_Purchase i = new New_Purchase();
            i.Show();
        }

        private void newProductToolStripMenuItem_Click(object sender, EventArgs e)
        {
            New_Product i = new New_Product();
            i.Show();
        }

        private void productDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Product_Data i = new Product_Data();
            i.Show();
        }

        private void salesDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sale_Data i = new Sale_Data();
            i.Show();
        }

        private void newSaleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            New_Sale i = new New_Sale();
            i.Show();
        }

        private void discountFromTheStoreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Discount_Material i = new Discount_Material();
            i.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Pay_Product i = new Pay_Product();
            i.Show();
        }

        private void newPurchaseToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Pay_Product i = new Pay_Product();
            i.Show();
        }

        private void purchaseDataToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            PurchaseProduct_Data i = new PurchaseProduct_Data();
            i.Show();
        }

        private void newDiscountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Discount_Material i = new Discount_Material();
            i.Show();
        }

        private void discountDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Discount_Material_Data i = new Discount_Material_Data();
            i.Show();
        }

        private void newPeoductionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            New_Production i = new New_Production();
            i.Show();
        }

        private void productionDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Production_Data i = new Production_Data();
            i.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            New_Production i = new New_Production();
            i.Show();
        }
    }
}