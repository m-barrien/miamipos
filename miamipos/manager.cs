using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace miamiPOS
{
    
    public partial class manager : Form
    {
       //para tab sobre resumen de dia
        DataTable tablaProductos = null;
        DataTable tablaVentas = null;
        DataTable tablaFacturas = null;
        DataTable tablaRetiros = null;
        //Para tab sobre turnos
        DataTable tablaTurnos = null;
        //Para tab sobre cuentas
        DataTable tablaEmpresas = null;
        DataTable tablaCajeros = null;
        DataTable tablaSucursales = null;
        DataTable tablaCategorias = null;



        public manager()
        {
            InitializeComponent();

            // Para la pestaña de anticipos
            //dateTimePicker.Format = DateTimePickerFormat.Custom;
            //dateTimePicker.CustomFormat = "MM - yyyy";
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void manager_Load(object sender, EventArgs e)
        {
            miamiDB.getCategorias(ref comboBoxCategorias);
            miamiDB.getCategorias(ref cbCategoria);
            miamiDB.getSucursales(ref comboBoxSucursales1);
            comboBoxSucursales1.SelectedIndex = 0;
            checkBoxEditMode.Enabled = false; //Deshabilitar el checkmox de modo editar
        }

        private void comboBoxCategorias_Leave(object sender, EventArgs e)
        {
            Int32 idCategoria = Convert.ToInt32((comboBoxCategorias.SelectedItem as ComboboxItem).Value);
            Psql.execQuery("select  plu, barcode, nombre, precio, pesable, id_categoria from producto where id_categoria=" + idCategoria, ref tablaProductos);
            dataGridViewProductos.DataSource = tablaProductos;

            dataGridViewProductos.Columns["id_categoria"].Visible = false;
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            try
            {
                int selectedrowindex = dataGridViewProductos.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = dataGridViewProductos.Rows[selectedrowindex]; 

                var plu = Convert.ToInt32( selectedRow.Cells["plu"].Value ); //plu de item seleccionado
                Psql.execScalar("delete from producto where plu=" + plu);
                dataGridViewProductos.Rows.RemoveAt( selectedrowindex );
            }
            catch
            {
                Logger.log("Error al eliminar",Logger.WARN);
            }
        }

        //Esta funcion rellena las textbox de modo editar
        private void fillEditor(int plu, string barcode, string name, int price,int id_categoria, bool pesable)
        {
            tbPLU.Text = plu.ToString();
            tbBarcode.Text = barcode.ToString();
            tbName.Text = name.ToString();
            tbPrice.Text = price.ToString();
            checkBoxPesable.Checked = pesable;

            // Siguiente foreach para dejar la categoria del item seleccionada en modo editar
            foreach (ComboboxItem cbItem in this.cbCategoria.Items)
            {
                if (cbItem.Value == id_categoria)
                {
                    this.cbCategoria.SelectedItem = cbItem;
                }
            }
        }
        //Esta funcion retorna un objeto Producto con los valores obtenidos desde las textbox
        private Producto getFromEditor()
        {
            try
            {
                int plu;
                if (tbPLU.Text.Length < 1) plu = 0;
                else plu = Convert.ToInt32(tbPLU.Text);

                Int32 idCategoria = Convert.ToInt32((cbCategoria.SelectedItem as ComboboxItem).Value);
                return new Producto(plu, tbBarcode.Text, tbName.Text, Convert.ToInt32(tbPrice.Text), idCategoria, checkBoxPesable.Checked);
            }
            catch
            {
                throw new Exception("Producto en Editor no respeta la estructura deseada");
            }
        }
        private void buttonEdit_Click(object sender, EventArgs e)
        {
            try
            {
                int selectedrowindex = dataGridViewProductos.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = dataGridViewProductos.Rows[selectedrowindex];

                var plu = selectedRow.Cells["plu"].Value; //plu de item seleccionado
                var barcode = selectedRow.Cells["barcode"].Value;
                var nombre = selectedRow.Cells["nombre"].Value;
                var precio = selectedRow.Cells["precio"].Value;
                var pesable = Convert.ToBoolean (selectedRow.Cells["pesable"].Value );
                var id_categoria = selectedRow.Cells["id_categoria"].Value;


                fillEditor((int)plu, barcode.ToString() , (string)nombre, Convert.ToInt32(precio), (int)id_categoria, (bool)pesable);

                checkBoxEditMode.Checked = true; //activar modo editar
              
            }
            catch(Exception E)
            {
                Logger.log("Nada que Editar \r catch: " + E.Message,Logger.WARN);
            }
        }

        private void buttonNewItem_Click(object sender, EventArgs e)
        {
            foreach (Control x in this.groupBox1.Controls)
            {
                if (x is TextBox)
                {
                    ((TextBox)x).Clear();
                }
            }
            checkBoxEditMode.Checked = false;
            cbCategoria.SelectedItem = null;
            tbPLU.Focus();
        }
        private void newItem()
        {
            foreach (Control x in this.groupBox1.Controls)
            {
                if (x is TextBox)
                {
                    ((TextBox)x).Clear();
                }
            }
            checkBoxEditMode.Checked = false;
            cbCategoria.SelectedItem = null;
            tbPLU.Focus();
        }

        private void buttonSaveItem_Click(object sender, EventArgs e)
        {
            try
            {
                Producto productoAEditar = getFromEditor();
                Logger.log(productoAEditar.toSQL(checkBoxEditMode.Checked),Logger.DEBUG);

                Psql.execInsert(productoAEditar.toSQL(checkBoxEditMode.Checked));

                newItem();

                MessageBox.Show("EXITO");
            }
            catch (Exception E)
            {
                MessageBox.Show("ERROR AL ACTUALIZAR :\r" + E.Message);
            }

        }

        private void checkBoxEditMode_CheckedChanged(object sender, EventArgs e)
        {
            tbPLU.ReadOnly = checkBoxEditMode.Checked; //si esta en modo editar el textbox del plu se bloquea
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string query = "select  plu, barcode, nombre, precio, pesable, id_categoria from producto ";
            string whereCond = "";
            //Si la busqueda esta vacia
            if (textBoxSearch.Text.Length < 1)          whereCond ="ORDER BY last_change DESC LIMIT 30";
            // Si la busqueda no esta vacia
            else
            {
                // Todas las busquedas de digitos
                var m =Regex.IsMatch(textBoxSearch.Text, @"^\d+");
                if (m)
                {
                    whereCond = String.Format("where plu={0} OR barcode='{0}'", textBoxSearch.Text);
                }
                else whereCond = String.Format("where upper(nombre) LIKE upper('%{0}%')", textBoxSearch.Text);
            }

            Psql.execQuery(query + whereCond, ref tablaProductos);
            dataGridViewProductos.DataSource = tablaProductos;
            dataGridViewProductos.Columns["id_categoria"].Visible = false;
        }

        private void coneccionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Abre la pagina web de la interfaz guardada en las settings serverRemote
            System.Diagnostics.Process.Start(miamiPOS.Properties.Settings.Default.serverRemote);
        }
        private void textBoxSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                button1_Click(sender, new EventArgs());
            }
        }

        /*
         * Bloque para la pestaña pagos que incluye colaciones anticipos y facturas
         * 
         * 
         */

        private void dateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            Int32 idLocal = Convert.ToInt32((comboBoxSucursales1.SelectedItem as ComboboxItem).Value);
            // dateTimePicker.Value.Month
            int doy = dateTimePicker.Value.DayOfYear;
            int year = dateTimePicker.Value.Year;
            var query = String.Format("select nombre,sum(cantidad) as Cantidad,sum(venta_producto.total) as Dinero from producto,venta_producto,venta,turno where venta.id_venta = venta_producto.id_venta and venta_producto.plu=producto.plu and producto.pesable={0} and extract(year from venta.fecha)={1} and extract(doy from venta.fecha)={2} and turno.id=venta.id_turno and turno.sucursal={3} group by nombre order by Dinero DESC"
                ,checkBoxPesableventa.Checked.ToString(),year,doy,idLocal);
            Psql.execQuery(query, ref tablaVentas);
            dataGridViewVentas.DataSource = tablaVentas;
            //Se lleno el datagridViewventas

            //Ahora se rellena las facturas
            query = String.Format("select factura.codigo,empresa.nombre, factura.total from empresa,factura,turno where factura.id_empresa=empresa.id and factura.id_turno=turno.id and turno.sucursal={2} and extract(year from factura.fecha)={0} and extract(doy from factura.fecha)={1} order by factura.total DESC"
                , year, doy,idLocal);
            Psql.execQuery(query, ref tablaFacturas);
            dataGridViewFacturas.DataSource = tablaFacturas;

            //Ahora se rellena los anticipos
            query = String.Format("select cajero.nombre, anticipo.total from anticipo,cajero,turno where cajero.id=anticipo.id_deudor and anticipo.id_turno=turno.id and turno.sucursal={2} and extract(year from anticipo.fecha)={0} and extract(doy from anticipo.fecha)={1} order by anticipo.total DESC"
                , year, doy, idLocal);
            Psql.execQuery(query, ref tablaRetiros);
            dataGridViewRetiros.DataSource = tablaRetiros;

            dataGridViewVentas.Columns["dinero"].DefaultCellStyle.Format = "$##,###,###";
            //Se rellenaron los dataGridView


            //Ahora se rellenanan los campos de totales
            try
            {
                
                ResumenDiario Resumen = new ResumenDiario(doy, year, idLocal);
                textBoxVentas.Text = Resumen.ventas.ToString();
                textBoxAnticipos.Text = Resumen.anticipos.ToString();
                textBoxColaciones.Text = Resumen.colaciones.ToString();
                textBoxFacturas.Text = Resumen.facturas.ToString();
                textBoxDebito.Text = Resumen.debito.ToString();

                textBoxCfinal.Text = Resumen.cajaFinal.ToString();
                textBoxCinicial.Text = Resumen.cajaInicial.ToString();
                textBoxRetiros.Text = Resumen.getRetiros().ToString();
                
            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message);
            }
        }

        private void checkBoxPesableventa_CheckedChanged(object sender, EventArgs e)
        {
            Int32 idLocal = Convert.ToInt32((comboBoxSucursales1.SelectedItem as ComboboxItem).Value);
            int doy = dateTimePicker.Value.DayOfYear;
            int year = dateTimePicker.Value.Year;
            var query = String.Format("select nombre,sum(cantidad) as Cantidad,sum(venta_producto.total) as Dinero from producto,venta_producto,venta,turno where venta.id_venta = venta_producto.id_venta and venta_producto.plu=producto.plu and producto.pesable={0} and extract(year from venta.fecha)={1} and extract(doy from venta.fecha)={2} and turno.id=venta.id_turno and turno.sucursal={3} group by nombre order by Dinero DESC"
                , checkBoxPesableventa.Checked.ToString(), year, doy,idLocal);
            Psql.execQuery(query, ref tablaVentas);
            dataGridViewVentas.DataSource = tablaVentas;
            //Se lleno el datagridViewventas
        }

        private void inventarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            iInventario formInventario = new iInventario();
            formInventario.Show();
        }

        private void buttonCrearStock_Click(object sender, EventArgs e)
        {
            try
            {
                int selectedrowindex = dataGridViewProductos.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = dataGridViewProductos.Rows[selectedrowindex];

                var plu = selectedRow.Cells["plu"].Value.ToString(); //plu de item seleccionado
                Psql.execInsert(String.Format("insert into inventario (plu,stock) VALUES ({0},0)",plu));
            }
            catch(Exception error)
            {
                MessageBox.Show("ERROR - STOCK YA CREADO \r" + error.Message);
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            int doy = dateTimePicker1.Value.DayOfYear;
            int year = dateTimePicker1.Value.Year;
            var query = String.Format(" select id,nombre_cajero,local,comienzo_turno,fin_turno,caja_inicial,caja_final,total_ventas,debito,gastos,retiro,error from resumen_turno where extract(year from comienzo_turno)={0} and extract(doy from comienzo_turno)={1}"
                , year, doy);
            Psql.execQuery(query, ref tablaTurnos);
            dataGridViewTurnos.DataSource = tablaTurnos;
            
            foreach(DataGridViewRow row in dataGridViewTurnos.Rows  )
            {
                double prop_error = Math.Abs(Convert.ToDouble(row.Cells["error"].Value)/5000);
                if (prop_error > 1) prop_error = 1;
                byte red = Convert.ToByte(prop_error * 255);
                row.Cells["error"].Style.BackColor = Color.FromArgb(red, 255-red, 0);
            }
        }

        private void buttonUpdateAcc_Click(object sender, EventArgs e)
        {
            Psql.execQuery("select id,nombre,password from cajero ORDER BY id", ref tablaCajeros);
            Psql.execQuery("select * from empresa ORDER BY id", ref tablaEmpresas);
            Psql.execQuery("select id, nombre_categoria as nombre from categoria ORDER BY id", ref tablaCategorias);
            Psql.execQuery("select * from sucursales ORDER BY id", ref tablaSucursales);
            dataGridViewCajeros.DataSource = tablaCajeros;
            dataGridViewCajeros.Columns["id"].ReadOnly = true;
            dataGridViewEmpresas.DataSource = tablaEmpresas;
            dataGridViewEmpresas.Columns["id"].ReadOnly = true;
            dataGridViewGrupos.DataSource = tablaCategorias;
            dataGridViewGrupos.Columns["id"].ReadOnly = true;
            dataGridViewSucursales.DataSource = tablaSucursales;
            dataGridViewSucursales.Columns["id"].ReadOnly = true;

        }

        private void dataGridViewCajeros_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            try
            {
                int currentMaxId = (int)((sender as DataGridView).DataSource as DataTable).Compute("Max(id)", "");
                (sender as DataGridView).Rows[(e.Row as DataGridViewRow).Index - 1].Cells["id"].Value = currentMaxId + 1;
            }
            catch (InvalidCastException)
            {
                (sender as DataGridView).Rows[(e.Row as DataGridViewRow).Index - 1].Cells["id"].Value = 1;
            }
            
        }
        private void buttonSaveEmpresas_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in dataGridViewEmpresas.Rows)
                {
                    string query = String.Format("UPDATE {2} SET nombre = '{1}' where id ={0}; INSERT INTO {2}(id,nombre) SELECT {0},'{1}' WHERE NOT EXISTS (SELECT 1 FROM {2} WHERE id={0});"
                        , row.Cells["id"].Value.ToString(), row.Cells["nombre"].Value.ToString(), "empresa");
                    Psql.execInsert(query);
                }
            }
            catch (NullReferenceException) { }
            catch
            {
                MessageBox.Show("Error");
            }

            try
            {
                foreach (DataGridViewRow row in dataGridViewSucursales.Rows)
                {
                    string query = String.Format("UPDATE {2} SET nombre = '{1}' where id ={0}; INSERT INTO {2}(id,nombre) SELECT {0},'{1}' WHERE NOT EXISTS (SELECT 1 FROM {2} WHERE id={0});"
                        , row.Cells["id"].Value.ToString(), row.Cells["nombre"].Value.ToString(), "sucursales");
                    Psql.execInsert(query);
                }
            }
            catch (NullReferenceException) { }
            catch
            {
                MessageBox.Show("Error");
            }

            try
            {
                foreach (DataGridViewRow row in dataGridViewGrupos.Rows)
                {
                    string query = String.Format("UPDATE {2} SET nombre_categoria = '{1}' where id ={0}; INSERT INTO {2}(id,nombre_categoria) SELECT {0},'{1}' WHERE NOT EXISTS (SELECT 1 FROM {2} WHERE id={0});"
                        , row.Cells["id"].Value.ToString(), row.Cells["nombre"].Value.ToString(), "categoria");
                    Psql.execInsert(query);
                }
            }
            catch (NullReferenceException) { }
            catch
            {
                MessageBox.Show("Error");
            }

            try
            {
                foreach (DataGridViewRow row in dataGridViewCajeros.Rows)
                {
                    string query = String.Format("UPDATE cajero SET nombre = '{1}',password = '{2}' where cajero.id ={0}; INSERT INTO cajero(id,nombre,password) SELECT {0},'{1}','{2}' WHERE NOT EXISTS (SELECT 1 FROM cajero WHERE id={0});"
                        , row.Cells["id"].Value.ToString(), row.Cells["nombre"].Value.ToString(), row.Cells["password"].Value.ToString());
                    Psql.execInsert(query);
                }
            }
            catch (NullReferenceException) { }
            catch
            {
                MessageBox.Show("Error");
            }
        }

        private void dataGridViewAcc_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            string dataGridName = (sender as DataGridView).Name, tableName= null;
            if(dataGridName.Contains("mpresa"))
            {
                tableName = "empresa";
            }
            else if (dataGridName.Contains("ajero"))
            {
                tableName = "cajero";
            }
            else if (dataGridName.Contains("rupo"))
            {
                tableName = "categoria";
            }
            else if (dataGridName.Contains("ucursal"))
            {
                tableName = "sucursales";
            }
            string query = String.Format("DELETE FROM {0} where id ={1}"
                ,tableName,(e.Row as DataGridViewRow).Cells["id"].Value.ToString());
            try
            {
                Psql.execInsert(query);
            }
            catch
            {
                MessageBox.Show("No se pudo borrar, dato con historial?");
                e.Cancel = true;
            }
        }
    }
}
