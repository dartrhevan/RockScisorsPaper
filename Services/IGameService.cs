using RockScissorsPaper.Model;

namespace RockScissorsPaper.Services
{
    public interface IGameService
    {
        void JoinGame(User user, GameType type, string competitor = null);

        void LeaveGame(User user);

        User Play(User user, GameValue value);

    }
}