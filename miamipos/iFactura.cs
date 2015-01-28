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
            Button tecla = sender as Button;
            if (lastFocus.tb.SelectedText.Length > 0)
            {
                lastFocus.tb.Clear();
            }
            lastFocus.tb.Text += tecla.Text;
        }

       
        private void button12_Click(object sender, EventArgs e)
        {
            lastFocus.tb.Clear();
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
