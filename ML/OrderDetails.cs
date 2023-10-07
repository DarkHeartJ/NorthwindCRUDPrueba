using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class OrderDetails
    {
        public ML.Products? Product { get; set; }

        public int ProductID { get; set; }

        public short? Quantity { get; set; }

        public Single? Discount { get; set; }

        public decimal? UnitPrice { get; set; }

        public List<object>? OrderDetailss { get; set; }
    }
}
