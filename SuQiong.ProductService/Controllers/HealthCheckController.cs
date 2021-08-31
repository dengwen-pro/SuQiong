using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuQiong.ProductService.Controllers
{
    [ApiController]
    public class HealthCheckController : ControllerBase
    {
        [Route("healthCheck")]
        public IActionResult Get()
        {
            return Ok(HttpContext.Request.Host);
        }
    }
}
