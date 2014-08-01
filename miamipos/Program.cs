using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace miamiPOS
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Console.WriteLine(miamiDB.lastUpdate.ToString());

            try
            {
                Psql.testConnection();
            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message);
            }

            if (miamiPOS.Properties.Settings.Default.idTurno != 0)
            {
                if (MessageBox.Show("DESEA RESUMIR SU TURNO?", "CONFIRMAR", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    miamiDB.beginTurno(miamiPOS.Properties.Settings.Default.idTurno);
                }
                else
                {
                    Application.Run(new Login());
                    miamiDB.beginTurno();
                }
            }
            else
            {
                Application.Run(new Login());
                if(miamiPOS.Properties.Settings.Default.admin)
                {
                    Application.Run(new manager());
                    miamiPOS.Properties.Settings.Default.idCajero = 0;
                    miamiPOS.Properties.Settings.Default.Save();
                }
                    // Sino es administrador no se abre un turno
                else miamiDB.beginTurno();
            }
            if (miamiPOS.Properties.Settings.Default.idCajero != 0)
            {
                try { 
                    //POS
                    Application.Run(new mainForm());
                    //Panel para finalizar
                    Application.Run(new FinalizarForm());
                    //Finalizar turno
                    miamiDB.endTurno();
                }
                catch (Exception e) { MessageBox.Show(e.Message); }
                
            }
        }
    }
}
