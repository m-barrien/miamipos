using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

/*
// ejemplo para hacer evento cada tanto tiempo
public main_form()
{
    InitializeComponent();
    main_timer.Start();
    main_timer.Tick += new EventHandler(RunAlarm);
}

private void RunAlarm(object sender, EventArgs eArgs)
{
    Label text = this.set_at;
    if (text.Text == "ALARM!")
    {
        text.Text = "NO ALARM!";
    }
    else
    {
        text.Text = "ALARM!";
    }
}
// More code
*/


namespace miamiPOS
{
    public partial class mainForm : Form
    {
        Carrito Carro = new Carrito();
        public mainForm()
        {
            InitializeComponent();
            BackColor = miamiPOS.Properties.Settings.Default.backColor;
            tbCajero.Text = "Cajera "+miamiPOS.Properties.Settings.Default.nombreCajero;
            tbFecha.Text = "Entrada al sistema \r "+DateTime.Now.ToString() +"\r N° Turno "+miamiDB.id_turno+ "\r Caja Inicial: "+miamiPOS.Properties.Settings.Default.cajaInicial;

            dEBUGToolStripMenuItem.Visible = miamiPOS.Properties.Settings.Default.admin;

            textBox1.Focus();

            timerActualizar.Start();
        }
        private void mainForm_Load(object sender, EventArgs e)
        {
            miamiDB.initialize();
            try
            {
                miamiDB.linkDataGrid("producto",ref dgvProductos);

                dgvProductos.Columns["plu"].Width = 100;
                dgvProductos.Columns["precio"].Width = 100;
            }
            catch
            {
                MessageBox.Show("Error al mostrar datagridview de productos");
            }

            Carro.linkGrid(ref dgvCarrito);

            this.AcceptButton = btnAgregar;
            textBox1.Focus();
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
        private void button13_Click_1(object sender, EventArgs e)
        {
            textBox1.Focus();
            SendKeys.Send("{*}");
        }
        private void conectarToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dROPLOCALDBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            miamiDB.drop();
            dgvProductos.DataSource = null;

        }

        private void totalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            miamiDB.drop();
            miamiDB.loadProducts();
            miamiDB.linkDataGrid("producto",ref dgvProductos);
        }

        private void button13_Click(object sender, EventArgs e)
        {
            String[] tokens = {textBox1.Text};
            var cantidad = 1;
            Int64 plu;
            try
            {
                if (textBox1.Text.Contains("*"))
                {
                    tokens = textBox1.Text.Split('*');
                    cantidad = Convert.ToInt32(tokens[0]);
                    plu = Convert.ToInt64(tokens[1]);
                }
                else plu = Convert.ToInt64(tokens[0]);

                if (miamiDB.isPesable(plu))
                {
                    PesarForm pesa = new PesarForm();
                    pesa.ShowDialog();
                    while (pesa.IsAccessible) { System.Threading.Thread.Sleep(1000); }
                    Carro.addItem(plu, Pesable.Gramos);
                }
                else Carro.addItem(plu, cantidad);
                tbSubtotal.Text = Carro.subTotal().ToString();
            }
            catch
            {
                MessageBox.Show("Error Codigo invalido");
            }
            finally
            {
                textBox1.SelectAll();
            }
        }

        private void button15_Click(object sender, EventArgs e) //boton cancelar
        {
            Carro.cancel();
            tbSubtotal.Text = Carro.subTotal().ToString();
        }



        private void buttonTotal_Click(object sender, EventArgs e)
        {
            if (Carro.subTotal() <= 0)
            {
                MessageBox.Show("Carro Vacio");
            }
            else
            {
                try
                {
                    iMonederoForm monedero = new iMonederoForm(Carro.subTotal().ToString());
                    monedero.ShowDialog();
                    while (monedero.IsAccessible) { System.Threading.Thread.Sleep(1000); }
                    Carro.sendToDB();
                }
                catch (Exception E)
                {
                    MessageBox.Show(E.Message);
                }
            }
        }

        private void facturaTurnoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            iFactura Factura = new iFactura();
            Factura.ShowDialog();
        }

        private void buttonQuitar_Click(object sender, EventArgs e)
        {
            try
            {
                dgvCarrito.Rows.RemoveAt(this.dgvCarrito.SelectedRows[0].Index);
                tbSubtotal.Text = Carro.subTotal().ToString();

            }
            catch
            {
                Console.WriteLine("Nada que quitar de carrito");
            }
            finally
            {
                textBox1.SelectAll();
            }
        }

        private void revisarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            iInventario formInventario = new iInventario();
            formInventario.ShowDialog();
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            textBox1.Focus();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button14_Click(object sender, EventArgs e) //buscar
        {
            try
            {

                if (textBox1.Text.Length != 0) (dgvProductos.DataSource as DataTable).DefaultView.RowFilter = string.Format("[plu] = {0}", textBox1.Text);
                else (dgvProductos.DataSource as DataTable).DefaultView.RowFilter = null;
                textBox1.SelectAll();
            }
            catch
            {
                MessageBox.Show("DIGITOS ERRONEOS");
            }
        }

        private void incrementalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                 var actualizados =miamiDB.loadProductsIncremental();
                 if (actualizados > 0)
                 {
                     msgBox.Text = "Se actualizaron " + actualizados + " productos";
                 }
                 else msgBox.Text = "Nada que actualizar";
            }
            catch(Exception E)
            {
                Console.WriteLine(E.Message);
            }
        }

        private void mainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("DESEA TERMINAR SU TURNO?", "CONFIRMAR", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void timerActualizar_Tick(object sender, EventArgs e)
        {
            try
            {
                var actualizados = miamiDB.loadProductsIncremental();
                if (actualizados > 0)
                {
                    msgBox.Text = "Se actualizaron " + actualizados + " productos";
                }
                else msgBox.Text = "Nada que actualizar";
            }
            catch (Exception E)
            {
                Console.WriteLine(E.Message);
            }

        }

        private void anticipoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            iAnticipo formAnticipo = new iAnticipo();
            formAnticipo.ShowDialog();

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void dgvProductos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void colacionToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }







    }
}
