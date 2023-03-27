using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ats.Models
{
	public class JobOffer
	{
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string JobOfferID { get; set; }
		public string PositionName { get; set; }
		public string PositionDescription { get; set; }
		public decimal Salary { get; set; }
	}
}

