using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiWithSecurity.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/home")]
    public class HomeController : Controller
    {
        [HttpGet("")]
        public IActionResult Index()
        {
            return Json("Yse You Can Access It");
        }

        [HttpGet("secure")]
        [Authorize]
        public IActionResult Secure()
        {
            return Json("Yse You Can Access It");
        }
    }
}