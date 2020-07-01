using System;
using RockScissorsPaper.Services;

namespace RockScissorsPaper.Model
{
    public class PlayResult
    {
        public readonly User Looser;
        public readonly User Winner;
        public readonly GameResult Result;

        public PlayResult(User looser, User winner, GameResult result = GameResult.HasWinner)
        {
            Looser = looser;
            Winner = winner;
            Result = result;
        }

        public void EndGame()
        {
            CleanResult(Looser);
            CleanResult(Winner);
        }

        private void CleanResult(User user)
        {
            var random = new Random();
            user.Value = user.Login == "Bot" && user.Id == -1 ? (GameValue) random.Next(1, 3) : GameValue.None;
        }
    }
}