using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RockScisorsPaper.Services;
//using Microsoft.AspNet.Identity;
//using Microsoft.Owin.Security;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNet.Identity.Owin;
using System.Web;
using RockScisorsPaper.Model;

namespace RockScisorsPaper.Controllers
{/*
    [ApiController]
    [Route("[controller]")]*/
    public class AccountController : ControllerBase
    {/*
        private static CustomUserManager _customUserManager;

        public CustomUserManager UserManager
        {
            get
            {
                
                return _customUserManager ??
                       (_customUserManager = HttpContext.GetOwinContext().GetUserManager<CustomUserManager>());
            }
        }
        */
        private readonly ILogger<AccountController> _logger;
        private readonly SignInManager<User> _signInManager;

        public AccountController(ILogger<AccountController> logger, SignInManager<User> signInManager)
        {
            _logger = logger;
            _signInManager = signInManager;
        }
        [HttpGet]
        //[Route("/Test")]
        public string Test() => "Hello!";

        [HttpGet]
        public async Task<Claim[]> Login(string login, string password)
        {
            await _signInManager.SignInAsync(new User(login, password), false);
            return HttpContext.User.Claims.ToArray();
        }
    }
}
