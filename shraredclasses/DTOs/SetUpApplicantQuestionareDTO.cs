using System;
using shraredclasses.Models;

namespace shraredclasses.DTOs
{
	public class SetUpApplicantQuestionareDTO
	{
    
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string BirthDate { get; set; }
        public string BirthPlace { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public Document[] Documents { get; set; }
        public Address[] Addresses { get; set; }
        public BankDetails BankDetails { get; set; }
    }



  
}

