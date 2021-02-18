using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BBTracker.Persistence;
using BBTracker.Contracts.ViewModels;
using BBTracker.Model.Models;
using BBTracker.Contracts.Services;
using System.Security.Cryptography.X509Certificates;
using BBTracker.App.Mappers;

namespace BBTracker.App
{
    public class PlayerServiceNoDB// : IPlayerService
    {
        private static ICollection<Player> allPlayers = new[]
           {
                new Player(Guid.NewGuid(), "Michał", "Magic"),
                new Player(Guid.NewGuid(), "Ola", "Tolka"),
                new Player(Guid.NewGuid(), "Filip", "OłkiPołk")
            };
        //private readonly PlayerRepo _playerRepo;
        public PlayerServiceNoDB()
        {
            
        }

        public bool AddPlayer(FullPlayerDTO player)
        {
            throw new NotImplementedException();
        }

        public bool RemovePlayer(Guid id)
        {
            throw new NotImplementedException();
        }

        public bool EditPlayer(FullPlayerDTO player)
        {
            throw new NotImplementedException();
        }

        public ICollection<FullPlayerDTO> GetAllPlayersDTO()
        {
            return allPlayers               
                .Where(x => x.Deleted == false)
                .Select(x => Mapper.CreateFullPlayerDTO(x))
                .ToList();
        }

        public FullPlayerDTO GetPlayerById(Guid id) => Mapper.CreateFullPlayerDTO(
            allPlayers.FirstOrDefault(x => x.Id == id));

        public bool EditPlayer(Guid id, FullPlayerDTO player)
        {
            throw new NotImplementedException();
        }
    }
}
