using Microsoft.AspNetCore.Mvc;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace PL.Controllers
{
    public class ShippersController : Controller
    {
        private IHostingEnvironment environment;
        private IConfiguration configuration;
        public ShippersController(IHostingEnvironment _environment, IConfiguration _configuration)
        {
            environment = _environment;
            configuration = _configuration;
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Shippers shippers = new ML.Shippers();

            ML.Result resultShipper = new ML.Result();
            resultShipper.Objects = new List<Object>();

            using (HttpClient client = new HttpClient())
            {
                string webApi = configuration["WebApi"];
                client.BaseAddress = new Uri(webApi);

                var responseTask = client.GetAsync("shippers/getall");
                responseTask.Wait();

                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<ML.Result>();
                    readTask.Wait();

                    foreach (var resultItem in readTask.Result.Objects)
                    {
                        ML.Shippers resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Shippers>(resultItem.ToString());
                        resultShipper.Objects.Add(resultItemList);
                    }
                }

                shippers.Shipperss = resultShipper.Objects;
            }
            return View(shippers);
        }

        [HttpGet]
        public ActionResult Form(int? ShipperId)
        {
            ML.Shippers shippers = new ML.Shippers();

            if (ShipperId == null)
            {
                ViewBag.Titulo = "Register a new Shipper";
                ViewBag.Accion = "Add";

                return View(shippers);
            }
            else
            {
                ViewBag.Titulo = "Modify Customer Information";
                ViewBag.Accion = "Update";

                ML.Result result = BL.Shippers.GetById(ShipperId.Value);

                if (result.Object != null)
                {
                    shippers = (ML.Shippers)result.Object;
                    return View(shippers);
                }
                else
                {
                    ViewBag.Titulo = "Error";
                    ViewBag.Message = result.Message;
                    return View("Modal");
                }

            }
        }

        [HttpPost]
        public ActionResult Form(ML.Shippers shippers)
        {
                if (shippers.ShipperID == 0)
                {
                    ML.Result result = new ML.Result();

                    using (HttpClient client = new HttpClient())
                    {
                        string webApi = configuration["WebApi"];
                        client.BaseAddress = new Uri(webApi);

                        Task<HttpResponseMessage> postTask = client.PostAsJsonAsync<ML.Shippers>("shippers/add", shippers);
                        postTask.Wait();

                        HttpResponseMessage resultTask = postTask.Result;
                        if (resultTask.IsSuccessStatusCode)
                        {
                            result.Correct = true;
                            ViewBag.Titulo = "The record was inserted correctly.";
                            ViewBag.Message = result.Message;
                            return View("Modal");
                        }
                        else
                        {
                            ViewBag.Titulo = "An error occurred while inserting the record.";
                            ViewBag.Message = result.Message;
                            return View("Modal");
                        }
                    }

                }
                else
                {
                    ML.Result result = new ML.Result();

                    using (HttpClient client = new HttpClient())
                    {
                        string webApi = configuration["WebApi"];
                        client.BaseAddress = new Uri(webApi);

                        Task<HttpResponseMessage> postTask = client.PutAsJsonAsync<ML.Shippers>("shippers/update/" + shippers.ShipperID, shippers);
                        postTask.Wait();

                        HttpResponseMessage resultTask = postTask.Result;
                        if (resultTask.IsSuccessStatusCode)
                        {
                            result.Correct = true;
                            ViewBag.Titulo = "The registry was updated successfully.";
                            ViewBag.Message = result.Message;
                            return View("Modal");
                        }
                        else
                        {
                            ViewBag.Titulo = "An error occurred while updating the registry.";
                            ViewBag.Message = result.Message;
                            return View("Modal");
                        }
                    }
                }

            return View("GetAll");
        }

        [HttpGet]
        public ActionResult Delete(ML.Shippers shippers)
        {
            ML.Result resultShipper = new ML.Result();
            int shipperId = shippers.ShipperID;

            using (HttpClient client = new HttpClient())
            {
                string webApi = configuration["WebApi"];
                client.BaseAddress = new Uri(webApi);

                var responseTask = client.DeleteAsync("shippers/delete/" + shipperId);
                responseTask.Wait();

                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    ViewBag.Titulo = "The record was deleted successfully.";
                    return View("Modal");
                }
                else
                {
                    ViewBag.Titulo = "An error occurred while deleting the record.";
                    return View("Modal");
                }
            }
            return View("Modal");
        }
    }
}
