using Microsoft.AspNetCore.Mvc;

namespace PaymentGateway.WebApi.Controllers
{
    // http://localhost:5000/api/Home/GetHello
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        [Route("GetHello")]
        public string GetMessage()
        {
            return "Hello";
        }

        [HttpGet]
        [Route("Index")]
        public string Index()
        {
            return "Index";
        }
    }
}