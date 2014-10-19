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
        int selectedPLU;
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
            // Si no es admin bloquear controles
            if (!miamiPOS.Properties.Settings.Default.admin)
            {
                SetReadonlyControls(groupBoxEditor.Controls);
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           DataGridView dgv = sender as DataGridView;
           labelSelected.Text = dgv.Rows[e.RowIndex].Cells[1].Value.ToString();
            this.selectedPLU =  (int)dgv.Rows[e.RowIndex].Cells[0].Value;
        }

        private void SetReadonlyControls(Control.ControlCollection controlCollection)
        {
            if (controlCollection == null)
            {
                return;
            }

            foreach (TextBoxBase c in controlCollection.OfType<TextBoxBase>())
            {
                c.ReadOnly = true;
            }
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

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                int cantidad = Convert.ToInt32(this.textBoxEdit.Text);
                string query;
                DataGridViewRow rowEditada = dataGridView1.SelectedRows[0];

                if (radioButtonAdd.Checked)
                {
                    //suma a inventario
                    query=String.Format("UPDATE inventario SET stock=stock + {0} where plu={1}", cantidad, selectedPLU);
                    rowEditada.Cells["stock"].Value = cantidad + (int)rowEditada.Cells["stock"].Value;
                }
                else
                {
                    //cambiar total inventario
                    query = String.Format("UPDATE inventario SET stock={0} where plu={1}", cantidad, selectedPLU);
                    rowEditada.Cells["stock"].Value = cantidad;

                }
                Psql.execInsert(query);
                MessageBox.Show("EXITO");

            }
            catch(Exception E)
            {
                MessageBox.Show("No permitido"+E.Message);
            }
        }
    }
}
