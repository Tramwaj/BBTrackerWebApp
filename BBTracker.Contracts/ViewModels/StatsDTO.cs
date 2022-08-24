using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace BBTracker.Contracts.ViewModels
{
    public class StatsDTO
    {
        public Guid PlayerId { get; set; }
        public string Name { get; set; }
        public string Nick { get; set; }
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
        public StatsDTO()
        {

        }
        public StatsDTO(ICollection<StatsDTO> dTOs)
        {
            if (dTOs != null && dTOs.Count != 0)
            {
                PlayerId = Guid.Empty;
                Points = dTOs.Sum(x => x.Points);
                FieldGoalsMade2p = dTOs.Sum(x => x.FieldGoalsMade2p);
                FieldGoalsMissed2p = dTOs.Sum(x => x.FieldGoalsMissed2p);
                FieldGoalsMade3p = dTOs.Sum(x => x.FieldGoalsMade3p);
                FieldGoalsMissed3p = dTOs.Sum(y => y.FieldGoalsMissed3p);
                Assists = dTOs.Sum(x => x.Assists);
                Rebounds = dTOs.Sum(x => x.Rebounds);
                DefensiveRebounds = dTOs.Sum(x => x.DefensiveRebounds);
                OffensiveRebounds = dTOs.Sum(x => x.OffensiveRebounds);
                Steals = dTOs.Sum(x => x.Steals);
                Blocks = dTOs.Sum(x => x.Blocks);
                Turnovers = dTOs.Sum(x => x.Turnovers);
            }            
        }
    }
}
