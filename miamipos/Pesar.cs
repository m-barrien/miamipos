﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace miamiPOS
{
    public partial class PesarForm : Form
    {
        public PesarForm()
        {
            InitializeComponent();
            this.AcceptButton = btnReg;
            Rectangle r = Screen.PrimaryScreen.WorkingArea;
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(Screen.PrimaryScreen.WorkingArea.Width - this.Width, Screen.PrimaryScreen.WorkingArea.Height - this.Height);
        }


        private void buttonReg_Click(object sender, EventArgs e)
        {
            try
            {
                float peso;
                peso = Convert.ToSingle(textBox1.Text);
                Pesable.Gramos = peso;
                this.Close();

            }
            catch
            {
                MessageBox.Show("DIGITOS ERRONEOS");
                textBox1.Clear();
            }
        }

        private void FinalizarForm_Load(object sender, EventArgs e)
        {

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

        private void button13_Click(object sender, EventArgs e)
        {
            Pesable.Gramos = 0;
            this.Close();
        }

    }
}
