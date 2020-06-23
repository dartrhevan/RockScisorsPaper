using RockScissorsPaper.Model;

namespace RockScissorsPaper.Services
{
    public interface IGameService
    {
        User JoinGame(User user, GameType type);

        void LeaveGame(User user);

        PlayResult Play(User user, GameValue value);

    }
}