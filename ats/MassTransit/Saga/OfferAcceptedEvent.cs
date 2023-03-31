using System;
using ats.Models;
namespace shraredclasses.Events
{
	public class OfferAcceptedEvent 
	{
		public Guid ProcessID { get; set; }
		public Applicant Applicant { get; set; }
	}
}

