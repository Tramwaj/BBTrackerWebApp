using BBTracker.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBTracker.App.Services
{
    public static class NewPlayParser
    {
        public static ICollection<Play> ReadPlays(string[] playsIn, Guid gameId)
        {
            //sprawdzenie początkowe - mozliwe ze do wywalenia
            if (playsIn == null || playsIn.Length == 0) return new List<Play>();
            Play play1, play2;
            Guid p1Id;

            if (!Guid.TryParse(playsIn[0], out p1Id)) return new List<Play>();
            bool p1Team;
            if (!bool.TryParse(playsIn[1], out p1Team)) return new List<Play>();

            //jedziemy
            List<Play> playsOut = new List<Play>();

            string _rootType = playsIn[2];
            playsOut.Add(
                _rootType switch
                {
                    "fg" => new FieldGoal(),
                    "to" => new Turnover(),
                    "sub" => new Substitution(),
                    _ => null
                });
            if (playsOut[0] is null) return new List<Play>();
            playsOut[0].GameId = gameId;
            playsOut[0].PlayerId = p1Id;
            playsOut[0].IsTeamB = p1Team;
            playsOut[0].Time = DateTime.Now;

            if (playsOut[0] is FieldGoal)
            {
                playsOut = ParseFieldGoalBundle(playsOut, playsIn);
            }
            if (playsOut[0] is Turnover)
            {
                playsOut = ParseTurnoverBundle(playsOut, playsIn);
            }
            if (playsOut[0] is Substitution)
            {
                (playsOut[0] as Substitution).SubbedIn = false;
                playsOut.Add(new Substitution
                {
                    GameId = playsOut[0].GameId,
                    PlayerId = Guid.Parse(playsIn[3]),
                    IsTeamB = Boolean.Parse(playsIn[4]),
                    Time = DateTime.Now,
                });
            }
            
            return playsOut;
        }

        private static List<Play> ParseFieldGoalBundle(List<Play> playsOut, string[] playsIn)
        {
            var play1 = playsOut.FirstOrDefault() as FieldGoal;
            play1.Points = Int32.Parse(playsIn[3]);
            //'made', 'missed', 'blocked'
            if (playsIn[4] == "made")
            {
                if (playsIn[5] == "noassist") play1.WasAssisted = false;
                else
                {
                    play1.WasAssisted = true;
                    playsOut.Add(new Assist
                    {
                        GameId = play1.GameId,
                        PlayerId = Guid.Parse(playsIn[5]),
                        IsTeamB = Boolean.Parse(playsIn[6]),
                        FieldGoal = play1,
                        Time = DateTime.Now,
                    });
                }
            }
            if (playsIn[4] == "missed")
            {
                if (playsIn[5] != "norebound")
                {
                    playsOut.Add(new Rebound
                    {
                        GameId = play1.GameId,
                        PlayerId = Guid.Parse(playsIn[5]),
                        IsTeamB = Boolean.Parse(playsIn[6]),
                        IsOffensive = Boolean.Parse(playsIn[6]) != play1.IsTeamB,
                        FieldGoalRebounded = play1,
                        Time = DateTime.Now,
                    });
                }
            }
            if (playsIn[4] == "blocked")
            {
                playsOut.Add(new Block
                {
                    GameId = play1.GameId,
                    PlayerId = Guid.Parse(playsIn[5]),
                    IsTeamB = Boolean.Parse(playsIn[6]),
                    FieldGoalBlocked = play1,
                    Time = DateTime.Now,
                });
            }
            return playsOut;
        }

        private static List<Play> ParseTurnoverBundle(List<Play> playsOut, string[] playsIn)
        {
            if (playsIn[3] != "nosteal")
            {
                playsOut.Add(new Steal
                {
                    GameId = playsOut[0].GameId,
                    PlayerId = Guid.Parse(playsIn[3]),
                    IsTeamB = Boolean.Parse(playsIn[4]),
                    Time = DateTime.Now,
                });
            }
            return playsOut;
        }
    }
}
