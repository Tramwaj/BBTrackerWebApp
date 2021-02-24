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
        public int FieldGoalsMade3p { get; internal set; }
        public int FieldGoalsMissed3p { get; internal set; }
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
        public List<Tuple<TimeSpan,bool>> Substitutions { get; internal set; }
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
