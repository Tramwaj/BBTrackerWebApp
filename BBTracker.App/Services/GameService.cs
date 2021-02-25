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
using BBTracker.App.Interfaces;

namespace BBTracker.App.Services
{
    public class GameService : IGameService
    {
        //todo: repo do DI

        private readonly GameRepo _gameRepo;
        private readonly PlayerRepo _playerRepo;
        private readonly IUserService _userRepo;
        private readonly IPlayParser _playReader;
        private readonly IPlayService _playsService;

        public GameService(IPlayParser playReader, GameRepo gameRepo, PlayerRepo playerRepo, IUserService userRepo, IPlayService playerService)
        {
            _gameRepo = gameRepo;
            _playerRepo = playerRepo;
            _userRepo = userRepo;
            _playReader = playReader;
            _playsService = playerService;
        }

        public async Task<NewGameViewModel> NewGame(GamePlayersVM players, IEnumerable<Claim> userClaims)
        {
            //todo: check if any players are in game??
            string userName = userClaims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            User _user = await GetUser(userName);
            var _game = new Game(Guid.NewGuid(), _user.Id, DateTime.Now);
            await _gameRepo.NewGameAsync(_game);
            List<Guid> _teamA = players.Players.Where(p => !p.TeamB)
                                                    .Select(p => p.Id)
                                                    .ToList();
            List<Guid> _teamB = players.Players.Where(p => p.TeamB)
                                                    .Select(p => p.Id)
                                                    .ToList();


            foreach (var player in players.Players)
            {
                await AddPlayerToGame(new PlayerToGameVM(_game.Id, player.Id, player.TeamB));

                if (player.OnCourt)
                {
                    var _sub = new Substitution(Guid.NewGuid(), DateTime.Now, player.TeamB, player.Id, _game.Id, true);
                    await _playsService.AddPlay(_sub);
                }
            }

            return new NewGameViewModel(_game.Id, _game.Start, _teamA, _teamB);
        }

        private async Task<User> GetUser(string userName)
        {
            return await _userRepo.GetUser(userName);
        }

        //todo: from here should come the signal to refresh Player's Stats next time it's needed
        public async Task<bool> AddPlayerToGame(PlayerToGameVM PlayerGameDTO)//(Guid playerId, Guid gameId, bool teamB)
        {
            var _player = await GetPlayer(PlayerGameDTO.PlayerId);
            var _game = await GetGame(PlayerGameDTO.GameId);
            if (_player == null || _game == null) return false;
            else
            {
                await _gameRepo.AddPlayerGame(
                    new PlayerGame(_player.Id, _game.Id, PlayerGameDTO.TeamB)
                    );
                return true;
            }
        }

        public async Task<GameStatsViewModel> EndGame(Guid gameId)
        {
            //TODO: podliczenie wyniku
            //TODO: sub out players from the game
            var game = await GetGame(gameId);
            if (game == null)
                return null;
            if (game.End != null)
                return null;
            var end = DateTime.Now;
            await _gameRepo.UpdateEndTime(gameId, end);
            return await CreateGameViewModel(game);
        }

        private async Task<GameStatsViewModel> CreateGameViewModel(Game game)
        {
            var plays = await _gameRepo.GetPlaysByGameId(game.Id);

            var teamAStats = plays
                .Where(p => !p.IsTeamB)
                .Select(p => p.PlayerId)
                .Distinct()
                .Select(p => new Stats(p))
                .ToList();

            var teamBStats = plays
                .Where(p => p.IsTeamB)
                .Select(p => p.PlayerId)
                .Distinct()
                .Select(p => new Stats(p))
                .ToList();
            foreach (Play play in game.Plays.Where(p => !p.IsTeamB))
            {
                teamAStats.First(s => s.Id == play.PlayerId).ResolvePlay(play);
            }
            foreach (Play play in game.Plays.Where(p => p.IsTeamB))
            {
                teamBStats.First(s => s.Id == play.PlayerId).ResolvePlay(play);
            }
            //return new Game
            return new GameStatsViewModel(
                game.Id,
                game.Start,
                (DateTime)game.End,
                null, null,
                teamAStats.Select(ts => Mapper.DTOFromStats(ts)).ToList(),
                teamBStats.Select(ts => Mapper.DTOFromStats(ts)).ToList()
                );
        }

        public async Task<bool> AddPlayersToGame(ICollection<PlayerToGameVM> players)
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
            var _game = await GetGame(playsVM.GameId);
            if (_game.End != null) return false;
            //todo: make async
            var _plays = _playReader.ReadPlaysBundle(playsVM.playDTOs, playsVM.GameId);

            if (_plays == null) return await Task.FromResult(false);
            if (await _playsService.AddPlays(_plays) == false)
                return await Task.FromResult(false);
            return await Task.FromResult(true);
        }

        private async Task<Player> GetPlayer(Guid id)
        {
            return await _playerRepo.GetPlayerAsync(id);
        }
        private async Task<Game> GetGame(Guid id)
        {
            return await _gameRepo.GetGameByIdAsync(id);
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
