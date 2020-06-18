using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace RockScisorsPaper
{
    public class AuthOptions
    {
        public const string ISSUER = "RockScisorsPaper"; // издатель токена
        public const string AUDIENCE = "RockScisorsPaperFront"; // потребитель токена
        const string KEY = "5tguybiu3hfewnds!";   // ключ для шифрации
        public const int LIFETIME = 101; // время жизни токена - 101 минута
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}