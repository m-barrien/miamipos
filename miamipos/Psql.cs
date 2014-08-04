using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
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

                Console.WriteLine("Se agregaron {0} lineas en la BD", rowsAffected);
                
            }
            catch (Npgsql.NpgsqlException e)
            {
                throw new Exception("Sin conexion \r" + e.Message);
            }
            finally
            {
                conn.Close();
                
            }
            return rowsAffected;
        
        }
        public static String execScalar(String query)
        {
            conn.Open();

            NpgsqlCommand command = new NpgsqlCommand(query, conn);
            String result = "" ;

            try
            {
                result = Convert.ToString( command.ExecuteScalar());
                Console.WriteLine("Executed scalar query with result: {0}", result);
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
                throw new Exception("Sin conexion \r" + e.Message);
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
            else Console.WriteLine("Using Backup database");
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
                Console.WriteLine("Se desea esconder columna inexistente");
            }
            catch
            {
                Console.WriteLine("DataGrid not linked to table {0}", tableName);
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
                Psql.execQuery("select plu,nombre,precio,categoria.nombre_categoria,pesable,barcode FROM producto,categoria where producto.id_categoria=categoria.id", ref dt, "producto");
                miamiPOS.Properties.Settings.Default.masterSet = dt;
                miamiPOS.Properties.Settings.Default.Save();
                saveTime();
                isInitialized = true;
                
            }
            catch (Npgsql.NpgsqlException e)
            {
                Console.WriteLine("Error SQL:"+ e.Message);
            }
            catch(Exception e)
            {
                Console.WriteLine("Error Sistema:"+ e.Message);
            }

        }
        public static int loadProductsIncremental()
        {
            DataTable producto=null;
            if (dt == null || dt.Tables["producto"] == null || !isInitialized)
            {
                throw new InvalidOperationException("Base de datos no iniciada , mejor descarga total");
            }
            try
            {
                var found = Psql.execScalar("select count(*) from producto where last_change>= timestamp '" + miamiDB.lastUpdate.ToString() +"'");
                if (found == "0")
                {
                    Console.WriteLine("NADA QUE ACTUALIZAR");
                    return 0;
                }
                else
                {
                    Psql.execQuery("select plu,nombre,precio,categoria.nombre_categoria,pesable,barcode FROM producto,categoria where producto.id_categoria=categoria.id AND last_change>= timestamp '" + miamiDB.lastUpdate.ToString() + "'", ref producto);
                    Console.WriteLine("Se van a actualizar " + producto.Rows.Count + " productos");
                    foreach (DataRow newrow in producto.Rows)
                    {
                        Console.WriteLine(newrow[0] + " " + newrow[1]);
                        var oldrows = dt.Tables["producto"].Select("[plu]=" + newrow["plu"]);
                        try
                        {
                            oldrows[0].Delete();
                        }
                        catch (IndexOutOfRangeException)
                        {
                            Console.Write("Nuevo producto!");
                        }
                        dt.Tables["producto"].ImportRow(newrow);
                    }

                    miamiPOS.Properties.Settings.Default.masterSet = dt;
                    miamiPOS.Properties.Settings.Default.Save();
                    saveTime();
                    return producto.Rows.Count;
                }
            }
            catch (Npgsql.NpgsqlException e)
            {
                Console.WriteLine("Error SQL:" + e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error Sistema:" + e.Message);
            }

            return 0;
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
                Console.WriteLine("Error SQL:" + e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error Sistema:" + e.Message);
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
                Console.WriteLine("Error SQL:" + e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error Sistema:" + e.Message);
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
                Console.WriteLine("Error SQL:" + e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error Sistema:" + e.Message);
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
                    Console.WriteLine("Looking for PLU " + plu.ToString());
                }
                else
                {
                    Console.WriteLine("Looking for BARCODE "+plu.ToString());
                    foundRowArr = dt.Tables["producto"].Select(String.Format("barcode='{0}'",plu.ToString()));
                }
            
                Console.WriteLine(foundRowArr[0]["barcode"].ToString() + " " + foundRowArr[0]["barcode"].GetType());
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
            else Console.WriteLine("Nothing to Drop in DB");
        }
        public static void beginTurno()
        {
            Int32 id_cajero = miamiPOS.Properties.Settings.Default.idCajero;
            Int32 id_turno_actual = Convert.ToInt32(Psql.execScalar("select nextval('turno_id_seq')"));
            Int32 rowsAffected = Psql.execInsert("insert into turno(id,id_cajero,fecha,caja_inicial) VALUES (" + id_turno_actual + "," + id_cajero + ",now(),"+miamiPOS.Properties.Settings.Default.cajaInicial+")");
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
            myPrinter.justification('c');
            myPrinter.WriteLine("Sandwiches y Comidas MIAMI");

            foreach (DataRow row in carro.Rows)
            {
                myPrinter.justification('l');
                myPrinter.WriteLine(row["nombre"].ToString());

                myPrinter.justification('r');
                myPrinter.WriteLine(row["cantidad"].ToString() + "\t");
                myPrinter.WriteLine(row["total"].ToString());

            }
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
                    Console.WriteLine("Exception en ADDITEM: \r "+e.Message);
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
                catch(Exception e)
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
        public void sendToDB()
        {
            if (carro.Rows.Count == 0) throw new Exception("Carro Vacio");

            this.subTotal();
            Int32 idVenta = Convert.ToInt32(Psql.execScalar("select nextval('venta_id_venta_seq')"));
            string query = "insert into venta(id_venta,total,fecha,id_turno) VALUES (" + idVenta + "," + total + ",now()," + miamiDB.id_turno + ")";
            Int32 rowsAffected = Psql.execInsert(query);
            if (rowsAffected > 0)
            {
                foreach (DataRow row in carro.Rows)
                {

                    rowsAffected = Psql.execInsert("insert into venta_producto(id_venta,plu,cantidad,total) VALUES (" + idVenta + "," + row["plu"] + "," + row["cantidad"]+ "," + row["total"]+")");
                    if(!miamiDB.isPesable((Int32)row["plu"]))
                    {
                        rowsAffected = Psql.execInsert("update inventario set stock=stock-"+ row["cantidad"] +" where plu="+row["plu"]);
                    }
                }
                this.cancel();

            }
            else
            {
                throw new Exception("Error al ingresar venta");
            }
        }


    }

}
