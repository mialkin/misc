using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace WebMisc.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrdersController : ControllerBase
    {
        [HttpPost]
        [Route("cancel")]
        public IActionResult Cancel(CancelModel model)
        {
            return Ok(JsonSerializer.Serialize(new CancelResponse { Status = 1 }));
        }
    }
}