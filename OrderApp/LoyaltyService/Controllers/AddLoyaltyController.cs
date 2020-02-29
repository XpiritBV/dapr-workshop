using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderApp.Model;

namespace LoyaltyService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AddLoyaltyController : ControllerBase
    {
        // POST: Loyalty
        [HttpPost()]
        public async Task<ActionResult<bool>> AddLoyalty(Order order)
        {
            await Task.Delay(new Random().Next(2000));
            return true;
        }
    }
}