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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public int index;

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = Main.inventory.AllParts;
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.Columns.Remove("MachineID");
            dataGridView1.Columns.Remove("CompanyName");
            try
            {
                if (Main.inventory.Products.Count > 0)
                    dataGridView2.DataSource = Main.inventory.Products[dataGridView1.SelectedRows[0].Index].AssociatedParts;
            }
            catch (ArgumentOutOfRangeException)
            {  };

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var continueLoop = true;
            if (textBox3.Text.Length > 0 && textBox4.Text.Length > 0 && textBox5.Text.Length > 0 &&
               textBox6.Text.Length > 0 && textBox7.Text.Length > 0)
            {
                do
                {
                    try
                    {
                        if (Int32.Parse(textBox6.Text.ToString()) > Int32.Parse(textBox7.Text.ToString()))
                        {
                            throw new System.IndexOutOfRangeException("Min must be less than Max.");
                        }
                        if (Int32.Parse(textBox5.Text.ToString()) < Int32.Parse(textBox6.Text.ToString()) ||
                            Int32.Parse(textBox5.Text.ToString()) > Int32.Parse(textBox7.Text.ToString()))
                        {
                            throw new System.IndexOutOfRangeException("Inventory level must be between Min and Max.");
                        }

                        Product product = new Product(textBox3.Text, Decimal.Parse(textBox4.Text),
                                          Int32.Parse(textBox5.Text), Int32.Parse(textBox6.Text), Int32.Parse(textBox7.Text));

                        if (dataGridView2.RowCount > 0)
                        {
                            for (int i = 0; i < Main.inventory.Products[dataGridView2.SelectedRows[0].Index].AssociatedParts.Count; i++)
                            {
                                product.addAssociatedPart(Main.inventory.Products[dataGridView2.SelectedRows[0].Index].AssociatedParts[i]);
                            }
                        }
                        Main.inventory.addProduct(product);
                        Hide();
                        continueLoop = false;
                    }
                    catch(FormatException)
                    {
                        MessageBox.Show($"\nMust be in proper format. \n\n(text, decimal, integer, integer, integer.)");
                        continueLoop = false;
                    }
                    catch (IndexOutOfRangeException indexException)
                    {
                        MessageBox.Show($"\n{indexException.Message}");
                        continueLoop = false;
                    }
                    
                } while (continueLoop);
            }
        }
    
            
        private void button2_Click(object sender, EventArgs e)
        {
            Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {

            var continueLoop = true;
            do
            {
                try
                {
                    if (Main.inventory.Products[index].AssociatedParts.Contains(Main.inventory.AllParts[dataGridView1.CurrentRow.Index]))
                    { }
                    else
                    {
                        Main.inventory.Products[index].addAssociatedPart(Main.inventory.AllParts[dataGridView1.CurrentRow.Index]);
                    }
                    continueLoop = false;

                }
                catch (ArgumentOutOfRangeException)
                {
                    continueLoop = false;
                }

            } while (continueLoop);



            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView2.RowCount > 0)
            {

                        string message = "Delete the selected part?";
                        MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                        DialogResult result;
                        result = MessageBox.Show(message, "", buttons);

                        if (result == System.Windows.Forms.DialogResult.Yes)
                        {

                            Main.inventory.Products[index].removeAssociatedPart(dataGridView2.SelectedRows[0].Index);
                        }

            }

        }


        public void populate(int i)
        {
            index = i;
        }
    }
    
}
