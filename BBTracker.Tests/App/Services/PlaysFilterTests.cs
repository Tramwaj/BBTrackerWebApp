using BBTracker.App.Services.StatsCounting;
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
    public class PlaysFilterTests
    {

        List<Play> SubIns;
        List<Play> SubOuts;
        List<Play> PlaysIn;
        List<Play> PlaysOut;
        Play SubIn;
        Play SubOut;
        Play PlayIn;
        Play PlayOut;
        List<Player> players;
        List<Player> games;

        DateTime gameStart;

        [SetUp]
        public void Setup()
        {
            gameStart = DateTime.Now;
            Game game = new Game(Guid.NewGuid(), Guid.NewGuid(), gameStart);
            players = new List<Player> { new Player(Guid.NewGuid(), "a", "b") };

            SubIns = new List<Play> { new Substitution(Guid.NewGuid(), gameStart, true, players.First(), game, true) };
            SubOuts = new List<Play> { new Substitution(Guid.NewGuid(), gameStart.AddHours(2), true, players.First(), game, false) };
            PlaysIn = new List<Play> { new Steal(Guid.NewGuid(), gameStart.AddHours(1), true, players.First(), game) };
            PlaysOut = new List<Play> { new Turnover(Guid.NewGuid(), gameStart.AddHours(2.5), true, players.First(), game) };
            SubIn = SubIns.First();
            SubOut = SubOuts.First();
            PlayIn = PlaysIn.First();
            PlayOut = PlaysOut.First();
        }
        [Test]
        public void PlaysFilterNullTest()
        {
            IEnumerable<Play> input = null;
            IEnumerable<Play> output = input.FilterPlayerOnCourt(players.First());
            Assert.True(output is null);
        }
        [Test]
        public void PlaysFilterIncludePlaysWhenOnePeriod()
        {
            IEnumerable<Play> input = new List<Play> { SubIn, SubOut, PlayIn };
            IEnumerable<Play> output = input.FilterPlayerOnCourt(players.First()).ToList();
            Assert.True(output.ToList().Count == 1);
            //Assert.True(output.ToList()[0] is Steal);
        }
        [Test]
        public void PlaysFilterExcludePlaysWhenOnePeriod()
        {
            IEnumerable<Play> input = new List<Play> { SubIn, SubOut, PlayIn,PlayOut };
            IEnumerable<Play> output = input.FilterPlayerOnCourt(players.First()).ToList();
            Assert.True(output.ToList().Count == 1);
            //Assert.True(output.ToList()[0] is Steal);
        }
        [Test]
        public void MojTest()
        {
            ICollection<Play> plays = new List<Play> { SubIns[0], SubOuts[0] };
            IEnumerable<Substitution> playerSubs = plays.Where(x => x.Player == players.First())
                                       .Where(x => x is Substitution)
                                      .Select(x => (Substitution)x);
            ICollection<Period> periods = playerSubs.Where(x => x.SubbedIn == true)
                                                               .Zip(playerSubs.Where(x => x.SubbedIn == false), (x, y) => new Period(x.Time, y.Time))
                                                               .ToList();
            Assert.True(periods.First().Start==gameStart);
            Assert.True(periods.First().End == gameStart.AddHours(2));
        }
    }
}
