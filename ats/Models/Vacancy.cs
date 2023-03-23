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
        [JsonProperty("UserID")]
        public string VacancyID { get; set; }
        public string VacancyName { get; set; }
        public string City { get; set; }
        public Applicant[] Applicants { get; set; } 
    }
}

