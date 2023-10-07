using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Report
    {
        public ML.Orders? Order { get; set; }

        public ML.Customers? Customer { get; set; }

        public ML.OrderDetails? OrderDetail { get; set; }

        public ML.Products? Product { get; set; }

        public int SupplierID { get; set; }

        public string? CompanyName { get; set; }

        public string? Address { get; set; }

        public List<object>? Reports { get; set; }
    }
}
