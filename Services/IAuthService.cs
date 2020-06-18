using System.Threading.Tasks;

namespace RockScisorsPaper.Services
{
    public interface IAuthService
    {
        Task<string> LoginAsync(string login, string password);
        Task<string> RegisterAsync(string login, string password);
    }
}