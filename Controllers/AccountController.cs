using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using RockScisorsPaper.Model;

namespace RockScisorsPaper.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;

        public AccountController(ILogger<AccountController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public string Test() => "Hello!";

        [Authorize]
        [HttpGet]
        public string Check() => HttpContext.User.Identity.Name;

        [HttpPost]
        public object Login([FromForm] string username, [FromForm]string password)
        {
            _logger.Log(LogLevel.Debug, username);
            // создаем JWT-токен
            var encodedJwt = "";//TODO:
            var response = new
            {
                access_token = encodedJwt,
                username = username
            };
            return response;
        }

    }
}
