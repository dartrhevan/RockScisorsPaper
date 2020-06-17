using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;
//using Microsoft.AspNet.Identity;

namespace RockScisorsPaper.Model
{
    public class User //: IUser<string>
    {
        public User(string name, string password)
        {
            //Id = Guid.NewGuid().ToString();
            UserName = name;
            //TODO: implement real hashing
            PasswordHash = password.GetHashCode().ToString();
        }
        public string PasswordHash { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string UserName { get; set; }
    }
}