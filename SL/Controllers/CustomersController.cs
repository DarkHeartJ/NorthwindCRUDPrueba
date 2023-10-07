using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SL.Controllers
{
    [ApiController]
    [Route("api/customers")]
    public class CustomersController : ControllerBase
    {
        [HttpGet]
        [Route("getbycustomerid/{customerId}")]
        public IActionResult GetByCustomerId(string customerId)
        {
            ML.Customers customers = new ML.Customers();

            ML.Result result = BL.Customers.GetByCustomerId(customerId);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound(result);
            }
        }

        [HttpGet]
        [Route("getall")]
        public IActionResult GetAll()
        {
            ML.Result result = BL.Customers.GetAll();

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound(result);
            }
        }

        [HttpGet]
        [Route("getbyid/{customerId}")]
        public IActionResult GetById(string customerId)
        {
            ML.Customers customers = new ML.Customers();

            ML.Result result = BL.Customers.GetById(customerId);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound(result);
            }
        }

        [HttpPost]
        [Route("add")]
        public IActionResult Add([FromBody] ML.Customers customers)
        {
            ML.Result result = BL.Customers.Add(customers);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPut]
        [Route("update/{customerId}")]
        public IActionResult Update(string customerId, [FromBody] ML.Customers customers)
        {
            ML.Result result = BL.Customers.Update(customers);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpDelete]
        [Route("delete/{customerId}")]
        public IActionResult Delete(string customerId)
        {
            ML.Customers customers = new ML.Customers();
            customers.CustomerId = customerId;
            ML.Result result = BL.Customers.Delete(customers);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound(result);
            }
        }
    }
}
