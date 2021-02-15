﻿using BBTracker.Persistence.Repos;
using BBTracker.App.Mappers;
using BBTracker.Contracts.Services;
using BBTracker.Contracts.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BBTracker.Model.Models;

namespace BBTracker.App
{
    public class PlayerServiceDB : IPlayerService
    {
        private readonly PlayerRepo _playersRepository;
        public PlayerServiceDB()
        {
            _playersRepository = new PlayerRepo();
        }

        public async Task<PlayerDTO> GetPlayerById(Guid id)
        {
            var entity = await _playersRepository.GetPlayerAsync(id);
            return new PlayerDTO(entity.Id, entity.Name, entity.Nick, entity.City);
        }
        public async Task<ICollection<PlayerDTO>> GetAllPlayersDTO()
        {
            var _players = await _playersRepository.GetPlayersAsync();
            return await Task.FromResult(
                _players.Select(p => new PlayerDTO(p.Id, p.Name, p.Nick, p.City))
                .ToList());
        }
        public async Task<bool> AddPlayer(PlayerDTO player)
        {
            var players = await _playersRepository.GetPlayersAsync();
            if (players.Any(x => x.Name == player.Name && x.Nick == player.Nick))
                return false;
            else
            {
                await _playersRepository.Create(new Player(
                    Guid.NewGuid(),
                    player.Name,
                    player.Nick,
                    player.City
                    ));
                return true;
            }
        }

        public async Task<bool> RemovePlayer(Guid id)
        {
            var player = await _playersRepository.GetPlayerAsync(id);
            if (player == null)
            {
                return false;
            }
            else
            {
                await _playersRepository.Delete(player);
                return true;
            }
        }

        public async Task<bool> EditPlayer(Guid id, PlayerDTO playerDTO)
        {
            var entity = await _playersRepository.GetPlayerAsync(id);
            if (entity == null)
                return false;
            else
            {
                await _playersRepository.Update(Mapper.UpdatePlayerFromDTO(entity, playerDTO));
                return true;
            }
        }
    }
}
