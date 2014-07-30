using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Deployment.Application.Manifest;

namespace miamiPOS
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
            BackColor=miamiPOS.Properties.Settings.Default.backColor;
            //this.AcceptButton = button1;

            labelNew.Text = "NUEVO \r -Se muestra la Z al final del turno \r -Se puede resumir el turno en caso de reinicio";
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            
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
                MessageBox.Show("BIENVENIDA " + results.Rows[0][1]);

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

    }
}
