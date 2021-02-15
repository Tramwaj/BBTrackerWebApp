using BBTracker.Persistence.Repos;
using BBTracker.Contracts.Services;
using BBTracker.Model.Models;
using Microsoft.Data.SqlClient.DataClassification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using BBTracker.Contracts.ViewModels;

namespace BBTracker.App
{
    public class GameService : IGameService
    {

        private readonly GameRepo _gameRepository;
        private readonly PlayerRepo _playerRepository;
        public GameService() //to do DI jeszcze wrzucić
        {
            _gameRepository = new GameRepo();
            _playerRepository = new PlayerRepo();
        }
        public async Task<NewGameViewModel> NewGame(NewGamePlayersVM players)
        {
            var game = new Game(Guid.NewGuid(), DateTime.Now);
            await _gameRepository.Add(game);
            foreach (var player in players.Players)
            {
                await AddPlayerToGame(new AddPlayerToGameVM(game.Id, player.Id, player.TeamB));
            }

            return new NewGameViewModel(game.Id, game.Start);
        }

        public async Task<bool> AddPlayerToGame(AddPlayerToGameVM addPlayerToGameDTO)//(Guid playerId, Guid gameId, bool teamB)
        {
            var player = await _playerRepository.GetPlayerAsync(addPlayerToGameDTO.PlayerId);
            var game = await _gameRepository.GetGameByIdAsync(addPlayerToGameDTO.GameId);
            if (player == null || game == null) return false;
            else
            {
                await _gameRepository.AddPlayerGame(
                    
                    player.Id,
                    game.Id,
                    addPlayerToGameDTO.TeamB
                    );
                return true;

            }            
        }
        public async Task<bool> EndGame(Guid gameId)
        {
            //TODO: podliczenie wyniku
            var game = await _gameRepository.GetGameByIdAsync(gameId);
            if (game == null)
                return false;
            var end = DateTime.Now;
            await _gameRepository.UpdateEndTime(gameId,end);
            return true;
        }

        public async Task<bool> AddPlayersToGame(ICollection<AddPlayerToGameVM> players)
        {
            foreach (var playerDTO in players)
            {
                if (!(await AddPlayerToGame(playerDTO)))
                    return false;
            }
            return true;
        }

        public Task<bool> StartGame()
        {
            throw new NotImplementedException();
        }

        public Task<bool> CancelPlay(Guid playId)
        {
            throw new NotImplementedException();
        }
    }
}
