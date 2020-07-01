using NUnit.Framework;
using RockScissorsPaper.Model;

namespace RockScissorsPaper.Tests
{
    [TestFixture]
    public class GameTests
    {
        User user1 = new User("1", "") {Id = 1}; 
        User user2 = new User("2", "") { Id = 2 };

        [TestCase(GameValue.Scissors, GameValue.Paper, 1)]
        [TestCase(GameValue.Rock, GameValue.Scissors, 1)]
        [TestCase(GameValue.Rock, GameValue.Paper, 2)]
        public void PlayResultTest(GameValue value1, GameValue value2, int winnerId)
        {
            user1.Value = value1;
            user2.Value = value2;
            var game = new Game(user1, user2);
            var result = game.Play();
            Assert.AreEqual(winnerId, result.Winner.Id);
        }
    }
}