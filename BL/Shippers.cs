using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Shippers
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.NorthwindContext context = new DL.NorthwindContext())
                {
                    var query = context.Shippers.FromSqlRaw("ShippersGetAll").ToList();

                    if (query.Count > 0)
                    {
                        result.Objects = new List<object>();

                        foreach (DL.Shipper resultShipper in query)
                        {
                            ML.Shippers shippers = new ML.Shippers();
                            shippers.ShipperID = resultShipper.ShipperId;
                            shippers.CompanyName = resultShipper.CompanyName;
                            shippers.Phone = resultShipper.Phone;

                            result.Objects.Add(shippers);
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

        public static ML.Result GetById(int ShipperId)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.NorthwindContext context = new DL.NorthwindContext())
                {
                    var query = context.Shippers.FromSqlRaw($"ShippersGetById {ShipperId}").AsEnumerable().FirstOrDefault();

                    if (query != null)
                    {
                        ML.Shippers shippers = new ML.Shippers();;
                        shippers.CompanyName = query.CompanyName;
                        shippers.Phone = query.Phone;

                        result.Object = shippers;
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

        public static ML.Result Add(ML.Shippers shippers)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.NorthwindContext context = new DL.NorthwindContext())
                {
                    int queryResult = context.Database.ExecuteSqlRaw($"ShippersAdd '{shippers.CompanyName}', '{shippers.Phone}'");

                    if (queryResult > 0)
                    {
                        result.Correct = true;
                        result.Message = "The record was inserted correctly..";
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

        public static ML.Result Update(ML.Shippers shippers)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.NorthwindContext context = new DL.NorthwindContext())
                {
                    int queryResult = context.Database.ExecuteSqlRaw($"ShippersUpdate {shippers.ShipperID}, '{shippers.CompanyName}', '{shippers.Phone}'");

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

        public static ML.Result Delete(ML.Shippers shippers)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.NorthwindContext context = new DL.NorthwindContext())
                {
                    int queryResult = context.Database.ExecuteSqlRaw($"ShippersDelete '{shippers.ShipperID}'");

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
                result.Message = "An error occurred while deleting the record." + result.Ex;
            }

            return result;
        }
    }
}
