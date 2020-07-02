using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;

namespace RockScissorsPaper.Model
{
    public class Game
    {
        public readonly User User1;
        public readonly User User2;

        public User GetCompetitor(User user)
        {
            if (user.Id == User1.Id)
                return User2;
            if (user.Id == User2.Id)
                return User1;
            throw new Exception("The user does not participate the game!");
        }
        public PlayResult Play()
        {
            if (User1 == null || User2 == null || User1.Value == GameValue.None || User2.Value == GameValue.None)
                return new PlayResult(null, null, GameResult.NotCompleted);
            if (User1.Value == User2.Value)
                return new PlayResult(User1, User2, GameResult.Draw);
            return User1.Value switch
            {
                GameValue.Rock => User2.Value switch
                {
                    GameValue.Scissors => new PlayResult(User2, User1),// Tuple.Create(User1, Value1),
                    GameValue.Paper => new PlayResult(User1, User2),
                    _ => throw new ArgumentException("Value2 exp")
                },
                GameValue.Scissors => User2.Value switch
                {
                    GameValue.Rock => new PlayResult(User1, User2),
                    GameValue.Paper => new PlayResult(User2, User1),
                    _ => throw new ArgumentException("Value2 exp")
                },
                GameValue.Paper => User2.Value switch
                {
                    GameValue.Scissors => new PlayResult(User1, User2),
                    GameValue.Rock => new PlayResult(User2, User1),
                    _ => throw new ArgumentException("Value2 exp")
                },
                _ => throw new ArgumentException("Value1 exp")
            };
        }
        protected bool Equals(Game other)
        {
            return Equals(User1, other.User1) && Equals(User2, other.User2);
        }

        public override string ToString() => $"{User1} <-> {User2}";

        public bool Participates(User user) => User1.Equals(user) || User2.Equals(user);

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Game) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((User1 != null ? User1.GetHashCode() : 0) * 397) ^ (User2 != null ? User2.GetHashCode() : 0);
            }
        }

        public Game(User user1, User user2)
        {
            User1 = user1;
            User2 = user2;
        }
    }
}
