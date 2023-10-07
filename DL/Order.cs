using System;
using System.Collections.Generic;

namespace DL;

public partial class Order
{
    public int OrderId { get; set; }

    public string? CustomerId { get; set; }

    public int? EmployeeId { get; set; }

    public DateTime? OrderDate { get; set; }

    public DateTime? RequiredDate { get; set; }

    public DateTime? ShippedDate { get; set; }

    public int? ShipVia { get; set; }

    public decimal? Freight { get; set; }

    public string? ShipName { get; set; }

    public string? ShipAddress { get; set; }

    public string? ShipCity { get; set; }

    public string? ShipRegion { get; set; }

    public string? ShipPostalCode { get; set; }

    public string? ShipCountry { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual Employee? Employee { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual Shipper? ShipViaNavigation { get; set; }

    public int? ProductID { get; set; }
    public int? EmployeeID { get; set; }
    public string? ProductName { get; set; }
    public short? Quantity { get; set; }

    public Single? Discount { get; set; }

    public decimal? UnitPrice { get; set; }
    public int? SupplierID { get; set; }

    public string? CompanyName { get; set; }

    public string? Address { get; set; }
}
