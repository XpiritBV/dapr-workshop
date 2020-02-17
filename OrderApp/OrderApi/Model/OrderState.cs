using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderApi.Model
{
    public class OrderState
    {
        [JsonProperty("key")]
        public string Key { get; set; }

        [JsonProperty("value")]
        public Order Value { get; set; }
    }
}
