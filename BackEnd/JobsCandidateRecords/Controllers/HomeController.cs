using JobsCandidateRecords.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

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

        [HttpGet(Name = "Home")]
        public string Get()
        {
            return "Home page";
        }
    }
}
