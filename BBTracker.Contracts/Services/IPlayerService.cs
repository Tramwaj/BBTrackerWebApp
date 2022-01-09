using BBTracker.Contracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BBTracker.Contracts.Services
{
    public interface IPlayerService
    {        
        Task<ICollection<FullPlayerDTO>> GetAllPlayersDTO();
        Task<bool> AddPlayer(CreatePlayerDTO player);
        Task<bool> EditPlayer(Guid id, FullPlayerDTO player);
        Task<FullPlayerDTO> GetPlayerById(Guid id);
        Task<bool> RemovePlayer(Guid id);
    }
}
