﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.ComponentModel;
using System.Windows.Forms;
using Npgsql;
using SerialCOM;


namespace miamiPOS
{
    public static class Psql
    {
        /*connection string
         * using(NpgsqlConnection conn = new NpgsqlConnection("Server=127.0.0.1;Port=5432;User Id=joe;Password=secret;Database=joedata;"))
         * 
         */
        private static String serverIP = miamiPOS.Properties.Settings.Default.serverIP;
        private static short port = miamiPOS.Properties.Settings.Default.port;
        private static String dbUser = miamiPOS.Properties.Settings.Default.dbUser;
        private static String dbPass = miamiPOS.Properties.Settings.Default.dbPass;
        private static String dbName = miamiPOS.Properties.Settings.Default.dbName;

        private static String connString = "Server="+serverIP+";"+
                                           "Port="+port+";"+
                                           "User Id="+dbUser+";"+
                                           "Password="+dbPass+";"+
                                           "Database="+dbName+";"
                                           ;
        private static NpgsqlConnection conn = new NpgsqlConnection(connString);
        public static void updateHost(string serverAddress)
        {
            connString = "Server="+serverAddress+";"+
                        "Port="+port+";"+
                        "User Id="+dbUser+";"+
                        "Password="+dbPass+";"+
                        "Database="+dbName+";"
                        ;
            conn = new NpgsqlConnection(connString);
        }
        public static void testConnection()
        {
            try
            {
                conn.Open();
                conn.Close();
            }
            catch
            {
                throw new Exception("Sin conexion");
            }
        }

        public static Int32 execInsert(String query)
        {
            conn.Open();
            NpgsqlCommand command = new NpgsqlCommand(query, conn);
            Int32 rowsAffected = 0;
            try
            {
                rowsAffected = command.ExecuteNonQuery();

                Logger.log(String.Format("Se agregaron {0} lineas en la BD", rowsAffected),Logger.DEBUG);
                
            }
            catch (Npgsql.NpgsqlException e)
            {
                throw new Exception("ERROR SQL \r" + query + "\r" + e.Message);
            }
            finally
            {
                conn.Close();
                
            }
            return rowsAffected;
        
        }
        public static String execScalar(String query)
        {
            String result = "";
            try
            {
                conn.Open();
                NpgsqlCommand command = new NpgsqlCommand(query, conn);

                result = Convert.ToString( command.ExecuteScalar());
                Logger.log("Executed scalar query with result:" + result,Logger.DEBUG);
            }
            catch (Npgsql.NpgsqlException e)
            {
                throw e;
            }

            finally
            {
                conn.Close();
            }
            return result;
        }
        public static void execQuery(String query,ref DataSet myDS,String tableName)
        {
            try
            {
                conn.Open();
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(query, conn);

                da.Fill(myDS, tableName);

                
            }
            catch (Npgsql.NpgsqlException e)
            {
                throw e;
            }
            finally
            {
                conn.Close();
            }

        }
        public static void execQuery(String query,ref DataTable myTable)
        {
            try
            {
                conn.Open();
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(query, conn);
                // if (myTable == null) { myTable = new DataTable(); }
                myTable = new DataTable();
                da.Fill(myTable);
            }
            catch (Npgsql.NpgsqlException e)
            {
                throw new Exception("ERROR SQL \r" + e.Message);
            }
            finally
            {
                conn.Close();
            }

        }
      




    }

    public static class miamiDB
    {
        private static DataSet dt = miamiPOS.Properties.Settings.Default.masterSet;
        public static DateTime lastUpdate = miamiPOS.Properties.Settings.Default.lastUpdate;
        public static bool isInitialized=false;
        public static Int32 id_turno = 0 ;
        private static Int32 cajaInicial = miamiPOS.Properties.Settings.Default.cajaInicial;

        public static void initialize()  
        {
            if (dt !=null)
            {
                if (dt.Tables["producto"] != null)
                {
                    isInitialized = true;
                }
            }
            if (!isInitialized)
            {
                miamiPOS.Properties.Settings.Default.masterSet = new DataSet("miamiDB");
                loadProducts();
                miamiPOS.Properties.Settings.Default.Save();
                dt = miamiPOS.Properties.Settings.Default.masterSet;
                isInitialized = true;
            }
            else Logger.log("Using Backup database",Logger.DEBUG);
        }
        /*
         * linkDataGrid
         * @params tableName ,datagridView
         * vincula un dataSet a un DataGridView
         */
        public static void linkDataGrid(String tableName, ref DataGridView dgView){
            try
            {
                dgView.DataSource = dt.Tables[tableName];
                if (tableName == "producto") //filtrar cosas inncesesarias en las referencias a productos
                {
                    dgView.Columns["pesable"].Visible = false;
                    dgView.Columns["barcode"].Visible = false;
                    dgView.Columns["id_categoria"].Visible = false;

                }
            }
            catch (NullReferenceException)
            {
                Logger.log("Se desea esconder columna inexistente",Logger.ERROR);
            }
            catch
            {
                Logger.log("DataGrid not linked to table "+ tableName, Logger.ERROR);
                throw new Exception("Datagrid no vinculada para " + tableName);
            }
        }
        private static void saveTime()
        {
            lastUpdate = Convert.ToDateTime(Psql.execScalar("select now()"));
            miamiPOS.Properties.Settings.Default.lastUpdate = lastUpdate;
            miamiPOS.Properties.Settings.Default.Save();
        }
        public static void loadProducts()
        {
            DataTable producto ;
            if (dt != null && dt.Tables["producto"] == null)
            {
                producto = dt.Tables.Add("producto");
            }
            else if(dt != null) { producto = dt.Tables["producto"]; }
            try
            {
                Psql.execQuery("select plu,nombre,precio,pesable,barcode,id_categoria FROM producto", ref dt, "producto");
                miamiPOS.Properties.Settings.Default.masterSet = dt;
                miamiPOS.Properties.Settings.Default.Save();
                saveTime();
                isInitialized = true;
                
            }
            catch (Npgsql.NpgsqlException e)
            {
                Logger.log("Error SQL:"+ e.Message ,Logger.ERROR);
            }
            catch(Exception e)
            {
                Logger.log("Error Sistema:" + e.Message, Logger.ERROR);
            }

        }
        public static void getEmpresas(ref ComboBox listadoEmpresas)
        {
            DataTable empresas=null;
            
            try
            {
                Psql.execQuery("select id,nombre FROM empresa order by nombre ASC", ref empresas);
                foreach (DataRow row in empresas.Rows)
                {
                    ComboboxItem item = new ComboboxItem();
                    item.Text = row[1].ToString();
                    item.Value = Convert.ToInt32(row[0]);
                    listadoEmpresas.Items.Add(item);
                }
            }
            catch (Npgsql.NpgsqlException e)
            {
                Logger.log("Error SQL:" + e.Message, Logger.ERROR);
            }
            catch (Exception e)
            {
                Logger.log("Error Sistema:" + e.Message, Logger.ERROR);
            }
           
        }
        public static void getSucursales(ref ComboBox listadoSucursales)
        {
            DataTable sucursales = null;

            try
            {
                Psql.execQuery("select id,nombre FROM sucursales order by nombre ASC", ref sucursales);
                foreach (DataRow row in sucursales.Rows)
                {
                    ComboboxItem item = new ComboboxItem();
                    item.Text = row[1].ToString();
                    item.Value = Convert.ToInt32(row[0]);
                    listadoSucursales.Items.Add(item);
                }
            }
            catch (Npgsql.NpgsqlException e)
            {
                Logger.log("Error SQL:" + e.Message, Logger.ERROR);
            }
            catch (Exception e)
            {
                Logger.log("Error Sistema:" + e.Message, Logger.ERROR);
            }

        }

        public static void getCajeros(ref ComboBox listadoEmpresas)
        {
            DataTable empresas = null;

            try
            {
                Psql.execQuery("select id,nombre FROM cajero order by nombre ASC", ref empresas);
                foreach (DataRow row in empresas.Rows)
                {
                    ComboboxItem item = new ComboboxItem();
                    item.Text = row[1].ToString();
                    item.Value = Convert.ToInt32(row[0]);
                    listadoEmpresas.Items.Add(item);
                }
            }
            catch (Npgsql.NpgsqlException e)
            {
                Logger.log("Error SQL:" + e.Message, Logger.ERROR);
            }
            catch (Exception e)
            {
                Logger.log("Error Sistema:" + e.Message, Logger.ERROR);
            }

        }
        public static void getCategorias(ref ComboBox listaCategorias)
        {
            DataTable grupos = null;

            try
            {
                Psql.execQuery("select id,nombre_categoria FROM categoria order by nombre_categoria ASC", ref grupos);
                foreach (DataRow row in grupos.Rows)
                {
                    ComboboxItem item = new ComboboxItem();
                    item.Text = row[1].ToString();
                    item.Value = Convert.ToInt32(row[0]);
                    listaCategorias.Items.Add(item);
                }
            }
            catch (Npgsql.NpgsqlException e)
            {
                Logger.log("Error SQL:" + e.Message, Logger.ERROR);
            }
            catch (Exception e)
            {
                Logger.log("Error Sistema:" + e.Message, Logger.ERROR);
            }

        }
        public static bool isPesable(Int64 plu)
        {
            string query = "[plu] = " + plu.ToString();
            DataRow[] foundRowArr = dt.Tables["producto"].Select(query);
            if (foundRowArr.Length != 0)
            {
                return (bool)foundRowArr[0]["pesable"];
            }
            return false;
        }
        public static DataRow lookUp(Int64 plu)
        {
            string query;
            DataRow[] foundRowArr;
            try
            {
                if (plu.ToString().Length < 7)
                {
                    query = "[plu] = " + plu.ToString();
                    foundRowArr = dt.Tables["producto"].Select(query);
                    Logger.log("Looking for PLU " + plu.ToString(), Logger.DEBUG);
                }
                else
                {
                    Logger.log("Looking for BARCODE " + plu.ToString(), Logger.DEBUG);
                    foundRowArr = dt.Tables["producto"].Select(String.Format("barcode='{0}'",plu.ToString()));
                }

                Logger.log(foundRowArr[0]["barcode"].ToString() + " " + foundRowArr[0]["barcode"].GetType(), Logger.DEBUG);
                return foundRowArr[0];
            }
            catch
            {
                throw new Exception("Producto " + plu + " no pudo ser encontrado");
            }
        }
        /*
         * Drop()
         * Borra toda la base de datos local
         */
        public static void drop()
        {
            if (isInitialized)
            {
                try
                {
                    
                    foreach (DataTable tabla in miamiPOS.Properties.Settings.Default.masterSet.Tables)
                    {
                        foreach (DataRow row in tabla.Rows)
                        {
                            row.Delete();
                        }

                    }
                    miamiPOS.Properties.Settings.Default.masterSet = new DataSet() ;
                    miamiPOS.Properties.Settings.Default.Save();
                    isInitialized = false;
                }
                catch
                {
                    throw new Exception("Error al desechar base de datos local ");
                }
            }
            else Logger.log("Nothing to Drop in DB", Logger.DEBUG);
        }
        public static void beginTurno()
        {
            Int32 id_cajero = miamiPOS.Properties.Settings.Default.idCajero;
            string id_sucursal = miamiPOS.Properties.Settings.Default.id_sucursal;
            Int32 id_turno_actual = Convert.ToInt32(Psql.execScalar("select nextval('turno_id_seq')"));
            Int32 rowsAffected = Psql.execInsert("insert into turno(id,id_cajero,fecha,caja_inicial,sucursal) VALUES (" + id_turno_actual + "," + id_cajero + ",now(),"+miamiPOS.Properties.Settings.Default.cajaInicial+","+id_sucursal+")");
            if (rowsAffected == 1)
            {
                id_turno = id_turno_actual;
                miamiPOS.Properties.Settings.Default.idTurno=id_turno_actual;
                miamiPOS.Properties.Settings.Default.Save();
            }
            else throw new Exception("Error para comenzar turno");
        }
        public static void beginTurno(int savedTurno)
        {
            Int32 id_cajero = miamiPOS.Properties.Settings.Default.idCajero;
            Int32 id_turno_actual = savedTurno;
            DataTable turnoRescatado=null;
            Psql.execQuery("select id,id_cajero,fecha,caja_inicial from turno where id=" +id_turno_actual.ToString() ,ref turnoRescatado);
            if (turnoRescatado.Rows.Count == 1)
            {
                var row = turnoRescatado.Rows[0];
                //row={iturno,idcajero,fecha inicio, cajainicial}
                id_turno = id_turno_actual;
                miamiPOS.Properties.Settings.Default.idTurno = id_turno_actual;
                miamiPOS.Properties.Settings.Default.idCajero = (int)row[1];
                miamiPOS.Properties.Settings.Default.Save();

            }
            else throw new Exception("Error para comenzar turno");
        }
        public static void endTurno()
        {
            if (id_turno != 0)
            {
                Int32 rowsAffected = Psql.execInsert("UPDATE turno SET fin_turno=now() WHERE id=" + id_turno);
                if (rowsAffected != 1)
                {
                    throw new Exception("Error en finalizar turno");
                }
                miamiPOS.Properties.Settings.Default.idCajero = 0;
                miamiPOS.Properties.Settings.Default.idTurno = 0;
                miamiPOS.Properties.Settings.Default.Save();
            }
            else throw new Exception("Error al salir de sesion , no esta loggeado?");
        }


        internal static void printSalesDay()
        {
            ESCPrinter myPrinter = new ESCPrinter();
            var portName = miamiPOS.Properties.Settings.Default.printerPortName;
            myPrinter.open(portName);
            myPrinter.initialize();
            myPrinter.lineSpacing(120);
            myPrinter.printMode((byte)(ESCPrinter.DHEIGHT | ESCPrinter.EMPH | ESCPrinter.UNDER));
            myPrinter.justification('c');
            myPrinter.WriteLine("Sandwiches y Comidas MIAMI");
            myPrinter.WriteLine("RESUMEN");
            myPrinter.justification('l');
            myPrinter.printMode((byte)(ESCPrinter.EMPH | ESCPrinter.UNDER));

            var fecha_turno = Psql.execScalar("SELECT turno.fecha FROM turno WHERE turno.id= " + miamiDB.id_turno);
            myPrinter.WriteLine("ID TURNO: " + miamiDB.id_turno.ToString());
            myPrinter.WriteLine("Comienzo: " + fecha_turno);
            myPrinter.WriteLine("Fin:      " + DateTime.Now.ToString());

            string producto = String.Format("{0,-39}{1,-4}{2,5} "
                    ,"Nombre","#","Total");
            myPrinter.WriteLine(producto);

            myPrinter.printMode(0);

            DataTable tableQuery =null;
            string query=String.Format("SELECT producto.nombre as nombre, sum(venta_producto.cantidad) as cantidad ,sum(venta_producto.total) as Total  FROM venta,venta_producto,turno,producto WHERE venta.id_turno=turno.id AND venta.id_venta=venta_producto.id_venta AND turno.sucursal=2 AND venta_producto.plu= producto.plu AND turno.id={0} GROUP BY producto.nombre ORDER BY Total DESC",
                miamiDB.id_turno);
            Psql.execQuery(query,ref tableQuery );
            foreach (DataRow row in tableQuery.Rows)
            {
                producto = String.Format("{0,-39}{1,-4}{2,5}"
                    , row["nombre"].ToString(), row["cantidad"].ToString(), row["total"].ToString());
                myPrinter.WriteLine(producto);
            }
            //total en ticket
            object sumObject;
            sumObject = tableQuery.Compute("Sum(total)", "");

            myPrinter.printMode((byte)(ESCPrinter.DHEIGHT | ESCPrinter.EMPH));
            myPrinter.justification('r');
            myPrinter.WriteLine("TOTAL:" + sumObject.ToString());
            myPrinter.lineFeed();
            myPrinter.lineFeed();
            myPrinter.autoCutter();
            myPrinter.close();
        }

        public static int checkNewProducts()
        {
            if (dt == null || dt.Tables["producto"] == null || !isInitialized)
            {
                Logger.log("Base de datos no iniciada , mejor descarga total",Logger.ERROR);
            }
            try
            {
                string query = String.Format("select count(*) from producto where EXTRACT(year from last_change) >= {0} AND EXTRACT(doy from last_change) >= {1} AND EXTRACT(hour from last_change) >= {2}"
                    , miamiDB.lastUpdate.Year, miamiDB.lastUpdate.DayOfYear, miamiDB.lastUpdate.Hour);
                var found = Psql.execScalar(query);
                if (found == "0")
                {
                    Logger.log("Nada que actualizar.",Logger.INFO);
                    return 0;
                }
                else
                {
                    Logger.log("ACTUALIZAR PRODUCTOS.", Logger.INFO);
                    return Convert.ToInt32(found);
                }
            }
            catch (Npgsql.NpgsqlException e)
            {
                Logger.log("Error SQL:" + e.Message,Logger.ERROR);
            }
            catch (Exception e)
            {
                Logger.log("Error Sistema:" + e.Message, Logger.ERROR);
            }

            return 0;
        }
    }
    /*
     * Clase para carrito
     * codigo-descripcion-cantidad-precio-total
     */
    public class Carrito
    {
        private static DataTable carro;
        private static Int32 total=0;
        private static ESCPrinter myPrinter = new ESCPrinter();

        public Carrito()
        {
            carro = new DataTable("carrito");
            carro.Columns.Add("plu", typeof(int));
            carro.Columns.Add("nombre", typeof(string));
            carro.Columns.Add("cantidad", typeof(int));
            carro.Columns.Add("precio", typeof(int));
            carro.Columns.Add("total", typeof(int));

            DataColumn[] carro_key = { carro.Columns["plu"] };
            carro.PrimaryKey = carro_key;
        }
        internal void printReceipt(string portName)
        {
            myPrinter.open(portName);
            myPrinter.initialize();
            myPrinter.lineSpacing(150);
            myPrinter.printMode((byte)(ESCPrinter.DHEIGHT | ESCPrinter.EMPH | ESCPrinter.UNDER));
            myPrinter.justification('c');
            myPrinter.WriteLine("Sandwiches y Comidas MIAMI");
            myPrinter.justification('l');
            myPrinter.printMode((byte)(ESCPrinter.EMPH | ESCPrinter.UNDER));
            myPrinter.WriteLine(DateTime.Now.ToString());

            string producto = String.Format("{0,-5}{1,-20}{2,-5} ${3,5}"
                    , "Plu","Nombre","#","Total");

            myPrinter.WriteLine(producto);


            myPrinter.printMode(0);

            foreach (DataRow row in carro.Rows)
            {
                producto = String.Format("{0,-5}{1,-20}{2,-5}${3,5}"
                    , row["plu"].ToString(), row["nombre"].ToString(), row["cantidad"].ToString(), row["total"].ToString());
                myPrinter.WriteLine(producto);
            }
            myPrinter.printMode((byte)(ESCPrinter.DHEIGHT | ESCPrinter.EMPH));
            myPrinter.justification('r');
            myPrinter.WriteLine("TOTAL : " + this.subTotal().ToString());
            myPrinter.lineFeed();
            myPrinter.lineFeed();
            myPrinter.autoCutter();
            myPrinter.close();
        }
        public void addItem(Int64 plu, Int32 cantidad)
        {

            try
            {
                //prod={plu,nombre,precio,id_categoria}
                DataRow prod = miamiDB.lookUp(plu);
                try
                {
                    carro.Rows.Add(prod[0], prod[1], cantidad, prod[2], cantidad * Convert.ToInt32(prod[2]));
                }
                catch (Exception e)
                {
                    Logger.log("Exception en ADDITEM: \r " + e.Message, Logger.ERROR);
                    string query;
                    if (plu.ToString().Length < 7) query = "[plu] = " + plu.ToString();
                    else query = "[plu] = " + prod[0].ToString();
                    DataRow[] foundRowArr = carro.Select(query);
                    foundRowArr[0]["cantidad"] = cantidad + (Int32)foundRowArr[0]["cantidad"];
                    foundRowArr[0]["total"] = (Int32)foundRowArr[0]["cantidad"] * (Int32)foundRowArr[0]["precio"];
                    
                }

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        public void addItem(Int64 plu, float cantidad)
        {
            if (cantidad <= 0) throw new Exception("Peso erroneo");
            try
            {
                
                //prod={plu,nombre,precio,id_categoria}
                DataRow prod = miamiDB.lookUp(plu);
                try
                {
                    float total_plata=   cantidad * Convert.ToSingle(prod[2])/1000;
                    if((total_plata % 10) > 5  )total_plata += (10-(total_plata % 10)); //operacion para redondear a las decenas 
                    else total_plata -= (total_plata % 10); //operacion para redondear a las decenas 
                    carro.Rows.Add(prod[0], prod[1], (Int32)cantidad, prod[2], total_plata);
                }
                catch
                {
                    string query;
                    if (plu.ToString().Length < 7) query = "[plu] = " + plu.ToString();
                    else query = "[barcode] = " + prod[0].ToString();   

                    DataRow[] foundRowArr = carro.Select(query);
                    foundRowArr[0]["cantidad"] = cantidad + (Int32)foundRowArr[0]["cantidad"];

                    var total_plata =(Int32)foundRowArr[0]["cantidad"] * (Int32)foundRowArr[0]["precio"]/1000;
                    if ((total_plata % 10) > 5) total_plata += (10 - (total_plata % 10)); //operacion para redondear a las decenas 
                    else total_plata -= (total_plata % 10); //operacion para redondear a las decenas 
                    foundRowArr[0]["total"] = total_plata;
                }

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        public void rmItem(Int64 plu)
        {
            if (this.isAdded(plu))
            {
                String query = "[plu] = " + plu.ToString();
                DataRow[] foundRowArr = carro.Select(query);
                if (foundRowArr.Length != 0)
                {
                    total -= (Int32)foundRowArr[0]["total"];
                    foundRowArr[0].Delete();
                }
            }
            else throw new Exception("Plu no encontrado");
        }
        public bool isAdded(Int64 plu)
        {
            string query;
            if (plu.ToString().Length < 7)
            {
                query = "[plu] = " + plu.ToString();
            }
            else
            {
                query = "[barcode] = " + plu.ToString();
            }
            DataRow[] foundRowArr = carro.Select(query);
            if(foundRowArr.Length != 0 ){
                return true;
            }
            return false;     
        }

        public Int32 subTotal()
        {
            total = 0;
            foreach (DataRow row in carro.Rows)
            {
                total += (Int32)row["total"];
            }
            return total;
        }
        public void cancel()
        {
            while (carro.Rows.Count != 0)
            {
                carro.Rows[0].Delete();
            }
        }
        public void linkGrid(ref DataGridView dgvCarrito)
        {
            dgvCarrito.DataSource = carro;
        }
        public int sendToDB(bool debito)
        {
            if (carro.Rows.Count == 0) throw new Exception("Carro Vacio");
            this.subTotal();
            string esDebito;
            if (debito)
            {
                esDebito = "TRUE";
            }
            else
            {
                esDebito = "FALSE";
            }

            Int32 idVenta = Convert.ToInt32(Psql.execScalar("select nextval('venta_id_venta_seq')"));
            string query = "insert into venta(id_venta,total,fecha,id_turno,debito) VALUES ({0},{1},now(),{2},{3})";
            query = String.Format(query
                , idVenta, total, miamiDB.id_turno, esDebito);
            Int32 rowsAffected = Psql.execInsert(query);
            if (rowsAffected > 0)
            {
                foreach (DataRow row in carro.Rows)
                {

                    rowsAffected = Psql.execInsert("insert into venta_producto(id_venta,plu,cantidad,total) VALUES (" + idVenta + "," + row["plu"] + "," + row["cantidad"]+ "," + row["total"]+")");
                   
                        rowsAffected = Psql.execInsert("update inventario set stock=stock-"+ row["cantidad"] +" where plu="+row["plu"]);
                    
                }
                this.cancel();
                Logger.log("Venta #"+ idVenta.ToString(), Logger.INFO);
                return idVenta;

            }
            else
            {
                throw new Exception("Error al ingresar venta");
            }
        }


    }
    public static class Logger
    {
        private static TextBox messageBox;

        public static byte ERROR { get { return 3; } }
        public static byte WARN { get { return 2; } }
        public static byte INFO { get { return 1; } }
        public static byte DEBUG { get { return 0; } }

        public static void bind(ref TextBox ClientTb)
        {
            messageBox = ClientTb;
        }
        private static void show(string alert)
        {
            messageBox.Text = alert;
        }
        public static void log(string alert, int importance)
        {
            string text_importance= "OTHER";
            if      (importance == 0) text_importance = "DEBUG";
            else if (importance == 1) text_importance = "INFO ";
            else if (importance == 2) text_importance = "WARN ";
            else if (importance == 3) text_importance = "ERROR";
            
            string register_of_log = String.Format("{0} {1} - {2}", text_importance, DateTime.Now.ToString() , alert);
            Console.WriteLine(register_of_log);

            if (importance == INFO) show(alert);
        }
    }
}
