//using BBTracker.App.Services;
//using BBTracker.Common;
//using BBTracker.Contracts.ViewModels;
//using BBTracker.Model.Models;
//using NUnit.Framework;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Reflection.Emit;
//using System.Security.Policy;

//namespace BasketStatsTests
//{
//    [TestFixture]
//    public class PlayParserTests
//    {
//        //todo: there should be single actions here (prepared in setup)
//        //each test uses them to create own bundle
//        private IPlayParser playParser;

//        private PlayDTO P0Made2pFG;
//        private PlayDTO P0Missed2pFG;
//        private PlayDTO P0Made3pFG;
//        private PlayDTO P0Missed3pFG;
//        private PlayDTO P1ReboundSameTeam;
//        private PlayDTO P2ReboundDiffTeam;
//        private PlayDTO P1Assisted;
//        private PlayDTO P2Blocked;
//        private PlayDTO P0LostTheBall;
//        private PlayDTO P2StoleTheBall;
//        private PlayDTO P0SubbedOut;
//        private PlayDTO P1SubbedIn;
//        private List<Guid> playerIds;
//        private Guid gameId;
//        private List<Play> result;

//        [SetUp]
//        public void Setup()
//        {
//            playParser = new PlayParser();
//            gameId = Guid.NewGuid();
//            playerIds = new List<Guid>
//            {
//                Guid.NewGuid(),Guid.NewGuid(),Guid.NewGuid(),Guid.NewGuid()
//            };
//            P0Made2pFG = new PlayDTO("fieldgoal", false, playerIds[0], true, 2);
//            P0Missed2pFG = new PlayDTO("Fieldgoal", false, playerIds[0], false, 2);
//            var P0_2pFGM = new FieldGoal(Guid.NewGuid(), DateTime.Now, false, playerIds[0], gameId, 2, false, false, true);
//            P0Made3pFG = new PlayDTO("fieldgoal", false, playerIds[0], true, 3);
//            P0Missed3pFG = new PlayDTO("Fieldgoal", false, playerIds[0], false, 3);
//            P1ReboundSameTeam = new PlayDTO("rebound", false, playerIds[1]);
//            P2ReboundDiffTeam = new PlayDTO("Rebound", true, playerIds[2]);
//            P1Assisted = new PlayDTO("assist", false, playerIds[1]);
//            P2Blocked = new PlayDTO("block", true, playerIds[2]);
//            P0LostTheBall = new PlayDTO("Turnover", false, playerIds[0]);
//            P2StoleTheBall = new PlayDTO("steal", true, playerIds[2]);
//            P0SubbedOut = new PlayDTO("substitution", false, playerIds[0], true);
//            P1SubbedIn = new PlayDTO("substitution", false, playerIds[1], false);
//        }

//        #region FieldGoals
//        [Test]
//        public void _2_point_field_goal_with_assist_gives_proper_plays()
//        {
//            var bundle = new List<PlayDTO> { P0Made2pFG, P1Assisted };
//            var P0_2pFGM = new FieldGoal(Guid.NewGuid(), DateTime.Now, false, playerIds[0], gameId, 2, true, false, true);
//            var P1Assist = new Assist(Guid.NewGuid(), DateTime.Now, false, playerIds[1], gameId, 2, playerIds[0]);
//            result = playParser.ReadPlaysBundle(bundle, gameId).ToList();
//            try
//            {
//                Assert.Multiple(() =>
//            {
//                Assert.IsTrue(Plays.AreEqual((FieldGoal)result.First(), P0_2pFGM));
//                Assert.IsTrue(Plays.AreEqual((Assist)result.Last(), P1Assist));
//            });
//            }
//            catch (Exception) { }
//        }
//        [Test]
//        public void _3_point_field_goal_missed_with_offensive_rebound_gives_proper_plays()
//        {
//            var bundle = new List<PlayDTO> { P0Missed3pFG, P1ReboundSameTeam };
//            var P0_2pFGMissed = new FieldGoal(Guid.NewGuid(), DateTime.Now, false, playerIds[0], gameId, 3, false, false, false);
//            var P1Rebound = new Rebound(Guid.NewGuid(), DateTime.Now, false, playerIds[1], gameId, true, 3);
//            result = playParser.ReadPlaysBundle(bundle, gameId).ToList(); ;
//            try
//            {
//                Assert.Multiple(() =>
//            {
//                Assert.IsTrue(Plays.AreEqual((FieldGoal)result.First(), P0_2pFGMissed));
//                Assert.IsTrue(Plays.AreEqual((Rebound)result.Last(), P1Rebound));
//            });
//            }
//            catch (Exception) { }
//        }
//        [Test]
//        public void _2_point_field_goal_missed_with_defensive_rebound_gives_proper_plays()
//        {
//            var bundle = new List<PlayDTO> { P0Missed2pFG, P2ReboundDiffTeam };
//            var P0_2pFGMissed = new FieldGoal(Guid.NewGuid(), DateTime.Now, false, playerIds[0], gameId, 2, false, false, false);
//            var P2Rebound = new Rebound(Guid.NewGuid(), DateTime.Now, true, playerIds[2], gameId, false, 2);
//            result = playParser.ReadPlaysBundle(bundle, gameId).ToList();
//            try
//            {
//                Assert.Multiple(() =>
//                {
//                    Assert.True(Plays.AreEqual((FieldGoal)result.First(), P0_2pFGMissed));
//                    Assert.True(Plays.AreEqual((Rebound)result.Last(), P2Rebound));
//                });
//            }
//            catch (Exception) { }
//        }
//        [Test]
//        public void _2_point_field_goal_blocked_with_defensive_rebound_gives_proper_plays()
//        {
//            var bundle = new List<PlayDTO> { P0Missed2pFG, P2Blocked, P2ReboundDiffTeam };
//            var P0_2pFGMissed = new FieldGoal(Guid.NewGuid(), DateTime.Now, false, playerIds[0], gameId, 2, false, true, false);
//            var P2Block = new Block(Guid.NewGuid(), DateTime.Now, true, playerIds[2], gameId, 2, playerIds[0]);
//            var P2Rebound = new Rebound(Guid.NewGuid(), DateTime.Now, true, playerIds[2], gameId, false, 2);

//            result = playParser.ReadPlaysBundle(bundle, gameId).ToList();
//            try
//            {
//                Assert.Multiple(() =>
//                {
//                    Assert.True(Plays.AreEqual((FieldGoal)result.First(), P0_2pFGMissed));
//                    Assert.True(Plays.AreEqual((Block)result[1], P2Block));
//                    Assert.True(Plays.AreEqual((Rebound)result.Last(), P2Rebound));
//                });
//            }
//            catch (Exception) { }
//        }
//        [Test]
//        public void Lone_steal_gives_proper_play()
//        {
//            var bundle = new List<PlayDTO> { P2StoleTheBall };
//            var P2Steal = new Steal(Guid.NewGuid(), DateTime.Now, true, playerIds[2], gameId);

//            result = playParser.ReadPlaysBundle(bundle, gameId).ToList();
//            try
//            {
//                Assert.True(Plays.AreEqual((Steal)result.Last(), P2Steal));
//            }
//            catch (Exception) { }
//        }

//        [Test]
//        public void Lone_turnover_gives_proper_play()
//        {
//            var bundle = new List<PlayDTO> { P0LostTheBall };
//            var P0Turnover = new Steal(Guid.NewGuid(), DateTime.Now, false, playerIds[0], gameId);

//            result = playParser.ReadPlaysBundle(bundle, gameId).ToList();
//            try
//            {
//                Assert.True(Plays.AreEqual((Turnover)result.Last(), P0Turnover));
//            }
//            catch (Exception) { }
//        }
//        [Test]
//        public void Steal_and_turnover_give_proper_plays()
//        {
//            var bundle = new List<PlayDTO> { P2StoleTheBall, P0LostTheBall };
//            var P2Steal = new Steal(Guid.NewGuid(), DateTime.Now, true, playerIds[2], gameId, playerIds[0]);
//            var P0Turnover = new Turnover(Guid.NewGuid(), DateTime.Now, false, playerIds[0], gameId, playerIds[2]);
//            try
//            {
//                Assert.Multiple(() =>
//                {
//                    Assert.True(Plays.AreEqual((Steal)result.First(), P2Steal));
//                    Assert.True(Plays.AreEqual((Turnover)result.Last(), P0Turnover));
//                });
//            }
//            catch (Exception) { }
//        }
//        [Test]
//        public void Turnover_and_steal_give_proper_plays()
//        {
//            var bundle = new List<PlayDTO> { P0LostTheBall, P2StoleTheBall };
//            var P2Steal = new Steal(Guid.NewGuid(), DateTime.Now, true, playerIds[2], gameId);
//            var P0Turnover = new Turnover(Guid.NewGuid(), DateTime.Now, false, playerIds[0], gameId);
//            try
//            {
//                Assert.Multiple(() =>
//                {
//                    Assert.True(Plays.AreEqual((Steal)result.First(), P2Steal));
//                    Assert.True(Plays.AreEqual((Turnover)result.Last(), P0Turnover));
//                });
//            }
//            catch (Exception) { }
//        }
//        [Test]
//        public void Lone_sub_out_gives_proper_play()
//        {
//            var bundle = new List<PlayDTO> { P0SubbedOut };
//            var P0SubOut = new Substitution(Guid.NewGuid(), DateTime.Now, false, playerIds[0], gameId, true);
//            result = playParser.ReadPlaysBundle(bundle, gameId).ToList();
//            try
//            {
//                Assert.True(Plays.AreEqual((Substitution)result.Last(), P0SubOut));
//            }
//            catch (Exception) { }
//        }
//        [Test]
//        public void Lone_sub_in_gives_proper_play()
//        {
//            var bundle = new List<PlayDTO> { P1SubbedIn };
//            var P1SubIn = new Substitution(Guid.NewGuid(), DateTime.Now, false, playerIds[1], gameId, false);
//            result = playParser.ReadPlaysBundle(bundle, gameId).ToList();
//            try
//            {
//                Assert.True(Plays.AreEqual((Substitution)result.Last(), P1SubIn));
//            }
//            catch (Exception) { }
//        }
//        [Test]
//        public void Two_substitutions_give_proper_plays()
//        {
//            var bundle = new List<PlayDTO> { P1SubbedIn,P0SubbedOut };
//            var P0SubOut = new Substitution(Guid.NewGuid(), DateTime.Now, false, playerIds[0], gameId, true);
//            var P1SubIn = new Substitution(Guid.NewGuid(), DateTime.Now, false, playerIds[1], gameId, false);
//            result = playParser.ReadPlaysBundle(bundle, gameId).ToList();
//            try
//            {
//                Assert.Multiple(() =>
//                {
//                    Assert.True(Plays.AreEqual((Substitution)result.First(), P1SubIn));
//                    Assert.True(Plays.AreEqual((Substitution)result.Last(), P0SubOut));
//                });
//            }
//            catch (Exception) { }
//        }

//        #endregion


//    }
//    internal static class Plays
//    {
//        internal static bool AreEqual(FieldGoal p1, FieldGoal p2)
//        {
//            if (AreEqual((Play)p1, (Play)p2) && p1.Points == p2.Points && p1.Made == p2.Made && p1.WasBlocked == p2.WasBlocked && p1.WasAssisted == p2.WasAssisted)
//                return true;
//            else
//                return false;
//        }
//        internal static bool AreEqual(Assist p1, Assist p2)
//        {
//            if (AreEqual((Play)p1, (Play)p2) && p1.Points == p2.Points && p1.Scorer == p2.Scorer && p1.ScorerId == p2.ScorerId)
//                return true;
//            else
//                return false;
//        }
//        internal static bool AreEqual(Rebound p1, Rebound p2)
//        {
//            if (AreEqual((Play)p1, (Play)p2) && p1.IsOffensive == p2.IsOffensive && p1.PointsFieldGoal == p2.PointsFieldGoal)
//                return true;
//            else
//                return false;
//        }
//        internal static bool AreEqual(Block p1, Block p2)
//        {
//            if (AreEqual((Play)p1, (Play)p2) && p1.PointsBlocked == p2.PointsBlocked && p1.BlockedPlayerId == p2.BlockedPlayerId)
//                return true;
//            else
//                return false;
//        }
//        internal static bool AreEqual(Substitution p1, Substitution p2)
//        {
//            if (AreEqual((Play)p1, (Play)p2) && p1.SubbedOut == p2.SubbedOut)
//                return true;
//            else
//                return false;
//        }
//        internal static bool AreEqual(Steal p1, Steal p2)
//        {
//            if (AreEqual((Play)p1, (Play)p2) && p1.StolenFromId == p2.StolenFromId)
//                return true;
//            else
//                return false;
//        }
//        internal static bool AreEqual(Turnover p1, Turnover p2)
//        {
//            var _eq = p1.TurnoverCauserId == p2.TurnoverCauserId;
//            if (AreEqual((Play)p1, (Play)p2) && _eq)
//                return true;
//            else
//                return false;
//        }

//        internal static bool AreEqual(Play p1, Play p2)
//        {
//            if (p1.Game == p2.Game && p1.IsTeamB == p2.IsTeamB && p1.PlayerId == p2.PlayerId && p1.GameId == p2.GameId)
//                return true;
//            else
//                return false;
//        }
//        //  
//    }
//}
