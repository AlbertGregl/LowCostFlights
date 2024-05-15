using Microsoft.AspNetCore.Mvc;

namespace LowCostFlights.Server.Controllers
{
    [Route("api/")]
    [ApiController]
    public class HomeController : ControllerBase
    {

        [HttpGet]
        public IActionResult Get()
        {
            return File("home.html", "text/html");
        }
    }
}
