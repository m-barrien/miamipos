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
        Object esDebito = null;
        public iMonederoForm()
        {
            InitializeComponent();
        }
        public iMonederoForm(String total)
        {
            InitializeComponent();
            buttonDebit.Visible = true;
            labelTotal.Text = total;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBoxCash.Focus();
            SendKeys.Send("{1}");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBoxCash.Focus();
            SendKeys.Send("{2}");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBoxCash.Focus();
            SendKeys.Send("{3}");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBoxCash.Focus();
            SendKeys.Send("{4}");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBoxCash.Focus();
            SendKeys.Send("{5}");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            textBoxCash.Focus();
            SendKeys.Send("{6}");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            textBoxCash.Focus();
            SendKeys.Send("{7}");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            textBoxCash.Focus();
            SendKeys.Send("{8}");
        }

        private void button9_Click(object sender, EventArgs e)
        {
            textBoxCash.Focus();
            SendKeys.Send("{9}");
        }

        private void button10_Click(object sender, EventArgs e)
        {
            textBoxCash.Focus();
            SendKeys.Send("{0}");
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            textBoxCash.Focus();
            textBoxCash.Clear();
        }


        private void actualizarMonedero(object sender, EventArgs e)
        {
            Button tecla = sender as Button;
            textBoxCash.Text += tecla.Text;

            if (textBoxCash.Text.Length > 0)
            {
                try
                {
                    Int32 cash = Convert.ToInt32(textBoxCash.Text);
                    Int32 deuda = Convert.ToInt32(labelTotal.Text);
                    labelVuelto.Text = (cash - deuda).ToString();
                }
                catch
                {
                    MessageBox.Show("Digitos INVALIDOS");
                    textBoxCash.Clear();
                }
            }
        }

        private void buttonDebit_Click(object sender, EventArgs e)
        {
            esDebito = true;
            this.Close();
        }

        private void buttonCash_Click(object sender, EventArgs e)
        {
            esDebito = false;
            this.Close();
        }
        public bool payment()
        {
            
            while( esDebito == null);
            return (bool)esDebito ;
        }
    }
}
