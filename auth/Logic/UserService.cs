using System;
using auth.Controllers;
using auth.database;
using auth.Models;

namespace auth.Logic
{
	public class UserService
	{
        private readonly ILogger<UserService> _logger;
        private DataBaseService _dbService;

        public UserService(ILogger<UserService> logger, DataBaseService service)
        {
            _logger = logger;
            _dbService = service;
        }

        public string CreateUser(User user)
        {
            var _user = _dbService.Create(user);
            return _user.UserID;
        }
        public void UpdateUser()
        {

        }
        public void SetUserPassword(string userId, string password)
        {
            _dbService.SetPassword(userId, password);

        }
        //public void GetUserDetails()
        //{

        //}
        public void DeleteUser(string userId)
        {
            var result = _dbService.Delete(userId);
        }
	}
}

