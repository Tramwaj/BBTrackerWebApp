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
        public Task<NewGameViewModel> NewGame(GamePlayersVM players, IEnumerable<Claim> userClaims);  
        //The following is redundant if NewGame starts the game
        public Task<bool> StartGame(); 
        public Task<bool> AddPlayerToGame(PlayerToGameVM player);
        public Task<bool> AddPlayersToGame(ICollection<PlayerToGameVM> players);
        public Task<GameViewModel> EndGame(Guid gameId);

        public Task<bool> AddPlays(AddPlaysToGameViewModel playsVM);
        public Task<bool> CancelPlay(Guid playId);
        Task<SetupGameViewModel> GetGameViewModel(ClaimsPrincipal user);
    }

    public class GameVM
    {
        public Guid Id { get; set; }
        public ICollection<FullPlayerDTO> TeamA { get; set; }
        public ICollection<FullPlayerDTO> TeamB { get; set; }
    }
    
}
