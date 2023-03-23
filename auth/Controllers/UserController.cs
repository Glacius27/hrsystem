using System;
using auth.database;
using auth.Logic;
using auth.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using shraredclasses.DTO;
using shraredclasses.Models;

namespace auth.Controllers
{

        [ApiController]
        [Route("[controller]")]
        public class UserController : ControllerBase
        {
            private readonly ILogger<UserController> _logger;
            private DataBaseService _dbService;
            private UserService _userService;
        

            public UserController(ILogger<UserController> logger, DataBaseService service, UserService userService)
            {
                _logger = logger;
                _dbService = service;
                _userService = userService;

            }

        #region
        //[HttpPost]
        //[Route("/User")]
        //public async Task <WsResponse> CreateUser(CreateUserRequestDTO createUserRequest)
        //{
        ////var _user = await _dbService.Create(new User(createUserRequest));
        //    var _user = await _userService.CreateUser(new User(createUserRequest));
        //    return new WsResponse()
        //    {
        //        Data = _user.UserID
        //    };
        //}
        #endregion
        [HttpPut]
            [Route("/User/password/{userId}")]
            public ActionResult<WsResponse> SetUserPassword(string userId, string password)
            {
                _userService.SetUserPassword(userId, password);
                //_dbService.SetPassword(userId, password);
                return Ok();
            }
        #region
        //[Authorize]
        //[HttpGet]
        //[Route("{userId}")]
        //public IActionResult GetUserByID(string userId)
        //{
        //    var checkresult = CheckIdentity(userId);
        //    if (!checkresult)
        //        return Forbid();
        //    else
        //    {
        //        var user = _dbService.Find(userId);
        //        return Ok(new WsResponse() { Data = user });
        //    }
        //}


        //[Authorize]
        //[HttpDelete]
        //[Route("{userId}")]
        //public IActionResult DeleteUser(string userId)
        //{
        //    var checkresult = CheckIdentity(userId);
        //    if (!checkresult)
        //        return Forbid();

        //    var result = _dbService.Delete(userId);
        //    if (result.DeletedCount > 0)
        //    {
        //        return Ok(new WsResponse() { Message = "user deleted" });
        //    }
        //    else
        //    {
        //        return NotFound(new WsResponse() { Message = "can`t find user" });
        //    }
        //}
        #endregion


        [Authorize]
            [HttpPut]
            [Route("{userId}")]
            public IActionResult UpdateUser(string userId, User user)
            {
                var checkresult = CheckIdentity(userId);
                if (!checkresult)
                    return Forbid();
                _dbService.Update(userId, user);
                return Ok(new WsResponse { Message = "User updated" });
            }

            private bool CheckIdentity(string userID)
            {
                var _user = HttpContext.User.Identities;
                string email = null;
                foreach (var i in _user)
                {
                    email = i.Name;
                }
                var user = _dbService.Find(userID);
                if (user.Email != email)
                    return false;
                else
                    return true;
            }
        
    }
}

