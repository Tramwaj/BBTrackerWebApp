using BBTracker.Contracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BBTracker.Contracts.Services
{
    public interface IGameService
    {
        public Task<NewGameViewModel> NewGame(ICollection<GamePlayerDTO> players, IEnumerable<Claim> userClaims);  
        //The following is redundant if NewGame starts the game
        public Task<bool> StartGame(); 
        public Task<bool> AddPlayerToGame(PlayerToGameVM player);
        public Task<bool> AddPlayersToGame(ICollection<PlayerToGameVM> players);
        public Task<GameStatsViewModel> EndGame(Guid gameId);

        public Task<bool> AddPlays(AddPlaysToGameViewModel playsVM);
        public Task<bool> AddPlaysFromVerbs(string[] playVerbs, Guid gameId);
        public Task<bool> CancelPlay(Guid playId);
        Task<SetupGameViewModel> GetAvailablePlayers(ClaimsPrincipal user);
        Task<GameStatsViewModel> GetGameState(Guid gameId);
    }

    public class GameVM
    {
        public Guid Id { get; set; }
        public ICollection<FullPlayerDTO> TeamA { get; set; }
        public ICollection<FullPlayerDTO> TeamB { get; set; }
    }
    
}
