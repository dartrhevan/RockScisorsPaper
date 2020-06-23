using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RockScissorsPaper.Model
{
    public class User //: IPlayer
    {
        public User() { }

        private sealed class IdEqualityComparer : IEqualityComparer<User>
        {
            public bool Equals(User x, User y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (ReferenceEquals(x, null)) return false;
                if (ReferenceEquals(y, null)) return false;
                if (x.GetType() != y.GetType()) return false;
                return x.Id == y.Id;
            }

            public int GetHashCode(User obj)
            {
                return obj.Id;
            }
        }

        public static IEqualityComparer<User> IdComparer { get; } = new IdEqualityComparer();

        public User(string login, string password)
        {
            Login = login;
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(password);
        }
        public string PasswordHash { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Login { get; set; }
        [NotMapped]
        public GameValue Value { get; set; }
    }
}