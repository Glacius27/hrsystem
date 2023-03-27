using System;
using ats.Controllers;
using ats.Models;
using shraredclasses.DTO;
using ats.DB;

namespace ats.Logic
{
	public class AtsService
	{
        private readonly ILogger<AtsService> _logger;
        private DataBaseService _dataBaseService;
        public AtsService(ILogger<AtsService> logger, DataBaseService dataBaseService)
        {
            _logger = logger;
            _dataBaseService = dataBaseService;
        }


        public bool RegisterVacancyResponse(string vacancyId, VacancyResponse vacancyResponse)
		{
			var response = _dataBaseService.Update(vacancyResponse);
			var result = _dataBaseService.Update(vacancyId, vacancyResponse.VacancyResponseID);


			return true;
		}
		public string CreateVacancy(Vacancy vacancy)
		{
			var vacation = _dataBaseService.Create(vacancy);
			return vacation.VacancyID;
		}
		public string CreateVacancyResponse()
		{
			var response = _dataBaseService.Create(new VacancyResponse());
			return response.VacancyResponseID;

		}
		public void RegisterVacancyResponse()
		{ }

	}
}

