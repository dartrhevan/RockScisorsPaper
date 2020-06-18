using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RockScissorsPaper.Model
{
    public class User 
    {
        public User(string login, string password)
        {
            Login = login;
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(password);
        }
        public string PasswordHash { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Login { get; set; }
    }
}