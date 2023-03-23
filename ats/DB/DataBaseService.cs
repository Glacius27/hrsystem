﻿using System;
using MongoDB.Driver;
using ats.Models;

namespace ats.DB
{
    public class DataBaseService
    {
        private readonly IMongoCollection<Vacancy> _vacancies;
        //private readonly IMongoCollection<Applicant> _applicants;

        public DataBaseService(IDataBaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _vacancies = database.GetCollection<Vacancy>("Vacancies");
            //_applicants = database.GetCollection<Vacancy>("Applicants");
        }

        public User Create(User user)
        {
            _users.InsertOne(user);
            return user;
        }

        public IList<User> Read() =>
            _users.Find(x => true).ToList();

        public User Find(string userId) =>
            _users.Find(x => x.UserID == userId).SingleOrDefault();

        public User Identity(string email, string password) =>
            _users.Find(x => x.Email == email && x.Password == password).SingleOrDefault();

        public UpdateResult Update(string userId, User user)
        {
            var update = Builders<User>.Update
                    .Set(x => x.FirstName, user.FirstName)
                    .Set(x => x.LastName, user.LastName)
                    .Set(x => x.Email, user.Email);
            var result = _users.UpdateOne(x => x.UserID == userId, update);
            return result;
        }
        public DeleteResult Delete(string userId) =>
            _users.DeleteOne(x => x.UserID == userId);

        public UpdateResult SetPassword(string userId, string password)
        {
            var update = Builders<User>.Update
                    .Set(x => x.Password, password);
            var result = _users.UpdateOne(x => x.UserID == userId, update);
            return result;
        }
    }
}

