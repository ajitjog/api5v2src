using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreApi5V2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnvController : ControllerBase
    {
        [HttpGet("{envname}")]
        public IActionResult Get(string envname)
        {
            var hname = Environment.MachineName;
            return Ok($"Hello World to {envname} from {hname} from V2 !!!");
        }

        [HttpGet("envval/{envname}")]
        public IActionResult GetEnv(string envname)
        {

            var evalue = Environment.GetEnvironmentVariable(envname);
            return Ok($"Value of env {envname} is : {evalue}");
        }

    }
}
