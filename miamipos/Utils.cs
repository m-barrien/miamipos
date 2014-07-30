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
}
