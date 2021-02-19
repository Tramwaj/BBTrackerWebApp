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
using System.Security.Claims;
using BBTracker.App.Mappers;
using BBTracker.App.Services;

namespace BBTracker.App
{
    public class GameService : IGameService
    {
        //todo: !!! Services z repo
        private readonly GameRepo _gameRepo;
        private readonly PlayerRepo _playerRepo;
        private readonly UserService _userRepo;
        private readonly IPlayParser _playReader;
        private readonly IPlayingTimeService _playingTimeService;
        public GameService(IPlayParser playReader, GameRepo gameRepo, PlayerRepo playerRepo, UserService userRepo, PlayingTimeService playerService) //to do DI jeszcze wrzucić
        {
            _gameRepo = gameRepo;
            _playerRepo =  playerRepo;
            _userRepo =  userRepo;
            _playReader = playReader;
            _playingTimeService = playerService;
        }
        public async Task<NewGameViewModel> NewGame(GamePlayersVM players, string userName)
        {
            var _user = await _userRepo.GetUser(userName);
            var _game = new Game(Guid.NewGuid(), _user.Id, DateTime.Now);
            await _gameRepo.StartGameAsync(_game);
            foreach (var player in players.Players)
            {
                await _playingTimeService.AddSubstitution(new AddSubstitutionViewModel(_game.Id,player.Id,true));
                await AddPlayerToGame(new AddPlayerToGameVM(_game.Id, player.Id, player.TeamB));
            }

            return new NewGameViewModel(_game.Id, _game.Start);
        }

        //todo: from here should come the signal to refresh Player's Stats next time it's needed
        public async Task<bool> AddPlayerToGame(AddPlayerToGameVM addPlayerToGameDTO)//(Guid playerId, Guid gameId, bool teamB)
        {
            var _player = await _playerRepo.GetPlayerAsync(addPlayerToGameDTO.PlayerId);
            var _game = await _gameRepo.GetGameByIdAsync(addPlayerToGameDTO.GameId);
            if (_player == null || _game == null) return false;
            else
            {                
                await _gameRepo.AddPlayerGame(
                    new PlayerGame(_player,_game,addPlayerToGameDTO.TeamB)
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
        public async Task<SetupGameViewModel> GetGameViewModel(ClaimsPrincipal user)
        {
            string userName = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var players = await _playerRepo.GetPlayersAsync();

            return new SetupGameViewModel
            {
                Players = players
                .Select(p => Mapper.CreateFullPlayerDTO(p))
                .ToList()
            };
        }

        public async Task<bool> AddPlays(AddPlaysToGameViewModel playsVM)
        {
            var _game = await _gameRepo.GetGameByIdAsync(playsVM.GameId);
            if (_game.End != null) return false;
                //todo: make async
            var _plays = _playReader.ReadPlaysBundle(playsVM.playDTOs, playsVM.GameId);
            //TODO: check if game ended
            if (_plays == null) return await Task.FromResult(false);
            
            else
            {
                foreach (var _play in _plays)
                {
                   // await 
                    await _gameRepo.AddPlay(_play);
                }
                    return await Task.FromResult(true);
            }

        }


        public async Task<bool> CancelPlay(Guid playId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> StartGame()
        {
            throw new NotImplementedException();
        }
    }
}
