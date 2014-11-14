using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace miamiPOS
{
    //Utilidad para finalizar el turno y mostrar totales para un turno
    public class ResumenTurno
    {
        public int ventas { get; set; }
        public int facturas { get; set; }
        public int colaciones { get; set; }
        public int anticipos { get; set; }
        public int cajaInicial { get; set; }
        public int cajaFinal { get; set; }
        public ResumenTurno(int idTurno)
        {
            //VENTAS
            var query = String.Format("select sum( venta.total ) from venta where venta.id_turno = {0}"
                , idTurno);
            var dump = Psql.execScalar(query);
            try
            {
                this.ventas = Convert.ToInt32(dump);
            }
            catch
            {
                this.ventas = 0;
            }
            //ANTICIPOS
            query = String.Format("select sum( anticipo.total ) from anticipo where anticipo.id_turno ={0}"
                , idTurno);
            dump = Psql.execScalar(query);
            try
            {
                this.anticipos = Convert.ToInt32(dump);
            }
            catch
            {
                this.anticipos = 0;
            }

            //COLACIONES
            query = String.Format("select sum( colacion.total ) from colacion where colacion.id_turno ={0}"
                 , idTurno);
            dump = Psql.execScalar(query);
            try
            {
                this.colaciones = Convert.ToInt32(dump);
            }
            catch
            {
                this.colaciones = 0;
            }
            //FACTURAS
            query = String.Format("select sum( factura.total ) from factura where factura.id_turno ={0}"
                 , idTurno);
            dump = Psql.execScalar(query);
            try
            {
                this.facturas = Convert.ToInt32(dump);
            }
            catch
            {
                this.facturas = 0;
            }
            //CAJA INICIAL
            query = String.Format("select turno.caja_inicial from turno where anticipo.id_turno ={0}"
                 , idTurno);
            dump = Psql.execScalar(query);
            try
            {
                this.cajaInicial = Convert.ToInt32(dump);
            }
            catch
            {
                this.cajaInicial = 0;
            }

        }

    }
    //Utilidad para el manager y asi resumir datos de ventas
    public class ResumenDiario
    {
        public int ventas { get; set; }
        public int facturas { get; set; }
        public int colaciones { get; set; }
        public int anticipos { get; set; }
        public int cajaInicial { get; set; }
        public int cajaFinal { get; set; }

        public ResumenDiario(int doy, int year,int idLocal)
        {
            //VENTAS
            var query = String.Format("select sum( venta.total ) from venta,turno where extract(year from venta.fecha)={0} and extract(doy from venta.fecha)={1} and turno.id=venta.id_turno and turno.sucursal={2}"
                , year, doy,idLocal);
            var dump = Psql.execScalar(query);
            try
            {
                this.ventas = Convert.ToInt32(dump);
            }
            catch
            {
                this.ventas = 0;
            }
            //ANTICIPOS
            query = String.Format("select sum( anticipo.total ) from anticipo,turno where extract(year from anticipo.fecha)={0} and extract(doy from anticipo.fecha)={1} and turno.id=anticipo.id_turno and turno.sucursal={2}"
                , year, doy, idLocal);
            dump = Psql.execScalar(query);
            try
            {
                this.anticipos = Convert.ToInt32(dump);
            }
            catch
            {
                this.anticipos = 0;
            }

            //COLACIONES
            query = String.Format("select sum( colacion.total ) from colacion,turno where extract(year from colacion.fecha)={0} and extract(doy from colacion.fecha)={1} and turno.id=colacion.id_turno and turno.sucursal={2}"
                 , year, doy, idLocal);
            dump = Psql.execScalar(query);
            try
            {
                this.colaciones = Convert.ToInt32(dump);
            }
            catch
            {
                this.colaciones = 0;
            }
            //FACTURAS
            query = String.Format("select sum( factura.total ) from factura,turno where extract(year from factura.fecha)={0} and extract(doy from factura.fecha)={1} and turno.id=factura.id_turno and turno.sucursal={2}"
                 , year, doy ,idLocal);
            dump = Psql.execScalar(query);
            try
            {
                this.facturas = Convert.ToInt32(dump);
            }
            catch
            {
                this.facturas = 0;
            }
            //CAJA INICIAL
            query = String.Format("select turno.caja_inicial from turno where extract(year from turno.fecha)={0} and extract(doy from turno.fecha)={1} and turno.sucursal={2}  order by id ASC"
                 , year, doy, idLocal);
            dump = Psql.execScalar(query);
            try
            {
                this.cajaInicial = Convert.ToInt32(dump);
            }
            catch
            {
                this.cajaInicial = 0;
            }
            //CAJA FINAL
            query = String.Format("select turno.caja_final from turno where extract(year from turno.fecha)={0} and extract(doy from turno.fecha)={1} and turno.sucursal={2}  order by id DESC"
                 , year, doy, idLocal);
            dump = Psql.execScalar(query);
            try
            {
                this.cajaFinal = Convert.ToInt32(dump);
            }
            catch
            {
                this.cajaFinal = 0;
            } 
        }

        public int getLiquido()
        {
            return this.ventas + this.cajaInicial - this.facturas - this.colaciones - this.anticipos;
        }
        public int getRetiros()
        {
            return this.getLiquido() - this.cajaFinal;
        }
    }
    //Custom class para los itemes de las empresas
    public class ComboboxItem
    {
        public String Text { get; set; }
        public int Value { get; set; }

        public override string ToString()
        {
            return Text;
        }
    }
    public static class Pesable
    {
        public static float Gramos { get; set; }
    }


    public class Producto
    {
        int plu;
        string barcode;
        string name;
        int price;
        int id_categoria;
        bool pesable;
        public Producto(int plu, string barcode, string name, int price, int id_categoria, bool pesable)
        {
            this.plu = plu;
            this.barcode = barcode;
            this.name = name;
            this.price = price;
            this.id_categoria = id_categoria;
            this.pesable = pesable;
        }
        public string toSQL(bool editar)
        {
            string pesableSQL="FALSE",pluSQL="DEFAULT";

            if (plu > 0) pluSQL = plu.ToString();

            if(pesable)
            {
                pesableSQL="TRUE";
            }
            if (this.barcode.Length <= 5)
            {
                this.barcode = "NULL"; //digitos menores que cinco cuentan como un PLU en el programa, asi mejor se borra el codigo de barras invalido
            }

            if (!editar)
            {
                System.Threading.Thread.Sleep(4000);
                string query = "INSERT INTO producto (plu,  nombre, barcode, precio, id_categoria, pesable) VALUES ({0},'{2}','{1}',{3},{4},{5})";
                if (this.barcode == "NULL") query = "INSERT INTO producto (plu,  nombre, barcode, precio, id_categoria, pesable) VALUES ({0},'{2}',NULL,{3},{4},{5})"; 

                string output = String.Format(query,
                              pluSQL, this.barcode,this.name,this.price,this.id_categoria,pesableSQL);
                return output;
            }
            else
            {
                string query = "UPDATE producto SET (nombre, precio, id_categoria, pesable, barcode,last_change)=('{1}',{2},{3},{4},'{5}',now()) WHERE plu={0}";
                if (this.barcode == "NULL")  query = "UPDATE producto SET (nombre, precio, id_categoria, pesable, barcode,last_change)=('{1}',{2},{3},{4},{5},now()) WHERE plu={0}"; 
                string output = String.Format(query,
                              this.plu, this.name, this.price, this.id_categoria, pesableSQL, this.barcode);
                return output;
            }
        }
    }
}
