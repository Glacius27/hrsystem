using System;
namespace shraredclasses.Commands
{
	public class CreateEmployeeRequest
	{
        public string CorrelationID { get; set; }
        public string VacancyID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ApplicantID { get; set; }
    }


    public class CreateEmployeeResponse
    {
        public string CorrelationID { get; set; }
        public string EmployeeID { get; set; }
        public string ApplicantID { get; set; }
    }
}


