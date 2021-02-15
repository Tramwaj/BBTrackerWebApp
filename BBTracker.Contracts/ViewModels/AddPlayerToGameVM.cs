using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBTracker.Contracts.ViewModels
{
    public class AddPlayerToGameVM
    {
        public Guid GameId { get; set; }
        public Guid PlayerId { get; set; }
        public bool TeamB { get; set; }
        public AddPlayerToGameVM()
        {

        }

        public AddPlayerToGameVM(Guid gameId, Guid playerId, bool teamB)
        {
            GameId = gameId;
            PlayerId = playerId;
            TeamB = teamB;
        }
    }
    
}
