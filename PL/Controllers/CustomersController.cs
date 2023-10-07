using Microsoft.AspNetCore.Mvc;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace PL.Controllers
{
    public class CustomersController : Controller
    {
        private IHostingEnvironment environment;
        private IConfiguration configuration;
        public CustomersController(IHostingEnvironment _environment, IConfiguration _configuration)
        {
            environment = _environment;
            configuration = _configuration;
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string customerId, string contactName)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string webApi = configuration["WebApi"];
                    client.BaseAddress = new Uri(webApi);

                    var responseTask = client.GetAsync("customers/getbycustomerid/" + customerId);
                    responseTask.Wait();

                    var resultLogin = responseTask.Result;

                    if (resultLogin.IsSuccessStatusCode)
                    {
                        var readTask = resultLogin.Content.ReadAsAsync<ML.Result>();
                        readTask.Wait();

                        ML.Customers customers = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Customers>(readTask.Result.Object.ToString());

                        if (contactName == customers.ContactName)
                        {
                            return View("../Home/Index");
                        }
                        else
                        {
                            ViewBag.Message = "Id or name is incorrect. Try again.";
                            return PartialView("ModalLogin");
                        }
                    }
                    else
                    {
                        
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return PartialView("ModalLogin");
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Customers customers = new ML.Customers();

            ML.Result resultCustomer = new ML.Result();
            resultCustomer.Objects = new List<Object>();

            using (HttpClient client = new HttpClient())
            {
                string webApi = configuration["WebApi"];
                client.BaseAddress = new Uri(webApi);

                var responseTask = client.GetAsync("customers/getall");
                responseTask.Wait();

                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<ML.Result>();
                    readTask.Wait();

                    foreach (var resultItem in readTask.Result.Objects)
                    {
                        ML.Customers resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Customers>(resultItem.ToString());
                        resultCustomer.Objects.Add(resultItemList);
                    }
                }

                customers.Customerss = resultCustomer.Objects;
            }
            return View(customers);
        }

        [HttpGet]
        public ActionResult Form(string? CustomerId)
        {
            ML.Customers customers = new ML.Customers();

            if (CustomerId == null)
            {
                ViewBag.Titulo = "Register a new Customer";
                ViewBag.Accion = "Add";
                customers.Action = "Add";

                return View(customers);
            }
            else
            {
                ViewBag.Titulo = "Modify Customer Information";
                ViewBag.Accion = "Update";
                

                ML.Result result = BL.Customers.GetById(CustomerId);

                if (result.Object != null)
                {
                    customers = (ML.Customers)result.Object;
                    customers.Action = "Update";
                    return View(customers);
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
        public ActionResult Form(ML.Customers customers)
        {
            if( customers.Action == "Add")
            {
                ML.Result result = new ML.Result();

                using (HttpClient client = new HttpClient())
                {
                    string webApi = configuration["WebApi"];
                    client.BaseAddress = new Uri(webApi);

                    Task<HttpResponseMessage> postTask = client.PostAsJsonAsync<ML.Customers>("customers/add", customers);
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

            if (customers.Action == "Update")
            {
                ML.Result result = new ML.Result();

                using (HttpClient client = new HttpClient())
                {
                    string webApi = configuration["WebApi"];
                    client.BaseAddress = new Uri(webApi);

                    Task<HttpResponseMessage> postTask = client.PutAsJsonAsync<ML.Customers>("customers/update/" + customers.CustomerId, customers);
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
        public ActionResult Delete(ML.Customers customers)
        {
            ML.Result resultCustomer = new ML.Result();
            string customerId = customers.CustomerId;

            using (HttpClient client = new HttpClient())
            {
                string webApi = configuration["WebApi"];
                client.BaseAddress = new Uri(webApi);

                var responseTask = client.DeleteAsync("customers/delete/" + customerId);
                responseTask.Wait();

                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    ViewBag.Titulo = "The record was deleted successfully.";
                    return View("Modal");
                }
                else
                {
                    ViewBag.Titulo = "It is not possible to delete the record because it is connected to another table. First delete the record from the linked table.";
                    return View("Modal");
                }
            }
            return View("Modal");
        }
    }
}
