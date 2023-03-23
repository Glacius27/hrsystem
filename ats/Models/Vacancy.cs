using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace ats.Models
{
	public class Vacancy
	{
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [JsonProperty("VacancyID")]
        public string VacancyID { get; set; }
        [JsonProperty("VacancyName")]
        public string VacancyName { get; set; }
        [JsonProperty("City")]
        public string City { get; set; }
        [JsonProperty("VacancyResponses")]
        public VacancyResponse[] VacancyResponses { get; set; } 
    }
}

