using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using shraredclasses.Commands;

namespace ats.Models
{
	public class Vacancy
	{
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [JsonProperty("VacancyID")]
        public string VacancyID { get; set; }
        [JsonProperty("PositionName")]
        public string PositionName { get; set; }
        [JsonProperty("PositionID")]
        public string PositionID { get; set; }
        [JsonProperty("City")]
        public string City { get; set; }
        [JsonProperty("VacancyResponses")]
        public List<string> VacancyResponses { get; set; }

        public Vacancy() { }
        public Vacancy(CreateVacancy createVacancy)
        {
            this.PositionID = createVacancy.PositionID;
            this.City = createVacancy.City;
            this.PositionName = createVacancy.PositionName;
            this.VacancyResponses = new List<string>();
        }
    }
}

