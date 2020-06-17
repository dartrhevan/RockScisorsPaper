using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace RockScisorsPaper.Services
{
    public class GameHub : Hub
    {
        private ILogger<GameHub> _logger;

        public GameHub(ILogger<GameHub> logger)
        {
            _logger = logger;
        }
    }
}