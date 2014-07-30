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
    public partial class iInventario : Form
    {
        public iInventario()
        {
            InitializeComponent();
            DataTable inventario = null;
            try
            {
                Psql.execQuery("select inventario.plu, producto.nombre ,inventario.stock from inventario,producto where producto.plu=inventario.plu order by inventario.stock ASC", ref inventario);
                dataGridView1.DataSource = inventario;

            }
            catch
            {
                MessageBox.Show("Sin conexion");
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void iInventario_Load(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.Columns[0].Width = 40;
                dataGridView1.Columns[2].Width = 100;
            }
            catch (Exception E)
            {
                Console.WriteLine(E.Message);
            }
        }
    }
}
