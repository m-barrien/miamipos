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
                this.barcode = "NULL"; //digitos menores que cinco cuentan como un PLU en el programa, asi mejor se borra el codigo de barras invalido
            }

            if (!editar)
            {
                string query = "INSERT INTO producto (plu,  nombre, barcode, precio, id_categoria, pesable) VALUES ({0},'{2}','{1}',{3},{4},{5})";
                if (this.barcode == "NULL") { query = "INSERT INTO producto (plu,  nombre, barcode, precio, id_categoria, pesable) VALUES ({0},'{2}',{1},{3},{4},{5})"; }
                string output = String.Format(query,
                              this.plu, this.barcode,this.name,this.price,this.id_categoria,pesableSQL);
                return output;
            }
            else
            {
                string query ="UPDATE producto SET (nombre, precio, id_categoria, pesable, barcode)=('{1}',{2},{3},{4},'{5}') WHERE plu={0}";
                if (this.barcode == "NULL") { query = "UPDATE producto SET (nombre, precio, id_categoria, pesable, barcode)=('{1}',{2},{3},{4},{5}) WHERE plu={0}"; }
                string output = String.Format(query,
                              this.plu, this.name,this.price,this.id_categoria,pesableSQL,this.barcode);
                return output;
            }
        }
    }
}
