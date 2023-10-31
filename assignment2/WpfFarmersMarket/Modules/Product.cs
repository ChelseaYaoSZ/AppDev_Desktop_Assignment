using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfFarmersMarket.Modules
{
    internal class Product
    {
        public int product_id { get; set; }

        public string product_name { get; set; }

        public double amount { get; set; }

        public double price { get; set; }
    }
}
