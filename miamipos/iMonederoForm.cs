using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace miamiPOS
{
    public partial class iMonederoForm : Form
    {
        public iMonederoForm()
        {
            InitializeComponent();
        }
        public iMonederoForm(String total)
        {
            InitializeComponent();
            button14.Visible = true;
            labelTotal.Text = total;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Focus();
            SendKeys.Send("{1}");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Focus();
            SendKeys.Send("{2}");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Focus();
            SendKeys.Send("{3}");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.Focus();
            SendKeys.Send("{4}");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox1.Focus();
            SendKeys.Send("{5}");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            textBox1.Focus();
            SendKeys.Send("{6}");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            textBox1.Focus();
            SendKeys.Send("{7}");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            textBox1.Focus();
            SendKeys.Send("{8}");
        }

        private void button9_Click(object sender, EventArgs e)
        {
            textBox1.Focus();
            SendKeys.Send("{9}");
        }

        private void button10_Click(object sender, EventArgs e)
        {
            textBox1.Focus();
            SendKeys.Send("{0}");
        }

        private void button12_Click(object sender, EventArgs e)
        {
            textBox1.Focus();
            textBox1.Clear();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            textBox1.Focus();
            SendKeys.Send("{ENTER}");
        }

        private void iMonederoForm_Load(object sender, EventArgs e)
        {
           
        }

        private void button13_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length > 0)
            {
                try
                {
                    Int32 pago = Convert.ToInt32(textBox1.Text);
                    Int32 total = Convert.ToInt32(labelTotal.Text);
                    labelVuelto.Text = (pago - total).ToString();
                }
                catch
                {
                    MessageBox.Show("Digitos INVALIDOS");
                }
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
