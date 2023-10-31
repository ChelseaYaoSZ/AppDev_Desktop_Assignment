using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfFarmersMarket.Modules
{
    internal class Response
    {
        public int statusCode { get; set; }
        public string messageCode { get; set; }
        public Product product { get; set; }
        public List<Product> products { get; set; }
    }
}
