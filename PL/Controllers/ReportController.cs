using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ML;
using System.Data;
using System.Text.Json;
using System.Diagnostics;
using PL.Models;
using Newtonsoft.Json;
using System.Text;
using System.IO;

namespace PL.Controllers
{
    public class ReportController : Controller
    {
        [HttpGet]
        public IActionResult Report()
        {
            ML.Result result = BL.Report.ReportJson();

            var jsonReport = JsonConvert.SerializeObject(result);

            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"C:\Users\ALIEN10\Documents\Alexis Jair Del Castillo Castillo\NorthwindCRUD\PL\wwwroot\reports\report.txt");

            try
            {
                System.IO.File.WriteAllText(filePath, jsonReport);

                var fileContent = System.IO.File.ReadAllText(filePath);

                var formattedJson = JsonConvert.SerializeObject(
                    JsonConvert.DeserializeObject(fileContent),
                    Formatting.Indented);;

                return View("ShowReport", formattedJson);
            }
            catch (Exception ex)
            {
                return Content($"Error: {ex.Message}");
            }
        }
    }
}
