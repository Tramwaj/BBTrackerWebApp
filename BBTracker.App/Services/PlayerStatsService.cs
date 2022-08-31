using BBTracker.Contracts.Services;
using BBTracker.Contracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBTracker.App.Services
{
    public class PlayerStatsService : IPlayerStatsService
    {
        public async Task<ICollection<PlayerStatsDTO>> GetPlayersStats(bool per30mins)
        {
            throw new NotImplementedException();
        }
    }
}
