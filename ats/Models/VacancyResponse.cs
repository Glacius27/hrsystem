using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace ats.Models
{
	public class VacancyResponse
	{
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [JsonProperty("VacancyResponseID")]
        public string VacancyResponseID { get; set; }
        [JsonProperty("FirstName")]
        public string FirstName { get; set; }
        [JsonProperty("LastName")]
        public string LastName { get; set; }
        [JsonProperty("Email")]
        public string Email { get; set; }
        [JsonProperty("Phone")]
        public string Phone { get; set; }
        [JsonProperty("CvUrl")]
        public string CvUrl { get; set; }
    }
}

