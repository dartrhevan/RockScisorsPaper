using NUnit.Framework;
using RockScissorsPaper.Model;
using RockScissorsPaper.Services;

namespace RockScissorsPaper.Tests
{
    [TestFixture]
    public class GameServiceTests
    {
        [Test]
        public void JoinRandomGameTest()
        {
            var gameService = new GameServiceImpl();
            var user = new User("a", "b");
            gameService.JoinGame(user, GameType.RandomCompetitor);
            Assert.AreEqual(1, gameService.WaitingUsers.Count);
        }


        [TestCase(5, 1, 2)]
        [TestCase(1, 1, 0)]
        [TestCase(3, 1, 1)]
        [TestCase(2,0, 1)]
        public void MultipleJoinRandomGameTest(int count, int result, int gameCount)
        {
            var gameService = new GameServiceImpl();
            for (var i = 0; i < count; i++)
            //var user = new User("a", "b");
                gameService.JoinGame(new User(i.ToString(), "b"), GameType.RandomCompetitor);
            Assert.AreEqual(result, gameService.WaitingUsers.Count, "Users count");
            Assert.AreEqual(gameCount, gameService.Games.Count, "Games count");
        }
    }
}