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

        [HttpPost]
        public async Task<object> Login([FromForm] string login, [FromForm]string password)
        {
            logger.Log(LogLevel.Debug, login);
            var encodedJwt = await authService.LoginAsync(login, password);
            var response = new
            {
                access_token = encodedJwt,
                username = login
            };
            return response;
        }
        
        [HttpPost]
        public async Task<object> Register([FromForm] string login, [FromForm] string password)
        {
            var encodedJwt = await authService.RegisterAsync(login, password);
            var response = new
            {
                access_token = encodedJwt,
                username = login
            };
            return response;
        }
    }
}
