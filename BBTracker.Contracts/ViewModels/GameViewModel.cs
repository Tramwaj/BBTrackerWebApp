using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBTracker.Contracts.ViewModels
{
    public class GameViewModel
    {
        public Guid Id { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        
        public StatsDTO TeamAStats { get; set; }
        public StatsDTO TeamBStats { get; set; }
        public ICollection<StatsDTO> TeamA{ get; set; }
        public ICollection<StatsDTO> TeamB{ get; set; }

        public GameViewModel(Guid id, DateTime start, DateTime end, StatsDTO teamAStats, StatsDTO teamBStats, ICollection<StatsDTO> teamA, ICollection<StatsDTO> teamB)
        {
            Id = id;
            Start = start;
            End = end;
            TeamAStats = teamAStats;
            TeamBStats = teamBStats;
            TeamA = teamA;
            TeamB = teamB;
        }
        
    }

}
