﻿using System;
using MongoDB.Driver;
using ats.Models;
using shraredclasses.Commands;
using shraredclasses.DTOs;
using ats.MassTransit.Saga;

namespace ats.DB
{
    public class DataBaseService
    {
        private readonly IMongoCollection<Vacancy> _vacancies;
        private readonly IMongoCollection<VacancyResponse> _vacancyResponses;
        private readonly IMongoCollection<CreateUser> _createUserRequests;
        private readonly IMongoCollection<Applicant> _applicants;
        private readonly IMongoCollection<OnboardingEmployeeSagaState> _sagas;


        public DataBaseService(IDataBaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _vacancies = database.GetCollection<Vacancy>("Vacancies");
            _vacancyResponses = database.GetCollection<VacancyResponse>("VacanciesResponses");
            _createUserRequests = database.GetCollection<CreateUser>("CreateUserRequests");
            _sagas = database.GetCollection<OnboardingEmployeeSagaState>("Sagas");
            _applicants = database.GetCollection<Applicant>("Applicants");
        }

        public Vacancy Create(Vacancy vacancy)
        {
            _vacancies.InsertOne(vacancy);
            return vacancy;
        }

        public VacancyResponse Create(VacancyResponse vacancyResponse)
        {
            _vacancyResponses.InsertOne(vacancyResponse);
            return vacancyResponse;
        }

        public CreateUser Create(CreateUser createUserRequest)
        {
            _createUserRequests.InsertOne(createUserRequest);
            return createUserRequest;
        }

        public OnboardingEmployeeSagaState CreateSaga(OnboardingEmployeeSagaState onboardingEmployeeSagaState)
        {
            _sagas.InsertOne(onboardingEmployeeSagaState);
            return onboardingEmployeeSagaState;
        }

        public UpdateResult UpdateSaga(OnboardingEmployeeSagaState onboardingEmployeeSagaState, string state)
        {
            var update = Builders<OnboardingEmployeeSagaState>.Update
                    .Set(x => x.CurrentState, state);
            var result = _sagas.UpdateOne(x => x.CorrelationId == onboardingEmployeeSagaState.CorrelationId, update);
            return result;
        }


        public Applicant Create(Applicant applicant)
        {
            _applicants.InsertOne(applicant);
            return applicant;
        }

        public DeleteResult DeleteCreateUser(string createUserRequestCorrelationID) =>
            _createUserRequests.DeleteOne(x => x.CorrelationID == createUserRequestCorrelationID);


        public IList<Vacancy> Read() =>
            _vacancies.Find(x => true).ToList();

        public Vacancy Find(string vacancyId) =>
            _vacancies.Find(x => x.VacancyID == vacancyId).SingleOrDefault();

        public Applicant FindApplicant(string applicantId) =>
            _applicants.Find(x => x.ID == applicantId).SingleOrDefault();


        public VacancyResponse FindVacancyResponse(string vacancyResponseId) =>
            _vacancyResponses.Find(x => x.VacancyResponseID == vacancyResponseId).SingleOrDefault();


        public UpdateResult Update(string vacancyId, Vacancy vacancy)
        {
            var update = Builders<Vacancy>.Update
                    .Set(x => x.PositionName, vacancy.PositionName)
                    .Set(x => x.City, vacancy.City);
            var result = _vacancies.UpdateOne(x => x.VacancyID == vacancyId, update);
            return result;
        }

        public UpdateResult AddEmailToApplicant(string applicantId, VacancyResponse vacancyResponse)
        {
            var update = Builders<Applicant>.Update
                    .Set(x => x.Email, vacancyResponse.Email);
            var result = _applicants.UpdateOne(x => x.ID == applicantId, update);
            return result;
        }

        public UpdateResult AddPersonalDataToApplicant(string applicantId, VacancyResponse vacancyResponse)
        {
            var update = Builders<Applicant>.Update
                    .Set(x => x.LastName, vacancyResponse.LastName)
                    .Set(x => x.FirstName, vacancyResponse.FirstName)
                    .Set(x => x.Phone, vacancyResponse.Phone);
            var result = _applicants.UpdateOne(x => x.ID == applicantId, update);
            return result;
        }

        public JobOffer UpdateOffer(string applicantId, CreateJobOfferDTO createJobOfferDTO)
        {
            var update = Builders<Applicant>.Update
                    .Set(x => x.JobOffer.PositionDescription, createJobOfferDTO.PositionDescription)
                    .Set(x => x.JobOffer.PositionName, createJobOfferDTO.PositionName)
                    .Set(x => x.JobOffer.Salary, createJobOfferDTO.Salary)
                    .Set(x => x.JobOffer.JobOfferStatus, JobOfferStatus.pending)
                    .Set(x => x.JobOffer.JobOfferID, Guid.NewGuid().ToString());
            var result = _applicants.UpdateOne(x => x.ID == applicantId, update);
            var applicant = _applicants.Find(x => x.ID == applicantId).SingleOrDefault();
            return applicant.JobOffer;
        }

        public UpdateResult ChangeOfferState(string applicantId, string JobOfferId, JobOfferStatus status)
        {
            var update = Builders<Applicant>.Update
                     .Set(x => x.JobOffer.JobOfferStatus, status);
            var result = _applicants.UpdateOne(x => x.ID == applicantId, update);
            return result;
        }


        public UpdateResult AddVacancyIdToApplicant(string applicantId, string vacancyId)
        {
            var update = Builders<Applicant>.Update
                    .Set(x => x.VacancyID, vacancyId);
            var result = _applicants.UpdateOne(x => x.ID == applicantId, update);
            return result;
        }
        public UpdateResult AddUserIdToApplicant(CreateUserResponse createUserResponse)
        {
            var update = Builders<Applicant>.Update
                    .Set(x => x.UserID, createUserResponse.UserID);
            var result = _applicants.UpdateOne(x => x.Email == createUserResponse.Email, update);
            return result;
        }

        public UpdateResult AddApplicantQuestionnare(string applicantId, SetUpApplicantQuestionnareDTO setUpApplicantQuestionnare)
        {
            var update = Builders<Applicant>.Update
                    .Set(x => x.Questionare, setUpApplicantQuestionnare);
            var result = _applicants.UpdateOne(x => x.ID == applicantId, update);
            return result;
        }


        public UpdateResult Update(VacancyResponse vacancyResponse)
        {
            var update = Builders<VacancyResponse>.Update
                .Set(x => x.FirstName, vacancyResponse.FirstName)
                .Set(x => x.LastName, vacancyResponse.LastName)
                .Set(x => x.Email, vacancyResponse.Email)
                .Set(x => x.Phone, vacancyResponse.Phone)
                .Set(x => x.CvUrl, vacancyResponse.CvUrl);
            var result = _vacancyResponses.UpdateOne(x => x.VacancyResponseID==vacancyResponse.VacancyResponseID, update);
            return result;
        }


        public UpdateResult Update(string vacancyId, string vacancyResponseID)
        {
            var update = Builders<Vacancy>.Update
                .AddToSet(x => x.VacancyResponses, vacancyResponseID);
            var result = _vacancies.UpdateOne(x => x.VacancyID == vacancyId, update);
            return result;
        }


        public DeleteResult Delete(string vacancyId) =>
            _vacancies.DeleteOne(x => x.VacancyID == vacancyId);

        
    }
}

