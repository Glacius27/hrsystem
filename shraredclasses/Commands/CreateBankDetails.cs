using System;
namespace shraredclasses.Commands
{
	public class CreateBankDetailsRequest
	{
			public string EmployeeId { get; set; }
			public string BankName { get; set; }
			public string BankNumber { get; set; }
			public string BankAccount { get; set; }	
	}

    public class CreateBankDetailsResponse
    {
       
    }
}

