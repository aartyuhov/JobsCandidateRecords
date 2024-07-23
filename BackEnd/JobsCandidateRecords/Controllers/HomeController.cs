using Microsoft.AspNetCore.Mvc;

namespace JobsCandidateRecords.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        /// <summary>
        /// Get.
        /// </summary>
        [HttpGet(Name = "Home")]
        public string Get()
        {
            return "Home page";
        }
    }
}
