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
    public partial class Form4 : Form
    {
        int index;
        public Form4()
        {
            InitializeComponent();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            
        }

        private void radioButton1_selected(object sender, EventArgs e)
        {
            label7.Text = "Machine ID";
        }

        private void radioButton2_selected(object sender, EventArgs e)
        {
            label7.Text = "Company name";
        }
        private void button1_click(object sender, EventArgs e)
        {
            
            var continueLoop = true;
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

                        Inhouse house = new Inhouse(textBox2.Text, decimal.Parse(textBox3.Text), Int32.Parse(textBox4.Text),
                                                    Int32.Parse(textBox5.Text), Int32.Parse(textBox6.Text), Int32.Parse(textBox7.Text));
                        Main.inventory.updatePart(index, house);
                        MessageBox.Show("Part updated.");
                        continueLoop = false;
                        Hide();

                    }
                    if (radioButton2.Checked)
                    {
                        Outsourced outsourced = new Outsourced(textBox2.Text, decimal.Parse(textBox3.Text), Int32.Parse(textBox4.Text),
                                                    Int32.Parse(textBox5.Text), Int32.Parse(textBox6.Text), textBox7.Text);
                        Main.inventory.updatePart(index, outsourced);
                        MessageBox.Show("Part updated.");
                        continueLoop = false;
                        Hide();
                    }
                }
                catch(FormatException)
                {
                    MessageBox.Show($"\nMust be in proper format. \n\n(For in-house: text, decimal, integer, integer, integer, integer.)" +
                                                                $"\n(For outsourced: text, decimal, integer, integer, integer, text.)");
                    continueLoop = false;
                }
                catch (IndexOutOfRangeException indexException)
                {
                    MessageBox.Show($"\n{indexException.Message}");
                    continueLoop = false;

                }

            } while (continueLoop);


        }

        private void button2_click(object sender, EventArgs e)
        {
            Hide();
        }
        public void populate(string[] args, bool inhouse, int i)
        {
            textBox1.Text = args[0];
            textBox2.Text = args[1];
            textBox3.Text = args[2];
            textBox4.Text = args[3];
            textBox5.Text = args[4];
            textBox6.Text = args[5];

            if (inhouse)
            {
                radioButton1.Checked = (true);
                if (Main.inventory.AllParts[i] is Inhouse)
                    textBox7.Text = (Main.inventory.AllParts[i].MachineID).ToString();
            }
            else
            {
                radioButton2.Checked = (true);
                if (Main.inventory.AllParts[i] is Outsourced)
                    textBox7.Text = (Main.inventory.AllParts[i].CompanyName).ToString();


            }
            index = i;
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
