using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using shraredclasses.DTOs;

namespace ats.Models
{
	public class Applicant
	{
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ID { get; set; }
		public string UserID { get; set; }
		public string VacancyID { get; set; }
		public Questoinare Questionare { get; set; }
		public JobOffer JobOffer { get; set; }
		public ApplicantStatus ApplicantStatus { get; set; }

		public Applicant()
		{
			this.Questionare = new Questoinare();
			this.JobOffer = new JobOffer();
		}
	}


	public class Questoinare : SetUpApplicantQuestionareDTO { }
	

	public enum ApplicantStatus
	{
		Registred,
		Accepted,
		Refused
	}
}

