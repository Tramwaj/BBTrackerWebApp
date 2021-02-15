using BBTracker.Contracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BBTracker.Contracts.Services
{
    public interface IGameService
    {
        public Task<NewGameViewModel> NewGame();
        public Task<bool> AddPlayerToGame(AddPlayerToGameDTO player);
        public Task<bool> AddPlayersToGame(ICollection<AddPlayerToGameDTO> players);
        public Task<bool> EndGame(Guid gameId);
    }

    public class GameVM
    {
        public Guid Id { get; set; }
        public ICollection<PlayerDTO> TeamA { get; set; }
        public ICollection<PlayerDTO> TeamB { get; set; }
    }
    
}
