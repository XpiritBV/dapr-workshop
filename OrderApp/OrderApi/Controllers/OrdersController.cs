using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderApi.Model;
using Newtonsoft.Json;
using OrderApp.Model;

namespace OrderApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private string _daprPort;
        private string _stateStoreUrl;
        private string _stateStoreName = "statestore";
        HttpClient _stateClient;

        public OrdersController()
        {
            _daprPort = Environment.GetEnvironmentVariable("DAPR_HTTP_PORT");
            _stateStoreUrl = $"http://localhost:{_daprPort}/v1.0/state/{_stateStoreName}";

            _stateClient = new HttpClient();
        }

        // GET: api/Orders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            try
            {
                var order = JsonConvert.DeserializeObject<Order>(await _stateClient.GetStringAsync($"{ _stateStoreUrl}/order-{id}"));


                if (order == null)
                {
                    Console.WriteLine($"Order with {id} not found.");
                    return NotFound();
                }

                return order;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error retrieving order with id {id}, Error: {ex}. \n{ex.StackTrace}");
                return new StatusCodeResult(500);
            }
        }


        // PUT: api/Orders/id
        [HttpPut("{id}")]
        public async Task<ActionResult<Order>> PutOrder(int id, Order order)
        {
            if (id != order.Id)
            {
                return BadRequest("Id's do not match");
            }

            OrderState state = new OrderState();
            state.Key = $"order-{id}";

            state.Value = order;
            List<OrderState> stateList = new List<OrderState>();
            stateList.Add(state);

            var result = await _stateClient.PostAsJsonAsync(_stateStoreUrl, stateList);

            return order;
        }

        // POST: api/Orders
        [HttpPost()]
        public async Task<ActionResult<Order>> PostOrder(Order order)
        {
            OrderState state = new OrderState();
            state.Key = $"order-{order.Id}";

            state.Value = order;
            List<OrderState> stateList = new List<OrderState>();
            stateList.Add(state);

            var result = await _stateClient.PostAsJsonAsync(_stateStoreUrl, stateList);

            return order;
        }
    }
}