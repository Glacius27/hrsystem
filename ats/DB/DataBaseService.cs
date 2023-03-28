using System;
using MongoDB.Driver;
using ats.Models;
using shraredclasses.Commands;
using shraredclasses.DTOs;

namespace ats.DB
{
    public class DataBaseService
    {
        private readonly IMongoCollection<Vacancy> _vacancies;
        private readonly IMongoCollection<VacancyResponse> _vacancyResponses;
        private readonly IMongoCollection<CreateUser> _createUserRequests;
        private readonly IMongoCollection<Applicant> _applicants;

        public DataBaseService(IDataBaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _vacancies = database.GetCollection<Vacancy>("Vacancies");
            _vacancyResponses = database.GetCollection<VacancyResponse>("VacanciesResponses");
            _createUserRequests = database.GetCollection<CreateUser>("CreateUserRequests");
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

