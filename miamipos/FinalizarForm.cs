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
        TextBox selectedTB;
        public FinalizarForm()
        {
            InitializeComponent();
            this.ControlBox = false;
            this.AcceptButton = btnReg;
            selectedTB = textBoxCI;
        }

        private void buttonReg_Click(object sender, EventArgs e)
        {
            try
            {
                Int32 caja = Math.Abs(Convert.ToInt32(textBoxCI.Text));
                Int32 retiros = Math.Abs(Convert.ToInt32(textBoxRetiros.Text));
                miamiPOS.Properties.Settings.Default.cajaInicial = caja;
                miamiPOS.Properties.Settings.Default.Save();

                string query = String.Format("UPDATE turno SET caja_final={0},retiro={1} WHERE id={2}", caja, retiros, miamiDB.id_turno);
                Psql.execInsert(query);

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
            Button tecla = sender as Button;
            selectedTB.Text += tecla.Text;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            selectedTB.Clear();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            textBoxCI.Focus();
            SendKeys.Send("{ENTER}");
        }

        private void FinalizarForm_Load_1(object sender, EventArgs e)
        {
            ResumenTurno turno = new ResumenTurno(miamiDB.id_turno);
            textBoxTurno.Text = miamiDB.id_turno.ToString();
            textBoxVentas.Text = turno.ventas.ToString();
            textBoxFacturas.Text = turno.facturas.ToString();
            textBoxAncticipos.Text = turno.anticipos.ToString();
            textBoxColaciones.Text = turno.colaciones.ToString();
            textBoxCinicial.Text = turno.cajaInicial.ToString();
            textBoxDebito.Text = turno.debito.ToString();

        }

        private void switchContext(object sender, EventArgs e)
        {
            TextBox focusedTextBox = sender as TextBox;
            selectedTB = focusedTextBox;
        }

    }
}
