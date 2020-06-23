using System;
using System.Collections.Generic;
using System.Linq;
using RockScissorsPaper.Model;

namespace RockScissorsPaper.Services
{
    public class GameServiceImpl : IGameService
    {

        public readonly List<User> WaitingUsers = new List<User>();
        public readonly Dictionary<string, User> ParticularGameUsers = new Dictionary<string, User>();
        public readonly HashSet<Game> Games = new HashSet<Game>();

        public void JoinGame(User user, GameType type)
        {
            switch (type)
            {
                case GameType.RandomCompetitor:
                    JoinRandomGame(user);
                    break;
                case GameType.Bot:
                    JoinToBot(user);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        public GameServiceImpl(int seed)
        {
            random = new Random(seed);
        }

        public GameServiceImpl()
        {
            random = new Random();
        }


        private void JoinToBot(User user) => Games.Add(new Game(user, GetBot()));
        

        private readonly Random random;
        private User GetBot()
        {
            var bot = new User("Bot", "") {Value = (GameValue) random.Next(1, 3)};
            return bot;
        }
        

        private void JoinRandomGame(User user)
        {
            var competitor = WaitingUsers.LastOrDefault();
            if (competitor != null)
            {
                Games.Add(new Game(user, competitor));
                WaitingUsers.Remove(competitor);
            }
            else
                WaitingUsers.Add(user);
        }


        public void LeaveGame(User user)
        {
            WaitingUsers.Remove(user);
            ParticularGameUsers.Remove(user.Login);
            Games.RemoveWhere(g => g.Participates(user));

        }

        public PlayResult Play(User user)
        {
            var game = Games.FirstOrDefault(g => g.Participates(user));
            return game?.Play();
        }
    }
}