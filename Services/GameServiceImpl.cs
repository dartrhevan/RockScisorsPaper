using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using ConcurrentCollections;
using RockScissorsPaper.Model;

namespace RockScissorsPaper.Services
{
    public class GameServiceImpl : IGameService
    {

        public readonly HashSet<User> WaitingUsers = new HashSet<User>();
        public readonly ConcurrentHashSet<Game> Games = new ConcurrentHashSet<Game>();
        /// <summary>
        /// To join the game. Retrieve a competitor. If such wasn't found retrieve null and save user to allow to notify about game start later.
        /// </summary>
        /// <param name="user">User to join</param>
        /// <param name="type">Type of the game</param>
        /// <returns></returns>
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
            var bot = new User("Bot", "") { Value = (GameValue)random.Next(1, 3), Id = -1 };
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

        /// <summary>
        /// Should be executed when a user leaves the game, retrieve a competitor, if exists
        /// </summary>
        /// <param name="user"></param>
        public User LeaveGame(User user)
        {
            WaitingUsers.Remove(user);
            
            var game = Games.FirstOrDefault(g => g.Participates(user));
            if (game != null)
            {
                Games.TryRemove(game);
                return game.GetCompetitor(user);
            }
            return null;
        }
        /// <summary>
        /// If GameResult is not NotCompleted method EndGame of a PlayResult object should be executed.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public PlayResult Play(User user, GameValue value)
        {
            var game = Games.FirstOrDefault(g => g.Participates(user));
            if(user.Equals(game.User1))
                game.User1.Value = value;
            else
                game.User2.Value = value;
            return game.Play();
        }
    }
}