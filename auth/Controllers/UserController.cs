using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace auth.Controllers
{
	public class UserController
	{
        [ApiController]
        [Route("[controller]")]
        public class UserController : ControllerBase
        {
            private readonly ILogger<UserController> _logger;
            private DataBaseService _dbService;

            public UserController(ILogger<UserController> logger, DataBaseService service)
            {
                _logger = logger;
                _dbService = service;
            }


            [HttpPost]
            [Route("/User")]
            public ActionResult<Response> CreateUser(CreateUserRequest createUserRequest)
            {
                var _user = _dbService.Create(new User(createUserRequest));
                return new Response()
                {
                    Data = _user.UserID
                };
            }

            [HttpPut]
            [Route("/User/Register/{userId}")]
            public ActionResult<Response> SetUserPassword(string userId, string password)
            {
                _dbService.SetPassword(userId, password);
                return Ok();
            }

            [Authorize]
            [HttpGet]
            [Route("{userId}")]
            public IActionResult GetUserByID(string userId)
            {
                var checkresult = CheckIdentity(userId);
                if (!checkresult)
                    return Forbid();
                else
                {
                    var user = _dbService.Find(userId);
                    return Ok(new Response() { Data = user });
                }
            }


            [Authorize]
            [HttpDelete]
            [Route("{userId}")]
            public IActionResult DeleteUser(string userId)
            {
                var checkresult = CheckIdentity(userId);
                if (!checkresult)
                    return Forbid();

                var result = _dbService.Delete(userId);
                if (result.DeletedCount > 0)
                {
                    return Ok(new Response() { Code = 200, Message = "user deleted" });
                }
                else
                {
                    return NotFound(new Response() { Code = 404, Message = "can`t find user" });
                }
            }

            [Authorize]
            [HttpPut]
            [Route("{userId}")]
            public IActionResult UpdateUser(string userId, User user)
            {
                var checkresult = CheckIdentity(userId);
                if (!checkresult)
                    return Forbid();
                _dbService.Update(userId, user);
                return Ok(new Response { Code = (int)ErrorCode.Success, Message = "User updated" });
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
}

