using System;
using ats.Controllers;
using ats.Models;
using shraredclasses.DTO;
using ats.DB;
using MassTransit;
using shraredclasses.Commands;
using shraredclasses.DTOs;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
		public async Task<bool> CreateVacancy(CreateVacancy createVacancy)
		{
			var vacation = _dataBaseService.Create(new Vacancy(createVacancy));
			var result = vacation.VacancyID;

            Uri uri = new Uri("rabbitmq://localhost/createvacancyresponse");
			var endPoint = await _bus.GetSendEndpoint(uri);
            await endPoint.Send(new CreateVacancyResponse() { CorrelationID = createVacancy.CorrelationID, VacancyID = result });
            return true;

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
        #region
        //     public async Task<bool> ChooseApplicant(string vacancyID, string vacancyResponseID)
        //     {
        //var data = _dataBaseService.FindVacancyResponse(vacancyResponseID);
        //Uri uri = new Uri("rabbitmq://localhost/createuser");
        //var endPoint = await _bus.GetSendEndpoint(uri);
        //var guid = Guid.NewGuid().ToString();

        //var createUserRequest = new CreateUser() { CorrelationID = guid, Email = data.Email, FirstName = data.FirstName, LastName = data.LastName, VacancyId = vacancyID };
        //var result = _dataBaseService.Create(createUserRequest);

        //         await endPoint.Send(createUserRequest);
        //return true;
        //     }

        //public async Task<bool> CreateApplicant(CreateUserResponse createUserResponse)
        //{
        //	var applicant = new Applicant(createUserResponse);
        //	var result = _dataBaseService.Create(applicant);

        //	var deleteresult = _dataBaseService.DeleteCreateUser(createUserResponse.CorrelationID);

        //          Uri uri = new Uri("rabbitmq://localhost/notification");
        //          var endPoint = await _bus.GetSendEndpoint(uri);
        //          await endPoint.Send(new CreateNotification() { UserID = applicant.UserID, PositionName = "Lead Developer", Email = applicant.Email, NotificationType = NotificationType.Register });
        //          return true;

        //      }
        #endregion


        public async Task<Applicant> CreateApplicantID()
        {
            var applicant = new Applicant();
            var result = _dataBaseService.Create(applicant);
            return result;

            #region
            //var deleteresult = _dataBaseService.DeleteCreateUser(createUserResponse.CorrelationID);

            //Uri uri = new Uri("rabbitmq://localhost/notification");
            //var endPoint = await _bus.GetSendEndpoint(uri);
            //await endPoint.Send(new CreateNotification() { UserID = applicant.UserID, PositionName = "Lead Developer", Email = applicant.Email, NotificationType = NotificationType.Register });
            //return true;
            #endregion
        }

        public async Task<bool> CreateApplicant(string applicantId, CreateApplicantDTO createApplicantDTO)
        {
            var vacancyResponse = _dataBaseService.FindVacancyResponse(createApplicantDTO.VacancyResponseID);
            var updateresult = _dataBaseService.AddEmailToApplicant(applicantId, vacancyResponse);
            updateresult = _dataBaseService.AddVacancyIdToApplicant(applicantId, createApplicantDTO.VacancyID);

            Uri uri = new Uri("rabbitmq://localhost/createuser");
            var endPoint = await _bus.GetSendEndpoint(uri);
            var guid = Guid.NewGuid().ToString();

            var createUserRequest = new CreateUser() { CorrelationID = guid, Email = vacancyResponse.Email, FirstName = vacancyResponse.FirstName, LastName = vacancyResponse.LastName, VacancyId = createApplicantDTO.VacancyID };
            var result = _dataBaseService.Create(createUserRequest);

            await endPoint.Send(createUserRequest);
            return true;
            #region
            //var applicant = new Applicant();
            //var result = _dataBaseService.Create(applicant);
            //return result;
            //var deleteresult = _dataBaseService.DeleteCreateUser(createUserResponse.CorrelationID);

            //Uri uri = new Uri("rabbitmq://localhost/notification");
            //var endPoint = await _bus.GetSendEndpoint(uri);
            //await endPoint.Send(new CreateNotification() { UserID = applicant.UserID, PositionName = "Lead Developer", Email = applicant.Email, NotificationType = NotificationType.Register });
            //return true;
            #endregion
        }


        public async Task<bool> SetUserIdToApplicant(CreateUserResponse createUserResponse)
        {
            var result = _dataBaseService.AddUserIdToApplicant(createUserResponse);
            var deleteresult = _dataBaseService.DeleteCreateUser(createUserResponse.CorrelationID);

            Uri uri = new Uri("rabbitmq://localhost/notification");
            var endPoint = await _bus.GetSendEndpoint(uri);
            await endPoint.Send(new CreateNotification() { UserID = createUserResponse.UserID, PositionName = "Lead Developer", Email = createUserResponse.Email, NotificationType = NotificationType.Register });
            return true;

            #region
            //var vacancyResponse = _dataBaseService.FindVacancyResponse(createApplicantDTO.VacancyResponseID);
            //var updateresult = _dataBaseService.AddEmailToApplicant(applicantId, vacancyResponse);
            //updateresult = _dataBaseService.AddVacancyIdToApplicant(applicantId, createApplicantDTO.VacancyID);

            //Uri uri = new Uri("rabbitmq://localhost/createuser");
            //var endPoint = await _bus.GetSendEndpoint(uri);
            //var guid = Guid.NewGuid().ToString();

            //var createUserRequest = new CreateUser() { CorrelationID = guid, Email = vacancyResponse.Email, FirstName = vacancyResponse.FirstName, LastName = vacancyResponse.LastName, VacancyId = createApplicantDTO.VacancyID };
            //var result = _dataBaseService.Create(createUserRequest);

            //await endPoint.Send(createUserRequest);
            //return true;
            
            //var applicant = new Applicant();
            //var result = _dataBaseService.Create(applicant);
            //return result;
            //var deleteresult = _dataBaseService.DeleteCreateUser(createUserResponse.CorrelationID);

            //Uri uri = new Uri("rabbitmq://localhost/notification");
            //var endPoint = await _bus.GetSendEndpoint(uri);
            //await endPoint.Send(new CreateNotification() { UserID = applicant.UserID, PositionName = "Lead Developer", Email = applicant.Email, NotificationType = NotificationType.Register });
            //return true;
            #endregion
        }

        public async Task<bool> SetUpQuestionnare(string applicantId, SetUpApplicantQuestionnareDTO setUpApplicantQuestionnare )
        {
			var result = _dataBaseService.AddApplicantQuestionnare(applicantId, setUpApplicantQuestionnare);
			return true;
        }
    }
}

