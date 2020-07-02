using RockScissorsPaper.Model;

namespace RockScissorsPaper.Services
{
    public interface IGameService
    {
        User JoinGame(User user, GameType type);

        User LeaveGame(User user);

        PlayResult Play(User user, GameValue value);

    }
}