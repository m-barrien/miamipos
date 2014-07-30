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
    public partial class manager : Form
    {
        DataTable tablaProductos = null;
        public manager()
        {
            InitializeComponent();
            dataGridViewProductos.DataSource = tablaProductos;
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void manager_Load(object sender, EventArgs e)
        {
            miamiDB.getCategorias(ref comboBoxCategorias);
            miamiDB.getCategorias(ref cbCategoria);

            checkBoxEditMode.Enabled = false; //Deshabilitar el checkmox de modo editar
        }

        private void comboBoxCategorias_SelectedIndexChanged(object sender, EventArgs e)
        {
            Int32 idCategoria = Convert.ToInt32((comboBoxCategorias.SelectedItem as ComboboxItem).Value);
            Psql.execQuery("select  plu, barcode, nombre, precio, pesable, id_categoria from producto where id_categoria=" + idCategoria, ref tablaProductos);
            dataGridViewProductos.DataSource = tablaProductos;

            dataGridViewProductos.Columns["id_categoria"].Visible = false;

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            try
            {
                int selectedrowindex = dataGridViewProductos.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = dataGridViewProductos.Rows[selectedrowindex]; 

                var plu = Convert.ToInt32( selectedRow.Cells["plu"].Value ); //plu de item seleccionado
                Psql.execScalar("delete from producto where plu=" + plu);
                dataGridViewProductos.Rows.RemoveAt( selectedrowindex );
            }
            catch
            {
                Console.WriteLine("Error al eliminar");
            }
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            try
            {
                int selectedrowindex = dataGridViewProductos.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = dataGridViewProductos.Rows[selectedrowindex];

                var plu = selectedRow.Cells["plu"].Value; //plu de item seleccionado
                var barcode = selectedRow.Cells["barcode"].Value;
                var nombre = selectedRow.Cells["nombre"].Value;
                var precio = selectedRow.Cells["precio"].Value;
                var pesable = Convert.ToBoolean (selectedRow.Cells["pesable"].Value );
                var id_categoria = selectedRow.Cells["id_categoria"].Value;

                tbPLU.Text = plu.ToString();
                tbBarcode.Text = barcode.ToString();
                tbName.Text = nombre.ToString();
                tbPrice.Text = precio.ToString();
                checkBoxPesable.Checked = pesable;

                // Siguiente foreach para dejar la categoria del item seleccionada en modo editar
                foreach(ComboboxItem cbItem in cbCategoria.Items){
                    if(cbItem.Value == (int)id_categoria)
                    {
                        cbCategoria.SelectedItem = cbItem;
                    }
                }

                checkBoxEditMode.Checked = true; //activar modo editar
              
            }
            catch
            {
                Console.WriteLine("Nada que Editar");
            }
        }

        private void buttonNewItem_Click(object sender, EventArgs e)
        {
            foreach (Control x in this.groupBox1.Controls)
            {
                if (x is TextBox)
                {
                    ((TextBox)x).Clear();
                }
            }
            checkBoxEditMode.Checked = false;
            cbCategoria.SelectedItem = null;
            tbPLU.Focus();
        }

        private void buttonSaveItem_Click(object sender, EventArgs e)
        {
            var plu = tbPLU.Text;
            var barcode = tbBarcode.Text;

        }

        private void checkBoxEditMode_CheckedChanged(object sender, EventArgs e)
        {
            tbPLU.ReadOnly = checkBoxEditMode.Checked; //si esta en modo editar el textbox del plu se bloquea
        }
    }
}
