using System;
using Newtonsoft.Json;

namespace shraredclasses.Models
{
        public class WsResponse
        {
            
            [JsonProperty("data")]
            public object Data { get; set; }

            [JsonProperty("message")]
            public string Message { get; set; }
        }
}

