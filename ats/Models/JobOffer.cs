using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using shraredclasses.DTOs;

namespace ats.Models
{
	public class JobOffer
	{
        public string JobOfferID { get; set; }
		public string PositionName { get; set; }
		public string PositionDescription { get; set; }
		public decimal Salary { get; set; }
		public JobOfferStatus JobOfferStatus { get; set; }
    }

	 

	public enum JobOfferStatus
	{
		none,
		pending,
		accepted,
		refused
	}

}

