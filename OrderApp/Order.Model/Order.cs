using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderApp.Model
{
    public class Order
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("customerId")]
        public string CustomerId { get; set; }

        [JsonProperty("shippingDate")]
        public DateTime ShippingDate { get; set; }

        public bool LoyaltyBonus { get; set; }

        public double ShippingCosts { get; set; }
    }
}
