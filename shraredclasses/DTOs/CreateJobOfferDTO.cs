using System;
namespace shraredclasses.DTOs
{
	public class CreateJobOfferDTO
	{
		public string PositionName { get; set; }
        public string PositionDescription { get; set; }
        public decimal Salary { get; set; }
    }
}

