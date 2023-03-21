using System;
namespace HRIS.models
{
	public class CreateEmployeeRequestDTO
	{
        public string PositionID { get; set; }
        public decimal Salary { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string DateofBirth { get; set; }
    }
}

