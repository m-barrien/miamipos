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
    public partial class iColacion : Form
    {
        public iColacion()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var total = Convert.ToInt32(textBox1.Text);
                Int32 idCajero = Convert.ToInt32((comboBox1.SelectedItem as ComboboxItem).Value);
                var existentes = Psql.execScalar("select count(*) from cajero where id=" + idCajero + " and password='" + textBox2.Text + "'");
                if (existentes == "1")
                {
                    Int32 rowsAffected = Psql.execInsert("insert into colacion(id_cajero,total,fecha) VALUES (" + idCajero + "," + total + ",now() )");
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

        private void iColacion_Load(object sender, EventArgs e)
        {
            miamiDB.getCajeros(ref comboBox1);
        }

    }
}
