using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RockScissorsPaper.Model
{
    public class User //: IPlayer
    {
        public User() { }
        public override string ToString() => $"{Login}: {Value}";

        protected bool Equals(User other)
        {
            return Id == other.Id;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((User) obj);
        }

        public override int GetHashCode()
        {
            return Id;
        }

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