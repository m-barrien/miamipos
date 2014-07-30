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
    public partial class iAnticipo : Form
    {
        public iAnticipo()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void iAnticipo_Load(object sender, EventArgs e)
        {
            miamiDB.getCajeros(ref comboBox1);
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r' && textBox1.Text.Length != 0)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r' && textBox2.Text.Length != 0)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var total=Convert.ToInt32(textBox1.Text);
                Int32 idCajero = Convert.ToInt32((comboBox1.SelectedItem as ComboboxItem).Value);
                var existentes = Psql.execScalar("select count(*) from cajero where id=" + idCajero + " and password='" + textBox2.Text + "'");
                if (existentes == "1")
                {
                    Int32 rowsAffected = Psql.execInsert("insert into anticipo(id_deudor,total,id_turno) VALUES (" + idCajero + "," + total + "," + miamiDB.id_turno + ")");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Contraseña Erronea");
                    textBox1.Clear();
                    textBox2.Clear();
                    textBox1.Focus();
                }
                
            }
            catch
            {
                MessageBox.Show("PROBLEMAS, no ingresada");
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
