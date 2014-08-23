using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Deployment.Application.Manifest;
using System.IO.Ports;

namespace miamiPOS
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
            BackColor=miamiPOS.Properties.Settings.Default.backColor;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            textBoxServer.Text = miamiPOS.Properties.Settings.Default.serverIP;
            textBoxSucursal.Text = miamiPOS.Properties.Settings.Default.id_sucursal;

            // Seccion para obtener los puertos COM para la impresora
            string[] nameArray = null;
            nameArray = SerialPort.GetPortNames();
            Array.Sort(nameArray);
            comboBoxPort.DataSource = nameArray;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var user = maskedTextBox1.Text;
            var pass = maskedTextBox2.Text;
            DataTable results=null;
            String query = "select id,nombre,admin from cajero where id="+user+"and password='"+pass+"'";
            
            try
            {
                Psql.execQuery(query, ref results);
                MessageBox.Show("BIENVENID@ " + results.Rows[0][1]);

                miamiPOS.Properties.Settings.Default.idCajero = (Int32)results.Rows[0][0];
                miamiPOS.Properties.Settings.Default.nombreCajero = (String)results.Rows[0][1];
                miamiPOS.Properties.Settings.Default.admin = (bool)results.Rows[0][2];
                miamiPOS.Properties.Settings.Default.Save();

                this.Close();

            }
            catch (Npgsql.NpgsqlException E)
            {
                MessageBox.Show(E.Message);
            }
            catch
            {
                MessageBox.Show("NO EXISTE");
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            maskedTextBox1.Clear();
            maskedTextBox2.Clear();
            maskedTextBox1.Focus();
        }

        private void maskedTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r' && maskedTextBox1.Text.Length != 0)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void maskedTextBox2_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void maskedTextBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r' && maskedTextBox2.Text.Length != 0)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void textBoxServer_Leave(object sender, EventArgs e)
        {
            Psql.updateHost(textBoxServer.Text);
            miamiPOS.Properties.Settings.Default.serverIP = textBoxServer.Text;
            miamiPOS.Properties.Settings.Default.Save();
        }

        private void comboBoxPort_SelectedIndexChanged(object sender, EventArgs e)
        {
            miamiPOS.Properties.Settings.Default.printerPortName = comboBoxPort.SelectedValue.ToString();
            miamiPOS.Properties.Settings.Default.Save();
        }

        private void textBoxSucursal_Leave(object sender, EventArgs e)
        {
            miamiPOS.Properties.Settings.Default.id_sucursal = textBoxSucursal.Text;
            miamiPOS.Properties.Settings.Default.Save();
        }

    }
}
