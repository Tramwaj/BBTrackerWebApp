using BBTracker.App.Services;
using BBTracker.Model.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBTrackerTests.App.Services
{
    [TestFixture]
    public class NewPlaysParserTests
    {

        private Guid gameId;
        private Guid player1Id;
        private Guid player2Id;

        [SetUp]
        public void Setup()
        {
            gameId = Guid.NewGuid();
            player1Id = Guid.NewGuid();
            player2Id = Guid.NewGuid();
        }

        [Test]
        public void NullInputShouldGiveEmptyCollection()
        {
            string[] array = null;
            var plays = NewPlayParser.ReadPlays(array, gameId);
            Assert.IsTrue(plays.Count == 0);
        }
        [Test]
        public void EmptyArrayShouldGiveEmptyCollection()
        {
            string[] array = new string[0];
            var plays = NewPlayParser.ReadPlays(array, gameId);
            Assert.IsTrue(!plays.Any());
        }
        [Test]
        public void SimpleFieldGoalReadProperly()
        {
            string[] array =
                { player1Id.ToString(), "true", "fg" ,"3", "made", "noassist" };
            var plays = NewPlayParser.ReadPlays(array, gameId);
            Assert.Multiple(() =>
            {                
                Assert.IsTrue(plays.Count() == 1);
                Assert.IsTrue(plays.First() is FieldGoal);
                var play = plays.First() as FieldGoal;
                Assert.IsTrue(play.GameId == gameId);
                Assert.IsTrue(play.PlayerId == player1Id);
                Assert.IsTrue(play.IsTeamB == true);
                Assert.IsTrue(play.Points == 3);
            });
        }
        [Test]
        public void SimpleTurnoverReadProperly()
        {
            string[] array =
            {
                player1Id.ToString(), "true", "to", "nosteal"
            };

            var plays = NewPlayParser.ReadPlays(array, gameId);
            Assert.Multiple(() =>
            {
                Assert.IsTrue(plays.Count == 1);
                Assert.IsTrue(plays.First() is Turnover);
                var turnover = plays.First() as Turnover;
                Assert.IsTrue(turnover.GameId == gameId);
                Assert.IsTrue(turnover?.PlayerId == player1Id);
            });
        }
    }
}
