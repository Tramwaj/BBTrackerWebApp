using BBTracker.Contracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BBTracker.Contracts.Services
{
    public interface IGameService
    {
        public Task<NewGameViewModel> NewGame(NewGamePlayersVM playerIDs, string userName);  
        //The following is redundant if NewGame starts the game
        public Task<bool> StartGame(); 
        public Task<bool> AddPlayerToGame(AddPlayerToGameVM player);
        public Task<bool> AddPlayersToGame(ICollection<AddPlayerToGameVM> players);
        public Task<bool> EndGame(Guid gameId);

        //public Task<bool> AddPlays(AddPlaysToGameDTO playsDTO);
        public Task<bool> CancelPlay(Guid playId);
        Task<SetupGameViewModel> GetGameViewModel(string userName);
    }

    public class GameVM
    {
        public Guid Id { get; set; }
        public ICollection<FullPlayerDTO> TeamA { get; set; }
        public ICollection<FullPlayerDTO> TeamB { get; set; }
    }
    
}
