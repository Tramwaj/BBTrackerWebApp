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

        private readonly GameRepo _gameRepo;
        private readonly PlayerRepo _playerRepo;
        private readonly UserService _userRepo;
        public GameService() //to do DI jeszcze wrzucić
        {
            _gameRepo = new GameRepo();
            _playerRepo = new PlayerRepo();
            _userRepo = new UserService();
        }
        public async Task<NewGameViewModel> NewGame(NewGamePlayersVM players, string userName)
        {
            var _user = await _userRepo.GetUser(userName);
            var _game = new Game(Guid.NewGuid(), _user.Id, DateTime.Now);
            throw new NotImplementedException();
            await _gameRepo.Add(_game);
            foreach (var player in players.Players)
            {
                await AddPlayerToGame(new AddPlayerToGameVM(_game.Id, player.Id, player.TeamB));
            }

            return new NewGameViewModel(_game.Id, _game.Start);
        }

        public async Task<bool> AddPlayerToGame(AddPlayerToGameVM addPlayerToGameDTO)//(Guid playerId, Guid gameId, bool teamB)
        {
            var _player = await _playerRepo.GetPlayerAsync(addPlayerToGameDTO.PlayerId);
            var _game = await _gameRepo.GetGameByIdAsync(addPlayerToGameDTO.GameId);
            if (_player == null || _game == null) return false;
            else
            {
                await _gameRepo.AddPlayerGame(

                    _player.Id,
                    _game.Id,
                    addPlayerToGameDTO.TeamB
                    );
                return true;

            }
        }
        public async Task<bool> EndGame(Guid gameId)
        {
            //TODO: podliczenie wyniku
            var game = await _gameRepo.GetGameByIdAsync(gameId);
            if (game == null)
                return false;
            var end = DateTime.Now;
            await _gameRepo.UpdateEndTime(gameId, end);
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

        public Task<SetupGameViewModel> GetGameViewModel(string userName)
        {
            throw new NotImplementedException();
        }
    }
}
