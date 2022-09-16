using BBTracker.Persistence.Repos;
using BBTracker.Contracts.Services;
using BBTracker.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BBTracker.Contracts.ViewModels;
using System.Security.Claims;
using BBTracker.App.Mappers;
using BBTracker.App.Interfaces;
using BBTracker.App.Services.StatsCounting;

namespace BBTracker.App.Services
{
    public class GameService : IGameService
    {
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

        public async Task<NewGameViewModel> NewGame(ICollection<GamePlayerDTO> players, IEnumerable<Claim> userClaims)
        {
            User _user = await GetUserFromClaims(userClaims);
            Game _game = await CreateAndGetNewGame(_user);
            await AddPlayersToGame(players, _game);
            return CreateNewGamemodel(players, _game);
        }


        private async Task<User> GetUserFromClaims(IEnumerable<Claim> userClaims)
        {
            return await GetUser
                (userClaims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
        }

        private async Task<Game> CreateAndGetNewGame(User _user)
        {
            var _game = new Game(Guid.NewGuid(), _user.Id, DateTime.Now);
            await _gameRepo.NewGameAsync(_game);
            return _game;
        }

        private async Task<User> GetUser(string userName) => await _userRepo.GetUser(userName);

        private async Task AddPlayersToGame(ICollection<GamePlayerDTO> players, Game _game)
        {
            foreach (var player in players)
            {
                await AddPlayerToGame(new PlayerToGameVM(_game.Id, player.Id, player.TeamB));

                if (player.OnCourt)
                {
                    await SubInStartingPlayer(_game, player);
                }
            }
        }

        private async Task SubInStartingPlayer(Game _game, GamePlayerDTO player)
        {
            var _sub = new Substitution(
                Guid.NewGuid(),
                DateTime.Now,
                player.TeamB,
                player.Id,
                _game.Id,
                true);
            await _playsService.AddPlay(_sub);
        }

        private NewGameViewModel CreateNewGamemodel(ICollection<GamePlayerDTO> players, Game _game)
        {
            List<Guid> _teamA = GetTeamGuids(players, false);
            List<Guid> _teamB = GetTeamGuids(players, true);

            return new NewGameViewModel(_game.Id, _game.Start, _teamA, _teamB);
        }

        private static List<Guid> GetTeamGuids(ICollection<GamePlayerDTO> players, bool TeamB)
        {
            return players.Where(p => p.TeamB == TeamB)
                .Select(p => p.Id)
                .ToList();
        }


        //todo: from here should come the signal to refresh Player's Stats next time it's needed
        public async Task<bool> AddPlayerToGame(PlayerToGameVM PlayerGameDTO)
        {
            var _player = await GetPlayer(PlayerGameDTO.PlayerId);
            var _game = await GetGame(PlayerGameDTO.GameId);
            if (PlayerOrGameMissing(_player, _game)) return false;
            else
            {
                await AddPlayerGame(PlayerGameDTO);
                return true;
            }
        }

        private async Task AddPlayerGame(PlayerToGameVM PlayerGameDTO)
        {
            await _gameRepo.AddPlayerGame(
                new PlayerGame(PlayerGameDTO.PlayerId, PlayerGameDTO.GameId, PlayerGameDTO.TeamB)
                );
        }

        private static bool PlayerOrGameMissing(Player _player, Game _game)
        {
            return _player == null || _game == null;
        }

        public async Task<GameStatsViewModel> EndGame(Guid gameId)
        {
            //TODO: podliczenie wyniku
            //TODO: sub out players from the game
            var game = await GetGame(gameId);
            //var plays = await _gameRepo.GetPlaysByGameId(gameId);
            if (game == null || game.End != null)
                return null;

            var end = DateTime.Now;
            await _gameRepo.UpdateEndTime(gameId, end);
            return await CreateGameViewModel(game);
        }

        private async Task<GameStatsViewModel> CreateGameViewModel(Game game)
        {
            var plays = await _gameRepo.GetPlaysByGameId(game.Id);
            if (plays.Count == 0)
            {
                //todo: na szybko
                var playerGames = await _gameRepo.GetPlayerGamesByGameId(game.Id);
                List<StatsDTO> teamA = new();
                List<StatsDTO> teamB = new();
                foreach (var playerGame in playerGames)
                {
                    var player = await _playerRepo.GetPlayerAsync(playerGame.PlayerId);
                    var playerStats = new StatsDTO
                    {
                        PlayerId = playerGame.PlayerId,
                        Name = player.Name,
                        Nick = player.Nick
                    };
                    if (!playerGame.TeamB)
                        teamA.Add(playerStats);
                    else
                        teamB.Add(playerStats);
                }
                return new GameStatsViewModel
                {
                    Id = game.Id,
                    TeamA = teamA,
                    TeamB = teamB,
                    Start = game.Start
                };
            }

            var stats = GameStatsCounter.CreateGameStats(game, plays);
            foreach (var item in stats.TeamA)
            {
                var data = await _playerRepo.GetPlayerAsync(item.PlayerId);
                item.Name = data.Name;
                item.Nick = data.Nick;
            }
            foreach (var item in stats.TeamB)
            {
                var data = await _playerRepo.GetPlayerAsync(item.PlayerId);
                item.Name = data.Name;
                item.Nick = data.Nick;
            }
            return stats;
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

        public async Task<SetupGameViewModel> GetAvailablePlayers(ClaimsPrincipal user)
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
            if (_game.End != null) return await Task.FromResult(false);
            //todo: make async
            var _plays = _playReader.ReadPlaysBundle(playsVM.playDTOs, playsVM.GameId);

            if (_plays == null) return await Task.FromResult(false);
            if (await _playsService.AddPlays(_plays) == false)
                return await Task.FromResult(false);
            return true;
        }

        public async Task<bool> AddPlaysFromVerbs(string[] playVerbs, Guid gameId)
        {
            if (playVerbs == null || playVerbs.Length == 0) return await Task.FromResult(false);
            ICollection<Play> plays = NewPlayParser.ReadPlays(playVerbs, gameId);
            if (plays == null || plays.Count==0) 
                return await Task.FromResult(false);
            if (await _playsService.AddPlays(plays) == false)
                return await Task.FromResult(false);
            return await Task.FromResult(true);

        }

        private async Task<Player> GetPlayer(Guid id) => await _playerRepo.GetPlayerAsync(id);

        private async Task<Game> GetGame(Guid id) => await _gameRepo.GetGameByIdAsync(id);

        public async Task<bool> CancelPlay(Guid playId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> StartGame()
        {
            throw new NotImplementedException();
        }

        public async Task<GameStatsViewModel> GetGameState(Guid gameId)
        {
            var game = await GetGame(gameId);
            return await CreateGameViewModel(game);
        }
    }
}
