using HelloWebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace HelloWebApi.Controllers
{
    [ApiController]
    [Route("home")]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetMessage()
        {
            var result= new ResponseModel()
            {
                HttpStatus = 200,
                Message = "Hello World get message"
            };

            return Ok(result);
            //return BadRequest(result);
            //return NotFound(result);
        }
    }
}
