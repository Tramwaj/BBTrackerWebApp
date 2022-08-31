using BBTracker.Contracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBTracker.Contracts.Services
{
    public interface IPlayerStatsService
    {
        public Task<ICollection<PlayerStatsDTO>> GetPlayersStats(bool per30mins);
    }
}
