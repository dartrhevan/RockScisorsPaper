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
            var gameService = new GameServiceImpl(seed);
            var user = new User("a", "b");
            gameService.JoinGame(user, GameType.RandomCompetitor);
            Assert.AreEqual(1, gameService.WaitingUsers.Count);
        }


        [TestCase(5, 1, 2)]
        [TestCase(1, 1, 0)]
        [TestCase(3, 1, 1)]
        [TestCase(2, 0, 1)]
        public void MultipleJoinRandomGameTest(int count, int result, int gameCount)
        {
            var gameService = new GameServiceImpl(seed);
            for (var i = 0; i < count; i++)
                gameService.JoinGame(new User(i.ToString(), "b") {Id = i}, GameType.RandomCompetitor);
            Assert.AreEqual(result, gameService.WaitingUsers.Count, "Users count");
            Assert.AreEqual(gameCount, gameService.Games.Count, "Games count");
        }

        [TestCase(GameValue.Paper, GameValue.Scissors, false)]
        [TestCase(GameValue.Scissors, GameValue.Rock, false)]
        [TestCase(GameValue.Paper, GameValue.Rock, true)]
        public void PlayRandomGame(GameValue aValue, GameValue cValue, bool aWin)
        {
            var gameService = new GameServiceImpl(seed);
            var a = new User("a", "b") {Id = 1};
            var c = new User("c", "b") {Id = 2};
            var comp1 = gameService.JoinGame(a, GameType.RandomCompetitor);
            var comp2 = gameService.JoinGame(c, GameType.RandomCompetitor);
            Assert.AreEqual(null, comp1);
            Assert.AreEqual(a, comp2);

            var intermediateResult = gameService.Play(a, aValue);
            Assert.AreEqual(GameResult.NotCompleted, intermediateResult.Result);
            Assert.AreEqual(null, intermediateResult.Winner);
            Assert.AreEqual(null, intermediateResult.Looser);

            var result = gameService.Play(c, cValue);
            Assert.AreEqual(GameResult.HasWinner, result.Result);
            Assert.AreEqual(aWin ? a : c, result.Winner);
            Assert.AreEqual(aWin ? c : a, result.Looser);
        }

        [Test]
        public void PlayRandomGameDraw()
        {
            var gameService = new GameServiceImpl(seed);
            var a = new User("a", "b") {Id = 1};
            var c = new User("c", "b") {Id = 2};
            gameService.JoinGame(a, GameType.RandomCompetitor);
            gameService.JoinGame(c, GameType.RandomCompetitor);

            var intermediateResult = gameService.Play(a, GameValue.Paper);
            Assert.AreEqual(GameResult.NotCompleted, intermediateResult.Result);
            Assert.AreEqual(null, intermediateResult.Winner);
            Assert.AreEqual(null, intermediateResult.Looser);

            var result = gameService.Play(c, GameValue.Paper);
            Assert.AreEqual(GameResult.Draw, result.Result);
            Assert.AreEqual( a, result.Winner);
            Assert.AreEqual( c, result.Looser);
        }

        [Test]
        public void PlyWithBot()
        {

            var gameService = new GameServiceImpl(seed);
            var a = new User("a", "b") {Id = 1};
            gameService.JoinGame(a, GameType.Bot);

            var result = gameService.Play(a, GameValue.Paper);
            Assert.AreEqual(GameValue.Rock, result.Looser.Value);
            Assert.AreEqual(GameResult.HasWinner, result.Result);
            Assert.AreEqual(a, result.Winner);
        }

        [Test]
        public void PlaySeveralTimes()
        {

            var gameService = new GameServiceImpl(seed);
            var a = new User("a", "b") { Id = 1 };
            var c = new User("c", "b") { Id = 2 };
            gameService.JoinGame(a, GameType.RandomCompetitor);
            gameService.JoinGame(c, GameType.RandomCompetitor);
            for (var i = 0; i < 3; i++)
            {
                var intermediateResult = gameService.Play(a, GameValue.Paper);
                Assert.AreEqual(GameResult.NotCompleted, intermediateResult.Result);
                Assert.AreEqual(null, intermediateResult.Winner);
                Assert.AreEqual(null, intermediateResult.Looser);
                var result = gameService.Play(c, GameValue.Rock);
                Assert.AreEqual(GameResult.HasWinner, result.Result);
                Assert.AreEqual(a, result.Winner);
                Assert.AreEqual(c, result.Looser);
                result.EndGame();
            }
        }
        /*
        [Test]
        public void ThreeDifferentGamesTest()
        {
            var gameService = new GameServiceImpl(seed);
            var a = new User("a", "b") { Id = 1 };
            var c = new User("c", "b") { Id = 2 };
            gameService.JoinGame(a, GameType.RandomCompetitor);
            gameService.JoinGame(c, GameType.RandomCompetitor);
            var e = new User("e", "b") { Id = 4 };
            var f = new User("f", "b") { Id = 5 };
            gameService.JoinGame(e, GameType.RandomCompetitor);
            gameService.JoinGame(f, GameType.RandomCompetitor);

            var intermediateResult = gameService.Play(a, GameValue.Paper);
            var d = new User("d", "b") { Id = 3 };
            gameService.JoinGame(d, GameType.Bot);

            var resultBot = gameService.Play(a, GameValue.Paper);
            Assert.AreEqual(GameValue.Rock, resultBot.Looser.Value);
            Assert.AreEqual(GameResult.HasWinner, resultBot.Result);
            Assert.AreEqual(d, resultBot.Winner);
            Assert.AreEqual(GameResult.NotCompleted, intermediateResult.Result);
            Assert.AreEqual(null, intermediateResult.Winner);
            Assert.AreEqual(null, intermediateResult.Looser);

            var result = gameService.Play(c, GameValue.Paper);
            Assert.AreEqual(GameResult.Draw, result.Result);
            Assert.AreEqual(a, result.Winner);
            Assert.AreEqual(c, result.Looser);
        }
        */
        [Test]
        public void LeaveGameTest()
        {

            var gameService = new GameServiceImpl(seed);
            var a = new User("a", "b") { Id = 1 };
            var c = new User("c", "b") { Id = 2 };
            var comp1 = gameService.JoinGame(a, GameType.RandomCompetitor);
            var comp2 = gameService.JoinGame(c, GameType.RandomCompetitor);

            var comp = gameService.LeaveGame(a);
            Assert.AreEqual(c, comp);
            Assert.AreEqual(0, gameService.WaitingUsers.Count);
            Assert.AreEqual(0, gameService.Games.Count);
        }

        [Test]
        public void DoubleJoiningTest()
        {
            var gameService = new GameServiceImpl(seed);
            var a = new User("a", "b") { Id = 1 };
            var c = new User("c", "b") { Id = 2 };
            var comp1 = gameService.JoinGame(a, GameType.RandomCompetitor);
            var comp2 = gameService.JoinGame(c, GameType.RandomCompetitor);

            Assert.AreEqual(1, gameService.Games.Count, "Games count");
            gameService.JoinGame(a, GameType.RandomCompetitor);
            gameService.JoinGame(c, GameType.RandomCompetitor);

            Assert.AreEqual(0, gameService.WaitingUsers.Count, "Users count");
            Assert.AreEqual(1, gameService.Games.Count, "Games count");
        }

        private const int seed = 510;
    }
}