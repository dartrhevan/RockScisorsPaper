using System.Threading.Tasks;

namespace RockScissorsPaper.Services
{
    public interface IAuthService
    {
        string Login(string login, string password);
        Task<string> RegisterAsync(string login, string password);
    }
}