﻿using System;
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
        private string _loyaltyUrl;
        private string _shippingCostsServiceUrl;
        private string _stateStoreName = "statestore";
        private string _shippingPublishMessageUrl;
        HttpClient _httpClient;

        public OrdersController()
        {
            _daprPort = Environment.GetEnvironmentVariable("DAPR_HTTP_PORT");
            _stateStoreName = Environment.GetEnvironmentVariable("DAPR_STATESTORE");
            _stateStoreUrl = $"http://localhost:{_daprPort}/v1.0/state/{_stateStoreName}";

            _loyaltyUrl = $"http://localhost:{_daprPort}/v1.0/invoke/loyaltyservice/method/addloyalty";
            _shippingCostsServiceUrl = $"http://localhost:{_daprPort}/v1.0/invoke/shippingcostsservice/method/shippingcosts";

            _shippingPublishMessageUrl = $"http://localhost:{_daprPort}/v1.0/publish/ShipOrders";

            _httpClient = new HttpClient();
        }

        // GET: Orders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            try
            {
                var order = JsonConvert.DeserializeObject<Order>(await _httpClient.GetStringAsync($"{ _stateStoreUrl}/order-{id}"));


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


        // PUT: Orders/id
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

            if (order.LoyaltyBonus)
            {
                //call loyalty service to add loyalty bonus
                var loyalty = await _httpClient.PostAsJsonAsync(_loyaltyUrl, order);
            }

            var shippingCostResult = await _httpClient.GetStringAsync($"{_shippingCostsServiceUrl}/{order.Id}");
            order.ShippingCosts = double.Parse(shippingCostResult);

            var result = await _httpClient.PostAsJsonAsync(_stateStoreUrl, stateList);


            var publishResult = await _httpClient.PostAsJsonAsync(_shippingPublishMessageUrl, order);

            return order;
        }
    }
}