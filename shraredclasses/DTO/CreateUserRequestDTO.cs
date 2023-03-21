using System;
using Newtonsoft.Json;
namespace shraredclasses.DTO
{
    public class CreateUserRequestDTO
    {
        [JsonProperty("FirstName")]
        public string FirstName { get; set; }
        [JsonProperty("lastname")]
        public string LastName { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
    }
}

