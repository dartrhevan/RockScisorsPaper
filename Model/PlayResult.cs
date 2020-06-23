using System;

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
            Looser.Value = GameValue.None;
            Winner.Value = GameValue.None;
        }


    }
}