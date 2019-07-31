using Microsoft.AspNetCore.Mvc;
using System.Threading;

namespace Classifieds.App.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ErrorController : ControllerBase
    {
        [HttpGet("{id}")]
        public int ErrorCode(int id)
        {
            Response.StatusCode = id;
            return Response.StatusCode;
        }
        [HttpGet("delay/")]
        public void Delay()
        {
            Thread.Sleep(100000);
        }
    }
}
