using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Controllers.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SamuraisController : ControllerBase
    {
        [HttpGet]
        public Samurai Get()
        {
            Samurai samurai1 = new Samurai();
            {
                Id = 1,Name="abdul"
            };
            return samurai1;
        }
    }
}
