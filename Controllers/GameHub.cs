using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using RockScissorsPaper.Model;
using RockScissorsPaper.Services;

namespace RockScissorsPaper.Controllers
{
    public class GameHub : Hub
    {
        private ILogger<GameHub> _logger;
        private readonly IGameService _gameService;
        private readonly IAuthService _authService;

        public GameHub(ILogger<GameHub> logger, IGameService gameService, IAuthService authService)
        {
            _logger = logger;
            _gameService = gameService;
            _authService = authService;
        }

        public async Task JoinGame(string type, string competitor = null)
        {
            GameType value;
            GameType.TryParse(type, out value);
            _gameService.JoinGame(await _authService.GetUser(Context.User.Identity.Name), value);
        }

        public void LeaveGame()
        {
            //TODO: implement
        }
        public void Play(string value)
        {
            //TODO: implement
        }
    }
}