using BBTracker.Contracts.Services;
using BBTracker.Contracts.ViewModels;
using BBTracker.Model.Models;
using BBTracker.Persistence.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBTracker.App.Services
{
    public class PlayingTimeService : IPlayingTimeService
    {
        private readonly SubstitutionRepo _subRepo;

        public async Task<bool> AddSubstitution(AddSubstitutionViewModel subVM)
        {
            //todo:timespan from game start in controller?
            if (await SubstitutionIsPossible(subVM))
            {
                await _subRepo.AddSubstitution(new Substitution(Guid.NewGuid(), DateTime.Now, new TimeSpan(0), subVM.PlayerId, subVM.GameId, subVM.SubIn));
                return true;
            }

            else
            {
                return false;
            }

        }
        private async Task<bool> SubstitutionIsPossible(AddSubstitutionViewModel subvm)
        {

            if (await PlayerIsOnTheFloor(subvm.PlayerId,subvm.GameId)!=subvm.SubIn)
                return true;
            else
                return false;
        }
        public async Task<bool> PlayerIsOnTheFloor(Guid playerId, Guid gameId)
        {
            var _lastsub = await _subRepo.GetLastSubByPlayerGame(playerId,gameId);
            if (_lastsub == null || !_lastsub.SubbedIn)
                return false;

            else return true;
        }
    }
}
