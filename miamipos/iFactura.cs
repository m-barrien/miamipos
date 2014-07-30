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

    public partial class iFactura : Form
    {
        public iFactura()
        {
            InitializeComponent();
            lastFocus.tb = textBox1;
            
            
        }

        public static class lastFocus
        {
            public static TextBox tb=null;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            lastFocus.tb.Focus();
            SendKeys.Send("{1}");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            lastFocus.tb.Focus();
            SendKeys.Send("{2}");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            lastFocus.tb.Focus();
            SendKeys.Send("{3}");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            lastFocus.tb.Focus();
            SendKeys.Send("{4}");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            lastFocus.tb.Focus();
            SendKeys.Send("{5}");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            lastFocus.tb.Focus();
            SendKeys.Send("{6}");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            lastFocus.tb.Focus();
            SendKeys.Send("{7}");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            lastFocus.tb.Focus();
            SendKeys.Send("{8}");
        }

        private void button9_Click(object sender, EventArgs e)
        {
            lastFocus.tb.Focus();
            SendKeys.Send("{9}");
        }

        private void button10_Click(object sender, EventArgs e)
        {
            lastFocus.tb.Focus();
            SendKeys.Send("{0}");
        }

        private void button12_Click(object sender, EventArgs e)
        {
            lastFocus.tb.Focus();
            textBox1.Clear();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (lastFocus.tb == textBox1) lastFocus.tb = textBox2;
            else lastFocus.tb = textBox1;
            lastFocus.tb.Focus();
            
        }

        private void iFactura_Load(object sender, EventArgs e)
        {
            miamiDB.getEmpresas(ref comboBox1);
        }

        private void buttonAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                Int32 indiceEmpresa = Convert.ToInt32((comboBox1.SelectedItem as ComboboxItem).Value);
                Int32 rowsAffected = Psql.execInsert("insert into factura(id_factura,codigo,id_empresa,id_turno,fecha,total) VALUES (DEFAULT," + textBox2.Text + "," + indiceEmpresa + "," + miamiDB.id_turno + ", now()," + textBox1.Text + " )");
                this.Close();
            }
            catch
            {
                MessageBox.Show("PROBLEMAS, no ingresada");
            }
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            lastFocus.tb = textBox2;
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            lastFocus.tb = textBox1;
        }
    }
}
