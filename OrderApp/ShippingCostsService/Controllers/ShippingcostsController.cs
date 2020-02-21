using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ShippingCostsService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ShippingcostsController : ControllerBase
    {
        // GET: Shippingcosts
        [HttpGet]
        public async Task<ActionResult<double>> Get()
        {
            var r = new Random().Next(5);
            if (r == 1)
            {
                //disabled throwing an error
                //return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

            //return fake shipping costs from 0 to 4 $
            return r;
        }
    }
}
