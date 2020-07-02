using System;
using System.Collections.Concurrent;
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
        private static readonly ConcurrentDictionary<User, string> usersConnections = new ConcurrentDictionary<User, string>();

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

        async Task LeaveGame()
        {
            var user = await CurrentUser;
            string val;
            var competitor = _gameService.LeaveGame(user);
            _logger.Log(LogLevel.Debug, "On disconnect: " + competitor?.Login??"" );
            if (competitor != null && usersConnections.ContainsKey(competitor))
                await Clients.Client(usersConnections[competitor]).SendAsync("leaveGame");
            usersConnections.Remove(user, out val);
        }
        
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await LeaveGame();
        }
        

        public async Task Play(string val)
        {
            GameValue value;
            GameValue.TryParse(val, out value);
            var result = _gameService.Play(await CurrentUser, value);
            if (result.Result == GameResult.HasWinner)
            {
                result.EndGame();
                if (usersConnections.ContainsKey(result.Looser))
                    await Clients.Client(usersConnections[result.Looser])
                    .SendAsync("playResult", "You are looser!", result.Winner.Value.ToString());
                if (usersConnections.ContainsKey(result.Winner))
                    await Clients.Client(usersConnections[result.Winner])
                        .SendAsync("playResult", "You are winner!", result.Looser.Value.ToString());
            } 
            else if (result.Result == GameResult.Draw)
            {
                result.EndGame();
                if (usersConnections.ContainsKey(result.Looser))
                    await Clients.Client(usersConnections[result.Looser])
                        .SendAsync("playResult", "There is no winner!", result.Winner.Value.ToString());
                if (usersConnections.ContainsKey(result.Winner))
                    await Clients.Client(usersConnections[result.Winner])
                        .SendAsync("playResult", "There is no winner!", result.Looser.Value.ToString());
            }
        }
    }
}