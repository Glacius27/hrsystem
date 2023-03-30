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

  //      public JobOffer(CreateJobOfferDTO createJobOfferDTO)
		//{
		//	this.PositionName = createJobOfferDTO.PositionName;
		//	this.PositionDescription = createJobOfferDTO.PositionDescription;
		//	this.Salary = createJobOfferDTO.Salary;
		//	this.JobOfferStatus = JobOfferStatus.pending;
		//}
    }

	 

	public enum JobOfferStatus
	{
		none,
		pending,
		accepted,
		refused
	}

}

