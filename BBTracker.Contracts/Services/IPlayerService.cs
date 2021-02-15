using BBTracker.Contracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BBTracker.Contracts.Services
{
    public interface IPlayerService
    {        
        Task<ICollection<PlayerDTO>> GetAllPlayersDTO();
        Task<bool> AddPlayer(PlayerDTO player);
        Task<bool> EditPlayer(Guid id, PlayerDTO player);
        Task<PlayerDTO> GetPlayerById(Guid id);
        Task<bool> RemovePlayer(Guid id);
    }
}
