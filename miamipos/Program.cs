using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Threading;
using System.Runtime.InteropServices;
 
namespace miamiPOS
{
    static class Program
    {
        private static string appGuid = "c0a76b5a-12ab-45c5-b9d9-d693faa6e7b9";
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Console.WriteLine(miamiDB.lastUpdate.ToString());

            using(Mutex mutex = new Mutex(false, "Global\\" + appGuid))
            {
                if(!mutex.WaitOne(0, false))
                {
                    MessageBox.Show("Se abrio el programa dos veces...\rCerrar esta ventana");
                    return;
                }

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
                        if (!miamiPOS.Properties.Settings.Default.modoSanguchero)
                        {
                            Application.Run(new FinalizarForm());
                        }
                        else
                        {
                            miamiDB.printSalesDay();
                        }
                        //Finalizar turno
                        miamiDB.endTurno();
                    }
                    catch (Exception e) { MessageBox.Show(e.Message); }
                
                }
        }
            miamiPOS.Properties.Settings.Default.admin = false;
            miamiPOS.Properties.Settings.Default.Save();
        }
    }
}
