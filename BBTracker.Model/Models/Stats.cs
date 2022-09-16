using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace BBTracker.Model.Models
{
    public class Stats //: IStats
    {
        public Guid Id { get; internal set; }
        public int FieldGoalsMade2p { get; internal set; }
        public int FieldGoalsMissed2p { get; internal set; }
        public int Attempted2p { get => FieldGoalsMade2p + FieldGoalsMissed2p; }
        public int FieldGoalsMade3p { get; internal set; }
        public int FieldGoalsMissed3p { get; internal set; }
        public int Attempted3p { get => FieldGoalsMade3p + FieldGoalsMissed3p; }
        public int Assists { get; internal set; }
        public int DefensiveRebounds { get; internal set; }
        public int OffensiveRebounds { get; internal set; }
        public int Steals { get; internal set; }
        public int Blocks { get; internal set; }
        public int Turnovers { get; internal set; }
        //public int PlusMinus { get; set; }

        //public TimeSpan TimePlayed { get; set; }
        public int Points { get=> FieldGoalsMade2p*2 + FieldGoalsMade3p*3;}
        public int TotalRebounds { get=>DefensiveRebounds+OffensiveRebounds; }
        public double TwoPointsPercentage { get => GetPercentage(FieldGoalsMade2p, FieldGoalsMissed2p); }
        public double ThreePointsPercentage { get => GetPercentage(FieldGoalsMade3p, FieldGoalsMissed3p); }
        public List<Tuple<TimeSpan,bool>> Substitutions { get; internal set; }
        public TimeSpan GetPlayingTime()
        {
            var result = TimeSpan.Zero;
            foreach (var item in Substitutions)
            {
                if (item.Item2) result -= item.Item1;
                else result += item.Item1;
            }
            return result;
        }
        public double GetPercentage(int made, int missed)
        {
            if (missed + made == 0) return 0;
            double pct = 100.0 * made / (made + missed);
            return pct;
        }
        //todo: change tuple to sth described?
        public Stats()
        {
        }

        public Stats(Guid id)
        {
            Id = id;
            Substitutions = new List<Tuple<TimeSpan,bool>>();
        }


        //private bool Counting
        public void ResolvePlay(Play play)
        {
            play.UpdateStats(this);
        }

    }
}
