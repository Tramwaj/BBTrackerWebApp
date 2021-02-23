using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBTracker.Contracts.ViewModels
{
    public class NewGameViewModel
    {
        public Guid Id { get; set; }
        public DateTime Start { get; set; }
        public ICollection<Guid> PlayersTeamA { get; set; }
        public ICollection<Guid> PlayersTeamB { get; set; }

        public NewGameViewModel(Guid id, DateTime start, ICollection<Guid> teamA=null, ICollection<Guid> teamB = null)
        {
            Id = id;
            Start = start;
            PlayersTeamA = teamA;
            PlayersTeamB = teamB;
        }
    }
}
