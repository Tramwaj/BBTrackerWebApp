using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBTracker.Contracts.ViewModels
{
    public class StatsDTO
    {
        public Guid PlayerId { get; set; }
        public int Points { get; set; }
        public int FieldGoalsMade2p { get; set; }
        public int FieldGoalsMissed2p { get; set; }
        public int FieldGoalsMade3p { get; set; }
        public int FieldGoalsMissed3p { get; set; }
        public int Assists { get; set; }
        public int Rebounds { get; set; }
        public int DefensiveRebounds { get; set; }
        public int OffensiveRebounds { get; set; }
        public int Steals { get; set; }
        public int Blocks { get; set; }
        public int Turnovers { get; set; }
    }
}
