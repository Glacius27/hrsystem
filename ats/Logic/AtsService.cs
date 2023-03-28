using System;
using ats.Controllers;
using ats.Models;
using shraredclasses.DTO;
using ats.DB;
using MassTransit;
using shraredclasses.Commands;
using shraredclasses.DTOs;

namespace ats.Logic
{
	public class AtsService
	{
        private readonly ILogger<AtsService> _logger;
        private DataBaseService _dataBaseService;
        private readonly IBus _bus;


		public AtsService(ILogger<AtsService> logger, DataBaseService dataBaseService, IBus bus)
        {
            _logger = logger;
            _dataBaseService = dataBaseService;
			_bus = bus;
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
		
        public async Task<bool> InviteToInterview(string vacancyID, string vacancyResponseID)
        {
            var data = _dataBaseService.FindVacancyResponse(vacancyResponseID);

            Uri uri = new Uri("rabbitmq://localhost/notification");
            var endPoint = await _bus.GetSendEndpoint(uri);
			await endPoint.Send(new CreateNotification() { PositionName = "Lead Developer", Email = data.Email, NotificationType = NotificationType.Interview });
			return true;
        }

        public async Task<bool> ChooseApplicant(string vacancyID, string vacancyResponseID)
        {
			var data = _dataBaseService.FindVacancyResponse(vacancyResponseID);
			Uri uri = new Uri("rabbitmq://localhost/createuser");
			var endPoint = await _bus.GetSendEndpoint(uri);
			var guid = Guid.NewGuid().ToString();

			var createUserRequest = new CreateUser() { CorrelationID = guid, Email = data.Email, FirstName = data.FirstName, LastName = data.LastName, VacancyId = vacancyID };
			var result = _dataBaseService.Create(createUserRequest);

            await endPoint.Send(createUserRequest);
			return true;
        }

		public async Task<bool> CreateApplicant(CreateUserResponse createUserResponse)
		{
			var applicant = new Applicant(createUserResponse);
			var result = _dataBaseService.Create(applicant);

			var deleteresult = _dataBaseService.DeleteCreateUser(createUserResponse.CorrelationID);

            Uri uri = new Uri("rabbitmq://localhost/notification");
            var endPoint = await _bus.GetSendEndpoint(uri);
            await endPoint.Send(new CreateNotification() { UserID = applicant.UserID, PositionName = "Lead Developer", Email = applicant.Email, NotificationType = NotificationType.Register });
            return true;

        }

        public async Task<bool> SetUpQuestionnare(string applicantId, SetUpApplicantQuestionnareDTO setUpApplicantQuestionnare )
        {
			var result = _dataBaseService.AddApplicantQuestionnare(applicantId, setUpApplicantQuestionnare);
			return true;
        }
    }
}

