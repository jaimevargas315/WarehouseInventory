using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
            
        }

        public static Inventory inventory = new Inventory();
        public Inventory getInventory()
        {
            return inventory;
        }

        private void Main_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = inventory.AllParts;
            dataGridView2.DataSource = inventory.Products;
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.Columns.Remove("MachineID");
            dataGridView1.Columns.Remove("CompanyName");

            Inhouse house = new Inhouse("Chain", (decimal)12.5, 12,3,25,154);
            inventory.addPart(house);

            Outsourced blerp  = new Outsourced("goob", (decimal)12.5, 12, 3, 25, "Dank");
            inventory.addPart(blerp);

            Outsourced outsourced = new Outsourced("Gear", (decimal)7.25, 23, 1, 75, "Aerotek");
            inventory.addPart(outsourced);

            Product product = new Product("Bike", (decimal)175.99, 45, 10, 100);
            inventory.addProduct(product);

        }       


        private void button1_Click(object sender, EventArgs e)
        {
            Form2 addPart = new Form2();
            addPart.Show();
        }

        
        private void button2_Click(object sender, EventArgs e)
        {

            string[] arg = new string[8];
            bool inhouse = false;
            if (dataGridView1.RowCount > 0)
            {
                foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                {
                    for (int i = 0; i < row.Cells.Count; i++)
                    {
                        if (row.Cells[i].Value != null)
                            arg[i] = row.Cells[i].Value.ToString();
                    }
                }
                if (inventory.AllParts[dataGridView1.SelectedRows[0].Index] is Inhouse)
                    inhouse = true;


                Form4 modPart = new Form4();
                modPart.populate(arg, inhouse, dataGridView1.SelectedRows[0].Index);
                modPart.Show();
            }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.RowCount > 0)
            {
                string message = "Delete the selected part?";
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                DialogResult result;
                result = MessageBox.Show(message, "", buttons);

                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                        dataGridView1.Rows.RemoveAt(row.Index);
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var continueLoop = true;
            if (textBox1.Text.Length > 0)
            {
                do
                {
                    try
                    {
                        Part result = inventory.lookupPart(Int32.Parse(textBox1.Text));
                        for (int i = 0; i < dataGridView1.RowCount; i++)
                        {
                            if (Int32.Parse(dataGridView1.Rows[i].Cells[0].Value.ToString()) == result.PartID)
                                dataGridView1.Rows[i].Selected = true;                         
                        }
                        continueLoop = false;
                    }
                    catch (FormatException formatException)
                    {
                        MessageBox.Show($"\n{formatException.Message} \nMust search by PartID (integer)");
                        continueLoop = false;
                    }
                    
                    catch (NullReferenceException)
                    {
                        MessageBox.Show("Part not found");
                        continueLoop = false;
                    }

                } while (continueLoop);
            }
           
        }
       
        private void button5_Click(object sender, EventArgs e)
        {
             Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            var continueLoop = true;
            if (textBox2.Text.Length > 0)
            {
                do
                {
                    try
                    {
                        Product result = inventory.lookupProduct(Int32.Parse(textBox2.Text));
                        for (int i = 0; i < dataGridView2.RowCount; i++)
                        {
                            if (Int32.Parse(dataGridView2.Rows[i].Cells[0].Value.ToString()) == result.ProductID)
                                dataGridView2.Rows[i].Selected = true;
                        }                

                        continueLoop = false;
                    }
                    catch (FormatException formatException)
                    {
                        MessageBox.Show($"\n{formatException.Message} \nMust search by ProductID (integer)");
                        continueLoop = false;
                    }
                    catch (NullReferenceException)
                    {
                        MessageBox.Show("Product not found");
                        continueLoop = false;
                    }

                } while (continueLoop);
            }

        }

        private void button7_Click(object sender, EventArgs e)
        {

            if (dataGridView2.RowCount > 0 && dataGridView2.SelectedRows.Count>0)
            {
                string message = "Delete the selected product?";
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                DialogResult result;
                result = MessageBox.Show(message, "", buttons);

                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    foreach (DataGridViewRow row in dataGridView2.SelectedRows)
                        dataGridView2.Rows.RemoveAt(row.Index);
                }
            }

        }

        private void button9_Click(object sender, EventArgs e)
        {
             Form1 addProd = new Form1();
            addProd.populate(dataGridView1.SelectedRows[0].Index);
             addProd.Show();
             
        }

        private void button8_Click(object sender, EventArgs e)
        {
            string[] arg = new string[7];
            if (dataGridView2.RowCount > 0)
            {
                foreach (DataGridViewRow row in dataGridView2.SelectedRows)
                {
                    for (int i = 0; i < row.Cells.Count; i++)
                    {
                        if (row.Cells[i].Value != null)
                            arg[i] = row.Cells[i].Value.ToString();
                    }
                }
                Form3 modProd = new Form3();
                try
                {
                    modProd.populate(arg, dataGridView1.SelectedRows[0].Index);
                }
                catch (ArgumentOutOfRangeException)
                {  }
                modProd.Show();

            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
