using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RockScissorsPaper.Model
{
    public class Game
    {
        public readonly User User1;
        public readonly User User2;

        public Tuple<User, GameValue> GetWinner()
        {
            if(User1 == null || User2 == null || Value1 == GameValue.None || Value2 == GameValue.None)
                throw new Exception("Not all players played");
            if (Value2 == Value1)
                return null;
            return Value1 switch
            {
                GameValue.Rock => Value2 switch
                {
                    GameValue.Scissors => Tuple.Create(User1, Value1),
                    GameValue.Paper => Tuple.Create(User2, Value2),
                    _ => throw new ArgumentException("Value2 exp")
                },
                GameValue.Scissors => Value2 switch
                {
                    GameValue.Rock => Tuple.Create(User2, Value2),
                    GameValue.Paper => Tuple.Create(User1, Value1),
                    _ => throw new ArgumentException("Value2 exp")
                },
                GameValue.Paper => Value2 switch
                {
                    GameValue.Scissors => Tuple.Create(User2, Value2),
                    GameValue.Rock => Tuple.Create(User1, Value1),
                    _ => throw new ArgumentException("Value2 exp")
                },
                _ => throw new ArgumentException("Value1 exp")
            };
        }

        public GameValue Value1 { get; set; } = GameValue.None;
        public GameValue Value2 { get; set; } = GameValue.None;

        protected bool Equals(Game other)
        {
            return Equals(User1, other.User1) && Equals(User2, other.User2);
        }

        public bool Participate(User user) => User1.Equals(user) || User2.Equals(user);

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
