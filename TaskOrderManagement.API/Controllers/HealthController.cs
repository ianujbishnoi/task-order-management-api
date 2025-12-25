using Microsoft.AspNetCore.Mvc;

namespace TaskOrderManagement.API.Controllers
{
    [ApiController]
    [Route("api/health")]
    public class HealthController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return Ok("API is running.");
        }
    }
}
