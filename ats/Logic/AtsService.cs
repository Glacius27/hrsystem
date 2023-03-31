using System;
using ats.Controllers;
using ats.Models;
using shraredclasses.DTO;
using ats.DB;
using MassTransit;
using shraredclasses.Commands;
using shraredclasses.DTOs;
using static System.Runtime.InteropServices.JavaScript.JSType;
using shraredclasses.Events;

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

        public async Task<Applicant> CreateApplicantID()
        {
            var applicant = new Applicant();
            var result = _dataBaseService.Create(applicant);
            return result;

        }

        public async Task<bool> CreateApplicant(string applicantId, CreateApplicantDTO createApplicantDTO)
        {
            var vacancyResponse = _dataBaseService.FindVacancyResponse(createApplicantDTO.VacancyResponseID);
            var updateresult = _dataBaseService.AddEmailToApplicant(applicantId, vacancyResponse);
            updateresult = _dataBaseService.AddVacancyIdToApplicant(applicantId, createApplicantDTO.VacancyID);
            updateresult = _dataBaseService.AddPersonalDataToApplicant(applicantId, vacancyResponse);
            Uri uri = new Uri("rabbitmq://localhost/createuser");
            var endPoint = await _bus.GetSendEndpoint(uri);
            var guid = Guid.NewGuid().ToString();

            var createUserRequest = new CreateUser() { CorrelationID = guid, Email = vacancyResponse.Email, FirstName = vacancyResponse.FirstName, LastName = vacancyResponse.LastName, VacancyId = createApplicantDTO.VacancyID };
            var result = _dataBaseService.Create(createUserRequest);

            await endPoint.Send(createUserRequest);
            return true;
    
        }


        public async Task<bool> SetUserIdToApplicant(CreateUserResponse createUserResponse)
        {
            var result = _dataBaseService.AddUserIdToApplicant(createUserResponse);
            var deleteresult = _dataBaseService.DeleteCreateUser(createUserResponse.CorrelationID);

            Uri uri = new Uri("rabbitmq://localhost/notification");
            var endPoint = await _bus.GetSendEndpoint(uri);
            await endPoint.Send(new CreateNotification() { UserID = createUserResponse.UserID, PositionName = "Lead Developer", Email = createUserResponse.Email, NotificationType = NotificationType.Register });
            return true;
        }

        public async Task<bool> SetUpQuestionnare(string applicantId, SetUpApplicantQuestionnareDTO setUpApplicantQuestionnare )
        {
			var result = _dataBaseService.AddApplicantQuestionnare(applicantId, setUpApplicantQuestionnare);
			return true;
        }


        public async Task<Applicant> GetApplicant (string applicantId)
        {
            var result = _dataBaseService.FindApplicant(applicantId);
            return result;
        }

        public async Task<JobOffer> CreateOffer(string applicantId, CreateJobOfferDTO createJobOfferDTO)
        {
            var result = _dataBaseService.UpdateOffer(applicantId, createJobOfferDTO);
            return result;
        }


        public async Task ChangeOfferState(string applicantId, string jobOfferID, JobOfferStatus jobOfferStatus)
        {
            var result = _dataBaseService.ChangeOfferState(applicantId, jobOfferID, jobOfferStatus);
            if(jobOfferStatus == JobOfferStatus.accepted)
            {
                var applicant = _dataBaseService.FindApplicant(applicantId);
                Uri uri = new Uri("rabbitmq://localhost/createEmployee");
                var endPoint = await _bus.GetSendEndpoint(uri);
                var i = new CreateEmployeeRequest()
                {
                    ApplicantID = applicant.ID,
                    CorrelationID = Guid.NewGuid().ToString(),
                    FirstName = applicant.FirstName,
                    LastName = applicant.LastName,
                    VacancyID = applicant.VacancyID
                };


                await endPoint.Send(i);
            }
            if (jobOfferStatus == JobOfferStatus.refused)
                return;
        }

        public async Task ApplyEmployee(CreateEmployeeResponse createEmployeeResponse)
        {
            var applicant = _dataBaseService.FindApplicant(createEmployeeResponse.ApplicantID);

            var bankDetails = new CreateBankDetailsRequest()
            {
                BankAccount = applicant.Questionare.BankDetails.BankAccount,
                BankName = applicant.Questionare.BankDetails.BankName,
                BankNumber = applicant.Questionare.BankDetails.BankNumber,
                EmployeeId = createEmployeeResponse.EmployeeID
            };

            Uri uri = new Uri("rabbitmq://localhost/createBankDetails");
            var endPoint = await _bus.GetSendEndpoint(uri);
            await endPoint.Send(bankDetails);

            var learning = new CreateLearningTrackRequest()
            {
                EmployeeId = createEmployeeResponse.EmployeeID
            };

            Uri uri1 = new Uri("rabbitmq://localhost/createLearningCourse");
            var endPoint1 = await _bus.GetSendEndpoint(uri1);
            await endPoint1.Send(learning);

            Uri uri2 = new Uri("rabbitmq://localhost/notification");
            var endPoint2 = await _bus.GetSendEndpoint(uri2);
            await endPoint2.Send(new CreateNotification() { UserID = applicant.UserID, Email = applicant.Email, NotificationType = NotificationType.Greetings });
        }
    }
}

