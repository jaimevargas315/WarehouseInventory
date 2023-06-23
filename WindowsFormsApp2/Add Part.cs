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
    public partial class Form2 : Form
    {
      
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var continueLoop = true;
            if (textBox2.Text.Length >0 && textBox3.Text.Length >0 && textBox4.Text.Length > 0 &&
                textBox5.Text.Length > 0 && textBox6.Text.Length > 0 && textBox7.Text.Length > 0)
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
                        if (radioButton1.Checked)
                        {

                            Inhouse house = new Inhouse(textBox2.Text, Decimal.Parse(textBox3.Text), Int32.Parse(textBox4.Text),
                                                        Int32.Parse(textBox5.Text), Int32.Parse(textBox6.Text), Int32.Parse(textBox7.Text));
                            Main.inventory.addPart(house);
                            MessageBox.Show("Part added.");
                            continueLoop = false;
                            Hide();

                        }
                        if (radioButton2.Checked)
                        {
                            Outsourced outsourced = new Outsourced(textBox2.Text, Decimal.Parse(textBox3.Text), Int32.Parse(textBox4.Text),
                                                        Int32.Parse(textBox5.Text), Int32.Parse(textBox6.Text), textBox7.Text);
                            Main.inventory.addPart(outsourced);
                            MessageBox.Show("Part added.");
                            continueLoop = false;
                            Hide();
                        }
                    }

                    catch (FormatException)
                    {
                        MessageBox.Show($"\nMust be in proper format. \n\n(For in-house: text, decimal, integer, integer, integer, integer.)" +
                                                                    $"\n(For outsourced: text, decimal, integer, integer, integer, text.)");
                        continueLoop = false;
                    }
                    catch(IndexOutOfRangeException indexException)
                    {
                        MessageBox.Show($"\n{indexException.Message}");
                                                continueLoop = false;

                    }

                } while (continueLoop);
            }
              
           
        }
        private void radioButton1_selected(object sender, EventArgs e)
        {
            label7.Text = "Machine ID";
        }

        private void radioButton2_selected(object sender, EventArgs e)
        {
            label7.Text = "Company name";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Hide();
        }
    }
}

