using System;
using Newtonsoft.Json;

namespace shraredclasses.DTO
{
	public class VacancyRequestDTO
	{
            [JsonProperty("PositionId")]
            public string PositionId { get; set; }
            [JsonProperty("VacancyName")]
            public string VacancyName { get; set; }
            [JsonProperty("City")]
            public string City { get; set; }
        
    }
}

