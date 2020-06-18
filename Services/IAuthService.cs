using System.Threading.Tasks;

namespace RockScissorsPaper.Services
{
    public interface IAuthService
    {
        Task<string> LoginAsync(string login, string password);
        Task<string> RegisterAsync(string login, string password);
    }
}