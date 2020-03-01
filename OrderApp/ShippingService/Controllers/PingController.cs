using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OrderApi.Controllers
{
    [ApiController]
    public class PingController : ControllerBase
    {
        [Route("pingservice")]
        [HttpGet]
        public string Get()
        {
            return "Everything OK!";
        }


    }
}
