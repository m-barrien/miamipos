using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace miamiPOS
{

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
            string pesableSQL="FALSE";
            if(pesable)
            {
                pesableSQL="TRUE";
            }
            if (barcode.Length <= 5)
            {
                this.barcode = "NULL"; //digitos menores que cinco cuentan como un PLU en el programa
            }
            else
            {
                this.barcode = "\'" + this.barcode + "\'";
            }
            if (!editar)
            {
                string output = String.Format("INSERT INTO producto (plu, barcode, nombre, precio, id_categoria, pesable) VALUES ({0},{1},'{2}',{3},{4},{5})",
                              this.plu, this.barcode,this.name,this.price,this.id_categoria,pesableSQL);
                return output;
            }
            else
            {
                string output = String.Format("UPDATE producto SET (nombre, precio, id_categoria, pesable)=('{1}',{2},{3},{4}) WHERE plu={0}",
                              this.plu, this.name,this.price,this.id_categoria,pesableSQL);
                return output;
            }
        }
    }
}
