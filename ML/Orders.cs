using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Orders
    {
        public int OrderID { get; set; }

        public ML.Employees? Employee { get; set; }

        public ML.Customers? Customer { get; set; }

        public ML.OrderDetails? OrderDetail { get; set; }

        public ML.Products? Product { get; set; }

        public ML.Suppliers? Supplier { get; set; }

        public DateTime? OrderDate { get; set; }

        public DateTime? RequiredDate { get; set; }

        public DateTime? ShippedDate { get; set; }

        public int ShipVia { get; set; }

        public decimal? Freight { get; set; }

        public string? ShipName { get; set; }

        public string? ShipAddress { get; set; }

        public string? ShipCity { get; set; }

        public string? ShipRegion { get; set; }

        public string? ShipPostalCode { get; set; }

        public string? ShipCountry { get; set; }

        public List<object>? Orderss { get; set; }
    }
}
