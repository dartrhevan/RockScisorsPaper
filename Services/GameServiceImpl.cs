using System;
using System.Collections.Generic;
using System.Linq;
using RockScissorsPaper.Model;

namespace RockScissorsPaper.Services
{
    public class GameServiceImpl : IGameService
    {

        public readonly List<User> WaitingUsers = new List<User>();

        public readonly HashSet<Game> Games = new HashSet<Game>();

        public void JoinGame(User user, GameType type, string competitor = null)
        {
            switch (type)
            {
                case GameType.RandomCompetitor:
                    JoinRandomGame(user);
                    break;
                case GameType.ParticularCompetitor:
                    JoinParticularlyGame(user, competitor);
                    break;
                case GameType.Bot:
                    JoinToBot(user);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        private void JoinToBot(User user)
        {
            throw new NotImplementedException();
        }

        private void JoinParticularlyGame(User user, string competitorLogin)
        {
            var competitor = WaitingUsers.FirstOrDefault(u => u.Login == competitorLogin);
            if (competitor != null)
            {
                Games.Add(new Game(user, competitor));
                WaitingUsers.Remove(competitor);
            }
            /*else
                WaitingUsers.Add(user);*/
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
            Games.RemoveWhere(g => g.Participates(user));
        }

        public PlayResult Play(User user, GameValue value)
        {
            var game = Games.FirstOrDefault(g => g.Participates(user));
            return game?.Play();
        }
    }
}