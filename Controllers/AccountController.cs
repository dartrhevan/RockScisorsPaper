using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using RockScissorsPaper.Services;

namespace RockScissorsPaper.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> logger;
        private readonly IAuthService authService;

        public AccountController(ILogger<AccountController> logger, IAuthService authService)
        {
            this.logger = logger;
            this.authService = authService;
        }

        [HttpGet]
        public string Test() => "Hello!";

        [Authorize]
        [HttpGet]
        public string Check() => HttpContext.User.Identity.Name;

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login([FromForm] string login, [FromForm]string password)
        {
            logger.Log(LogLevel.Debug, login);
            var encodedJwt = authService.Login(login, password);
            if (encodedJwt == null)
                return BadRequest(new { message = "Such user did not find"});
            var response = new
            {
                access_token = encodedJwt,
                username = login
            };
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register([FromForm] string login, [FromForm] string password)
        {
            logger.Log(LogLevel.Debug, login);
            logger.Log(LogLevel.Debug, password);
            var encodedJwt = await authService.RegisterAsync(login, password);
            if (encodedJwt == null)
                return BadRequest(new
                {
                    message = "Such user already exists"
                });
            var response = new
            {
                access_token = encodedJwt,
                username = login
            };
            return Ok(response);
        }
    }
}
