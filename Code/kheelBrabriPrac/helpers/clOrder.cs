using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kheelBrabriPrac.helpers
{
    public class clOrder
    {
        public int product_id { get; set; }
        public string product_name { get; set; }
        public int each_cost { get; set; }
        public int qty { get; set; }
        public int discount { get; set; }
        public int cost { get; set; }
    }
}
