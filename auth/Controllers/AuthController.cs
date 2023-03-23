using System;
using auth.auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using auth.Models;
using auth.database;
using shraredclasses.Models;

namespace auth.Controllers
{
    public class AuthController : Controller
    {
        private readonly ILogger<AuthController> _logger;
        private DataBaseService _dbService;

        public AuthController(ILogger<AuthController> logger, DataBaseService service)
        {
            _logger = logger;
            _dbService = service;
        }

        [HttpPost("/Auth")]
        public IActionResult Auth(string email, string password)
        {
            ClaimsIdentity _claims = null;
            var user = _dbService.Identity(email, password);
            if (user != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, "")
                };
                _claims = new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

                var now = DateTime.UtcNow;

                var jwt = new JwtSecurityToken(
                issuer: AuthOptions.ISSUER,
                        audience: AuthOptions.AUDIENCE,
                        notBefore: now,
                        claims: _claims.Claims,
                        expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                        signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
                var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
                return Ok(new WsResponse() { Data = encodedJwt });
            }
            else
                return null;
        }
    }
}

