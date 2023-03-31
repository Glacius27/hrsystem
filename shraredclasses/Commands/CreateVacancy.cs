using System;


namespace shraredclasses.Commands
{
	public class CreateVacancy
	{
		public string PositionID { get; set; }
		public string City { get; set; }
        public string PositionName { get; set; }
		public string CorrelationID { get; set; }
    }

    public class CreateVacancyResponse
    {
        public string CorrelationID { get; set; }
        public string VacancyID { get; set; }
    }
}

