using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Report
    {
        public static ML.Result ReportJson()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.NorthwindContext context = new DL.NorthwindContext())
                {
                    var query = context.Orders.FromSqlRaw("Reporte").ToList();

                    if (query.Count > 0)
                    {
                        result.Objects = new List<object>();

                        foreach (DL.Order resultOrder in query)
                        {
                            ML.Orders orders = new ML.Orders();
                            orders.OrderID = resultOrder.OrderId;
                            orders.Customer = new ML.Customers
                            {
                                CustomerId = resultOrder.CustomerId ?? string.Empty
                            };
                            orders.Employee = new ML.Employees
                            {
                                EmployeesID = resultOrder.EmployeeId.HasValue ? resultOrder.EmployeeId.Value : 0
                            };
                            orders.OrderDate = resultOrder.OrderDate.GetValueOrDefault();
                            orders.RequiredDate = resultOrder.RequiredDate.GetValueOrDefault();
                            orders.ShippedDate = resultOrder.ShippedDate.GetValueOrDefault();
                            orders.ShipVia = resultOrder.ShipVia.GetValueOrDefault();
                            orders.Freight = resultOrder.Freight.GetValueOrDefault();
                            orders.ShipName = resultOrder.ShipName ?? string.Empty;
                            orders.ShipAddress = resultOrder.ShipAddress ?? string.Empty;
                            orders.ShipCity = resultOrder.ShipCity ?? string.Empty;
                            orders.ShipRegion = resultOrder.ShipRegion ?? string.Empty;
                            orders.ShipPostalCode = resultOrder.ShipPostalCode ?? string.Empty;
                            orders.ShipCountry = resultOrder.ShipCountry ?? string.Empty;
                            orders.Product = new ML.Products
                            {
                                ProductID = resultOrder.ProductID.HasValue ? resultOrder.ProductID.Value : 0,
                                ProductName = resultOrder.ProductName ?? string.Empty
                            };
                            orders.OrderDetail = new ML.OrderDetails
                            {
                                Quantity = resultOrder.Quantity.GetValueOrDefault(),
                                Discount = resultOrder.Discount.GetValueOrDefault(),
                                UnitPrice = resultOrder.UnitPrice.GetValueOrDefault()
                            };
                            orders.Supplier = new ML.Suppliers
                            {
                                SupplierID = resultOrder.SupplierID.GetValueOrDefault(),
                                CompanyName = resultOrder.CompanyName ?? string.Empty,
                                Address = resultOrder.Address ?? string.Empty
                            };

                            result.Objects.Add(orders);
                        }

                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.Message = "An error occurred while displaying the logs." + result.Ex;
            }

            return result;
        }
    }
}
