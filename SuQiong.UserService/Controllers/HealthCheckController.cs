using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuQiong.UserService.Controllers
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
