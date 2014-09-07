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
    public partial class FinalizarForm : Form
    {
        public FinalizarForm()
        {
            InitializeComponent();
            this.ControlBox = false;
            this.AcceptButton = btnReg;
        }

        private void buttonReg_Click(object sender, EventArgs e)
        {
            try
            {
                Int32 caja = Math.Abs(Convert.ToInt32(textBox1.Text));
                miamiPOS.Properties.Settings.Default.cajaInicial = caja;
                miamiPOS.Properties.Settings.Default.Save();

                Psql.execInsert("UPDATE turno SET caja_final=" + caja + " WHERE id=" + miamiDB.id_turno);

                this.FormClosing -= new System.Windows.Forms.FormClosingEventHandler(this.FinalizarForm_FormClosing);
                this.Close();

            }
            catch (Npgsql.NpgsqlException)
            {
                MessageBox.Show("GRAVE ERROR : No se puede guardar la caja final , llamar a encargado");
            }
            catch
            {
                MessageBox.Show("DIGITOS ERRONEOS");
            }
        }

        private void FinalizarForm_Load(object sender, EventArgs e)
        {

        }

        private void FinalizarForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
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

        private void FinalizarForm_Load_1(object sender, EventArgs e)
        {
            var url = miamiPOS.Properties.Settings.Default.serverRemote +"miamipos/resume.php?turno=" + miamiDB.id_turno;
            webBrowser1.Navigate(url);
        }

    }
}
