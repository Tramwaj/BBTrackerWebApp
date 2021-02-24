using BBTracker.App.Interfaces;
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
    public class PlayService :IPlayService
    {
        private readonly PlayRepo _playRepo;
        public PlayService(PlayRepo subRepo)
        {
            _playRepo = subRepo;
        }                
        
        private async Task<bool> PlayerIsOnTheFloor(Guid playerId, Guid gameId)
        {
            var _playerSubs = (await _playRepo.GetPlayerGameSubstitutions(playerId,gameId));
            if (!_playerSubs.Any()) 
                return false;
            if (!_playerSubs.LastOrDefault().SubbedIn)
                return false;
            else return true;
        }
        public async Task<bool> PlayIsPossible(Play play)
        {
            if (play is Substitution && ((Substitution)play).SubbedIn)
                return !await PlayerIsOnTheFloor(play.PlayerId, play.GameId);
            else
                return await PlayerIsOnTheFloor(play.PlayerId, play.GameId);
        }
    }
}
