using BBTracker.Model.Interfaces;
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
        public Guid Id { get; set; }
        public int FieldGoalsMade2p { get; set; }
        public int FieldGoalsMissed2p { get; set; }
        public int FieldGoalsMade3p { get; set; }
        public int FieldGoalsMissed3p { get; set; }
        public int Assists { get; set; }
        public int DefensiveRebounds { get; set; }
        public int OffensiveRebounds { get; set; }
        public int Steals { get; set; }
        public int Blocks { get; set; }
        public int Turnovers { get; set; }
        //public TimeSpan TimePlayed { get; set; }
        //public int PlusMinus { get; set; }

        public int Points { get=> FieldGoalsMade2p*2 + FieldGoalsMade3p*3;}
        public int TotalRebounds { get=>DefensiveRebounds+OffensiveRebounds; }
        
        public Stats()
        {

        }

        public Stats(Guid id)
        {
            Id = id;
        }


        //private bool Counting
        public void ResolvePlay(Play play)
        {
            play.UpdateStats(this);
        }

    }
}
