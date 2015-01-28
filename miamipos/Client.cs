using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.IO.Ports;
using SerialCOM;
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
        pleaseWait loadingForm = new pleaseWait();

        public mainForm()
        {
            InitializeComponent();
            Logger.bind(ref this.msgBox);
            BackColor = miamiPOS.Properties.Settings.Default.backColor;
            tbCajero.Text = "Cajera "+miamiPOS.Properties.Settings.Default.nombreCajero;
            tbFecha.Text = "Entrada al sistema \r "+DateTime.Now.ToString() +"\r N° Turno "+miamiDB.id_turno+ "\r Caja Inicial: "+miamiPOS.Properties.Settings.Default.cajaInicial;

            dEBUGToolStripMenuItem.Visible = miamiPOS.Properties.Settings.Default.admin;

            textBox1.Focus();

            timerActualizar.Start();
            //Cargar categorias para la busqueda
            miamiDB.getCategorias(ref comboBoxCategorias);
        }
        private void mainForm_Load(object sender, EventArgs e)
        {

            //Activar modo sanguchero si es que estaba guardado
            activePrinter.Checked = miamiPOS.Properties.Settings.Default.modoSanguchero;
            toolStripMenuItem1_Click(sender, e);

            //revisar base de datos e iniciar
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
            Button tecla = sender as Button;
            if (textBox1.SelectedText.Length > 0)
            {
                textBox1.Clear();
            }
            textBox1.Text += tecla.Text;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
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
            dgvProductos.DataSource = null;
            miamiDB.linkDataGrid("producto",ref dgvProductos);
        }

        private void button13_Click(object sender, EventArgs e)
        {

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
                bool esDebito = false;
                try
                {
                    //Modo sanguchero
                    if (activePrinter.Checked)
                    {
                        Carro.printReceipt(miamiPOS.Properties.Settings.Default.printerPortName);
                    }
                    else
                    {
                        
                        iMonederoForm monedero = new iMonederoForm(Carro.subTotal().ToString());
                        monedero.ShowDialog();
                        esDebito = monedero.payment();
                    }
                    int idVenta = Carro.sendToDB(esDebito);
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

        // funcion que ejecuta la actualizacion de fondo
        private void incrementalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Configure a BackgroundWorker to perform your long running operation.
            BackgroundWorker bg = new BackgroundWorker();
            bg.DoWork += new DoWorkEventHandler(actualizarIncrementalThread);
            bg.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bg_RunWorkerCompleted);

            // Start the worker.
            bg.RunWorkerAsync();

            // Display the loading form.
            
            loadingForm.ShowDialog();

        }
        private void bg_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // Retrieve the result pass from bg_DoWork() if any.
            // Note, you may need to cast it to the desired data type.
            object result = e.Result;

            // Close the loading form.
            loadingForm.Close();

            // Update any other UI controls that may need to be updated.
        }

        //Funcion que ejecuta la actualizacion en si
        private void actualizarIncrementalThread(object sender, DoWorkEventArgs e)
        {
            try
            {
                e.Result = miamiDB.loadProductsIncremental();
            }
            catch (Exception E)
            {
                Console.WriteLine(E.Message);
                e.Result = 0;
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
            iColacion formColacion = new iColacion();
            formColacion.ShowDialog();
        }

        // click en activar modo sanguchero
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (!activePrinter.Checked)
            {
                this.BackColor = miamiPOS.Properties.Settings.Default.backColor;
                this.Text = "PAN Y PASTELES MIAMI";
            }
            else
            {
                this.BackColor = miamiPOS.Properties.Settings.Default.sangucheColor;
                this.Text = "COMIDAS Y SANDWICHES MIAMI";
            }
        }

        private void comboBoxPrinterPorts_Click(object sender, EventArgs e)
        {
            //quizas guardad puerto en settings
            
        }

        private void impresoraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void comboBoxCategorias_SelectedIndexChanged(object sender, EventArgs e)
        {
            Int32 idCategoria = Convert.ToInt32(((sender as ComboBox).SelectedItem as ComboboxItem).Value);
            (dgvProductos.DataSource as DataTable).DefaultView.RowFilter = string.Format("[id_categoria] = {0}", idCategoria);
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            String[] tokens = { textBox1.Text };
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
                textBox1.Focus();
                textBox1.SelectAll();
            }
        }







    }
}
