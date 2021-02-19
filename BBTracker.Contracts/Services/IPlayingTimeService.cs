using BBTracker.Contracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBTracker.Contracts.Services
{
    public interface IPlayingTimeService
    {
        public Task<bool> AddSubstitution(AddSubstitutionViewModel subvm);
        public Task<bool> PlayerIsOnTheFloor(Guid playerId, Guid gameId);
    }
}
