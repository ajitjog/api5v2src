using CoreApi5V2.Model;
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
    public class CitiesController : ControllerBase
    {
        [HttpGet()]
        public IActionResult GetAll()
        {
            return Ok(CitiesRepository.GetAll());
        }


        [HttpGet("{ctno}")]
        public IActionResult Get(int ctno)
        {
            var c = CitiesRepository.Get(ctno);
            if (c != null)
            {
                Utilities.KafkaUtility.Produce("kubetopic", c);
                return Ok(c);
            }
            else
                return NotFound("No Such City");
        }
        [HttpPost("{ct}")]
        public IActionResult NewCity(City ct)
        {
            CitiesRepository.Add(ct);
            return Ok(ct);
        }


    }
}
