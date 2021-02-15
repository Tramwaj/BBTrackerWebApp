using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BBTracker.Persistence;
using BBTracker.Contracts.ViewModels;
using BBTracker.Model;
using BBTracker.Contracts.Services;
using System.Security.Cryptography.X509Certificates;
using BBTracker.App.Mappers;

namespace BBTracker.App
{
    public class PlayerService// : IPlayerService
    {
        private static ICollection<Player> allPlayers = new[]
           {
                new Player(Guid.NewGuid(), "Michał", "Magic"),
                new Player(Guid.NewGuid(), "Ola", "Tolka"),
                new Player(Guid.NewGuid(), "Filip", "OłkiPołk")
            };
        //private readonly PlayerRepo _playerRepo;
        public PlayerService()
        {
            
        }

        public bool AddPlayer(PlayerDTO player)
        {
            throw new NotImplementedException();
        }

        public bool RemovePlayer(Guid id)
        {
            throw new NotImplementedException();
        }

        public bool EditPlayer(PlayerDTO player)
        {
            throw new NotImplementedException();
        }

        public ICollection<PlayerDTO> GetAllPlayersDTO()
        {
            return allPlayers               
                .Where(x => x.Deleted == false)
                .Select(x => Mapper.CreatePlayerDTO(x))
                .ToList();
        }

        public PlayerDTO GetPlayerById(Guid id) => Mapper.CreatePlayerDTO(
            allPlayers.FirstOrDefault(x => x.Id == id));

        public bool EditPlayer(Guid id, PlayerDTO player)
        {
            throw new NotImplementedException();
        }
    }
}
