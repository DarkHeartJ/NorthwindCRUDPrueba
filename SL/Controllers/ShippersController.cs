using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SL.Controllers
{
    [ApiController]
    [Route("api/shippers")]
    public class ShippersController : ControllerBase
    {
        [HttpGet]
        [Route("getall")]
        public IActionResult GetAll()
        {
            ML.Result result = BL.Shippers.GetAll();

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
        [Route("getbyid/{shipperId}")]
        public IActionResult GetById(int shipperId)
        {
            ML.Shippers shippers = new ML.Shippers();

            ML.Result result = BL.Shippers.GetById(shipperId);

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
        public IActionResult Add([FromBody] ML.Shippers shippers)
        {
            ML.Result result = BL.Shippers.Add(shippers);

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
        [Route("update/{shipperId}")]
        public IActionResult Update(int shipperId, [FromBody] ML.Shippers shippers)
        {
            ML.Result result = BL.Shippers.Update(shippers);

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
        [Route("delete/{shipperId}")]
        public IActionResult Delete(int shipperId)
        {
            ML.Shippers shippers = new ML.Shippers();
            shippers.ShipperID = Convert.ToInt32(shipperId);
            ML.Result result = BL.Shippers.Delete(shippers);

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
