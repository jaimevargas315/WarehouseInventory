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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        public int index;

        private void Form3_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = Main.inventory.AllParts;
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.Columns.Remove("MachineID");
            dataGridView1.Columns.Remove("CompanyName");

            try
            {
                if (Main.inventory.Products.Count > 0)
                    dataGridView2.DataSource = Main.inventory.Products[index].AssociatedParts;
            }
            catch (ArgumentOutOfRangeException)
            {  };

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var continueLoop = true;
            if (textBox2.Text.Length > 0 && textBox3.Text.Length > 0 && textBox4.Text.Length > 0 &&
               textBox5.Text.Length > 0 && textBox6.Text.Length > 0)
            {
                do
                {
                    try
                    {
                        if (Int32.Parse(textBox5.Text.ToString()) > Int32.Parse(textBox6.Text.ToString()))
                        {
                            throw new System.IndexOutOfRangeException("Min must be less than Max.");
                        }
                        if (Int32.Parse(textBox4.Text.ToString()) < Int32.Parse(textBox5.Text.ToString()) ||
                            Int32.Parse(textBox4.Text.ToString()) > Int32.Parse(textBox6.Text.ToString()))
                        {
                            throw new System.IndexOutOfRangeException("Inventory level must be between Min and Max.");
                        }
                        Product product = new Product(textBox2.Text, Decimal.Parse(textBox3.Text),
                                          Int32.Parse(textBox4.Text), Int32.Parse(textBox5.Text), Int32.Parse(textBox6.Text));

                        if (dataGridView2.RowCount > 0)
                        {
                            for (int i = 0; i < Main.inventory.Products[dataGridView2.SelectedRows[0].Index].AssociatedParts.Count; i++)
                            {
                                product.addAssociatedPart(Main.inventory.Products[dataGridView2.SelectedRows[0].Index].AssociatedParts[i]);
                            }
                        }
                        Main.inventory.updateProduct(index, ref product);
                        Hide();
                        continueLoop = false;
                    }
                    catch (FormatException)
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

        private void button5_Click(object sender, EventArgs e)
        {
            
            if (textBox7.Text.Length > 0)
            {           
                    try
                    {
                    
                        Part result = Main.inventory.lookupPart(Int32.Parse(textBox7.Text));
                        for (int i = 0; i < dataGridView1.RowCount; i++)
                        {
                            if (dataGridView1.Rows[i].Cells[0].Value == null)
                                continue;
                            if (Int32.Parse(dataGridView1.Rows[i].Cells[0].Value.ToString()) == result.PartID)
                                dataGridView1.Rows[i].Selected = true;
                        }
                        
                    }
                    catch (FormatException formatException)
                    {
                        MessageBox.Show($"\n{formatException.Message} \nMust search by PartID (integer)");
                    }
                
                    catch (NullReferenceException)
                    {
                        MessageBox.Show("Part not found");
                    } 
                    
                
            }
            

        }

        public void populate(string[] args, int i)
        {
            textBox1.Text = args[0];
            textBox2.Text = args[1];
            textBox3.Text = args[2];
            textBox4.Text = args[3];
            textBox5.Text = args[4];
            textBox6.Text = args[5];
                        
            index = i;


        }


    }



}
