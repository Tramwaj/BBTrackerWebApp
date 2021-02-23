using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBTracker.Contracts.ViewModels
{
    public class PlayerToGameVM
    {
        public Guid GameId { get; set; }
        public Guid PlayerId { get; set; }
        public bool TeamB { get; set; }
        public PlayerToGameVM()
        {

        }

        public PlayerToGameVM(Guid gameId, Guid playerId, bool teamB)
        {
            GameId = gameId;
            PlayerId = playerId;
            TeamB = teamB;
        }
    }
    
}
