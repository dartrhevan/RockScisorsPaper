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
        private static readonly Dictionary<User, string> usersConnections = new Dictionary<User, string>();

        public GameHub(ILogger<GameHub> logger, IGameService gameService, IAuthService authService)
        {
            _logger = logger;
            _gameService = gameService;
            _authService = authService;
        }

        public async Task JoinGame(string type)
        {
            GameType value;
            GameType.TryParse(type, out value);//TODO: handle error!
            var user = await CurrentUser;
            var competitor = _gameService.JoinGame(user, value);
            //TODO: check if exists
            usersConnections[user] = Context.ConnectionId;
            if (competitor != null)
            {
                if(usersConnections.ContainsKey(user))
                    await Clients.Client(usersConnections[user]).SendAsync("startGame", competitor.Login);
                if (usersConnections.ContainsKey(competitor))
                    await Clients.Client(usersConnections[competitor]).SendAsync("startGame", user.Login);
            }
        }

        private Task<User> CurrentUser => _authService.GetUser(Context.User.Identity.Name);

        public async Task LeaveGame()
        {
            var user = await CurrentUser;
            _gameService.LeaveGame(user);
            usersConnections.Remove(user);
            //TODO: send
        }

        public async Task Play(string val)
        {
            GameValue value;
            GameValue.TryParse(val, out value);
            var result = _gameService.Play(await CurrentUser, value);
            if (result.Result != GameResult.NotCompleted)
            {
                result.EndGame();
                if (usersConnections.ContainsKey(result.Looser))
                    await Clients.Client(usersConnections[result.Looser])
                    .SendAsync("playResult", "You are looser!", result.Winner.Value.ToString());
                if (usersConnections.ContainsKey(result.Winner))
                    await Clients.Client(usersConnections[result.Winner])
                        .SendAsync("playResult", "You are winner!", result.Looser.Value.ToString());
            }
        }
    }
}