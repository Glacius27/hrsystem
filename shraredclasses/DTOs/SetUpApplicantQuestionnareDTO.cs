using System;
using shraredclasses.Models;

namespace shraredclasses.DTOs
{
	public class SetUpApplicantQuestionnareDTO
	{

        public string BirthDate { get; set; }
        public string BirthPlace { get; set; }
        public Document[] Documents { get; set; }
        public Address[] Addresses { get; set; }
        public BankDetails BankDetails { get; set; }
    }



  
}

