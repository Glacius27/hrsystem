using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;


namespace shraredclasses.Commands
{
    public class CreateUser
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ID { get; set; }
        public string CorrelationID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string VacancyId { get; set; }
    }
}

