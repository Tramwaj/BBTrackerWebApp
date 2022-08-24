using BBTracker.App.Services;
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
        private Guid playerId;

        [SetUp]
        public void Setup()
        {
            gameId = Guid.NewGuid();
            playerId = Guid.NewGuid();
        }

        [Test]
        public void NullInputShouldGiveEmptyCollection()
        {
            string[] array = null;
            var plays = NewPlayParser.ReadPlays(array, gameId);
            Assert.IsTrue(plays.Count() == 0);
        }
        [Test]
        public void EmptyArrayShouldGiveEmptyCollection()
        {
            string[] array = new string[0];
            var plays = NewPlayParser.ReadPlays(array, gameId);
            Assert.IsTrue(!plays.Any());
        }
        //[Test]
        //public void SimpleTurnoverTest()
        //{
        //    string[] array =
        //        { playerId.ToString(), "false" , "sub" };
        //    var plays = NewPlayParser.ReadPlays(array,gameId);
        //    Assert.
        //}
    }
}
