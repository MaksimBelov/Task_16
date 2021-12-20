using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Class_Product
{
    public class Product
    {
        public int Code { get; set; }
        public string Name { get; set; }
        public double Cost { get; set; }

        public Product(int code, string name, double cost)
        {
            Code = code;
            Name = name;
            Cost = cost;
        }
        public Product(int code, string name)
        {
            Code = code;
            Name = name;
            Cost = 0;
        }
        public Product(int code)
        {
            Code = code;
            Name = null;
            Cost = 0;
        }
        public Product()
        {
            Code = 0;
            Name = null;
            Cost = 0;
        }
    }
}
