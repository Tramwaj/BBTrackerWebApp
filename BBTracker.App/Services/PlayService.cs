﻿using BBTracker.App.Interfaces;
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
    public class PlayService : IPlayService
    {
        private readonly PlayRepo _playRepo;
        public PlayService(PlayRepo playRepo)
        {
            _playRepo = playRepo;
        }

        public async Task<bool> AddPlays(ICollection<Play> plays)
        {
            foreach (var _play in plays)
            {
                if (!await PlayIsPossible(_play))
                    return false;
            }
            foreach (var _play in plays)
            {
                await AddPlay(_play);
            }
            return await Task.FromResult(true);
        }

        //todo: exception if not possible?
        public async Task AddPlay(Play play)
        {
            await _playRepo.AddPlay(play);
        }

        private async Task<bool> PlayIsPossible(Play play)
        {
            if (play is Substitution && ((Substitution)play).SubbedIn)
                return !await PlayerIsOnTheFloor(play.PlayerId, play.GameId);
            else
                return await PlayerIsOnTheFloor(play.PlayerId, play.GameId);
        }

        private async Task<bool> PlayerIsOnTheFloor(Guid playerId, Guid gameId)
        {
            var _playerSubs = (await _playRepo.GetPlayerGameSubstitutions(playerId, gameId));

            if (!_playerSubs.Any())
                return false;
            if (!_playerSubs.LastOrDefault().SubbedIn)
                return false;
            return true;
        }

    }
}
