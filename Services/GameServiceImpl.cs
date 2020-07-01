using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using RockScissorsPaper.Model;

namespace RockScissorsPaper.Services
{
    public class GameServiceImpl : IGameService
    {

        public readonly List<User> WaitingUsers = new List<User>();
        public readonly HashSet<Game> Games = new HashSet<Game>();

        public User JoinGame(User user, GameType type)
        {
            if (WaitingUsers.Contains(user) || Games.Any(g => g.Participates(user)))
                return null;//TODO: check
            return type switch
                {
                    GameType.RandomCompetitor => JoinRandomGame(user),
                    GameType.Bot => JoinToBot(user),
                    _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
                };
        }
        public GameServiceImpl(int seed)
        {
            random = new Random(seed);
        }

        public GameServiceImpl()
        {
            random = new Random();
        }


        private User JoinToBot(User user)
        {
            var bot = GetBot();
            Games.Add(new Game(user, bot));
            return bot;
        }


        private readonly Random random;
        private User GetBot()
        {
            var bot = new User("Bot", "") {Value = (GameValue) random.Next(1, 3)};
            return bot;
        }
        

        private User JoinRandomGame(User user)
        {
            var competitor = WaitingUsers.LastOrDefault();
            if (competitor != null)
            {
                Games.Add(new Game(user, competitor));
                WaitingUsers.Remove(competitor);
                return competitor;
            }
            else
                WaitingUsers.Add(user);

            return null;
        }


        public void LeaveGame(User user)
        {
            WaitingUsers.Remove(user);
            Games.RemoveWhere(g => g.Participates(user));

        }

        public PlayResult Play(User user, GameValue value)
        {
            //user.Value = value;
            var game = Games.FirstOrDefault(g => g.Participates(user));
            if(user.Equals(game.User1))
                game.User1.Value = value;
            else
                game.User2.Value = value;
            return game.Play();
        }
    }
}