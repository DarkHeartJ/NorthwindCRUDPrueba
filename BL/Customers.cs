using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Customers
    {
        public static ML.Result GetByCustomerId(string customerId)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.NorthwindContext context = new DL.NorthwindContext())
                {
                    var query = context.Customers.FromSqlRaw($"CustomerGetByCustomerId {customerId}").AsEnumerable().FirstOrDefault();

                    if (query != null)
                    {
                        ML.Customers customers = new ML.Customers();
                        customers.CustomerId = query.CustomerId;
                        customers.ContactName = query.ContactName;

                        result.Object = customers;
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
                result.Message = "An error occurred while displaying the log." + result.Ex;
            }
            return result;
        }

        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.NorthwindContext context = new DL.NorthwindContext())
                {
                    var query = context.Customers.FromSqlRaw("CustomersGetAll").ToList();

                    if (query.Count > 0)
                    {
                        result.Objects = new List<object>();

                        foreach (DL.Customer resultCustomer in query)
                        {
                            ML.Customers customers = new ML.Customers();
                            customers.CustomerId = resultCustomer.CustomerId;
                            customers.CompanyName = resultCustomer.CompanyName;
                            customers.ContactName = resultCustomer.ContactName;
                            customers.ContactTitle = resultCustomer.ContactTitle;
                            customers.Address = resultCustomer.Address;
                            customers.City = resultCustomer.City;
                            customers.Region = resultCustomer.Region;
                            customers.PostalCode = resultCustomer.PostalCode;
                            customers.Country = resultCustomer.Country;
                            customers.Phone = resultCustomer.Phone;
                            customers.Fax = resultCustomer.Fax;

                            result.Objects.Add(customers);
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

        public static ML.Result GetById(string customerId)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.NorthwindContext context = new DL.NorthwindContext())
                {
                    var query = context.Customers.FromSqlRaw($"CustomersGetById {customerId}").AsEnumerable().FirstOrDefault();

                    if (query != null)
                    {
                        ML.Customers customers = new ML.Customers();
                        customers.CustomerId = query.CustomerId;
                        customers.CompanyName = query.CompanyName;
                        customers.ContactName = query.ContactName;
                        customers.ContactTitle = query.ContactTitle;
                        customers.Address = query.Address;
                        customers.City = query.City;
                        customers.Region = query.Region;
                        customers.PostalCode = query.PostalCode;
                        customers.Country = query.Country;
                        customers.Phone = query.Phone;
                        customers.Fax = query.Fax;

                        result.Object = customers;
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
                result.Message = "An error occurred while displaying the log." + result.Ex;
            }
            return result;
        }

        public static ML.Result Add(ML.Customers customers)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.NorthwindContext context = new DL.NorthwindContext())
                {
                    int queryResult = context.Database.ExecuteSqlRaw($"CustomersAdd '{customers.CustomerId}', '{customers.CompanyName}', '{customers.ContactName}', '{customers.ContactTitle}', '{customers.Address}', '{customers.City}', '{customers.Region}', '{customers.PostalCode}', '{customers.Country}', '{customers.Phone}', '{customers.Fax}'");

                    if (queryResult > 0)
                    {
                        result.Correct = true;
                        result.Message = "The record was inserted correctly.";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.Message = "An error occurred while inserting the record." + result.Ex;
            }

            return result;
        }

        public static ML.Result Update(ML.Customers customers)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.NorthwindContext context = new DL.NorthwindContext())
                {
                    int queryResult = context.Database.ExecuteSqlRaw($"CustomersUpdate '{customers.CustomerId}', '{customers.CompanyName}', '{customers.ContactName}', '{customers.ContactTitle}', '{customers.Address}', '{customers.City}', '{customers.Region}', '{customers.PostalCode}', '{customers.Country}', '{customers.Phone}', '{customers.Fax}'");

                    if (queryResult > 0)
                    {
                        result.Correct = true;
                        result.Message = "The registry was updated successfully.";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.Message = "An error occurred while updating the registry." + result.Ex;
            }

            return result;
        }

        public static ML.Result Delete(ML.Customers customers)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.NorthwindContext context = new DL.NorthwindContext())
                {
                    int queryResult = context.Database.ExecuteSqlRaw($"CustomersDelete '{customers.CustomerId}'");

                    if (queryResult > 0)
                    {
                        result.Correct = true;
                        result.Message = "The record was deleted successfully.";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.Message = "It is not possible to delete the record because it is connected to another table. First delete the record from the linked table." + result.Ex;
            }

            return result;
        }
    }
}
