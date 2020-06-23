using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using RockScissorsPaper.Model;
using RockScissorsPaper.Services;

namespace RockScissorsPaper.Controllers
{
    [Authorize]
    public class GameHub : Hub
    {
        private ILogger<GameHub> _logger;
        private readonly IGameService _gameService;
        private readonly IAuthService _authService;
        private readonly Dictionary<User, string> usersConnections = new Dictionary<User, string>();

        public GameHub(ILogger<GameHub> logger, IGameService gameService, IAuthService authService)
        {
            _logger = logger;
            _gameService = gameService;
            _authService = authService;
        }

        public async Task JoinGame(string type)
        {
            GameType value;
            GameType.TryParse(type, out value);
            var user = await CurrentUser;
            var competitor = _gameService.JoinGame(user, value);
            usersConnections[user] = Context.ConnectionId;
            if (competitor != null)
                await Task.WhenAll(Clients.User(usersConnections[user]).SendAsync("startGame", competitor.Login),
                    Clients.User(usersConnections[competitor]).SendAsync("startGame", user.Login));
        }

        private Task<User> CurrentUser => _authService.GetUser(Context.User.Identity.Name);

        public async Task LeaveGame()
        {
            var user = await CurrentUser;
            _gameService.LeaveGame(user);
            usersConnections.Remove(user);
        }

        public async void Play(string val)
        {
            GameValue value;
            GameValue.TryParse(val, out value);
            var result = _gameService.Play(await CurrentUser, value);
            if (result.Result != GameResult.NotCompleted)
                result.EndGame();
            await Task.WhenAll(
                Clients.User(usersConnections[result.Looser])
                    .SendAsync("playResult", "You are looser!", result.Winner.Value.ToString()),
                Clients.User(usersConnections[result.Winner])
                    .SendAsync("playResult", "You are winner!", result.Looser.Value.ToString()));
        }
    }
}