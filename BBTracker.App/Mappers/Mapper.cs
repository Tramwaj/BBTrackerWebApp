using BBTracker.Contracts.ViewModels;
using BBTracker.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BBTracker.App.Mappers
{
    class Mapper
    {
        internal static PlayerDTO CreatePlayerDTO(Player player)
        {
            return new PlayerDTO(player.Id, player.Name, player.Nick, player.City);
        }
        internal static Player UpdatePlayerFromDTO(Player player, PlayerDTO playerDTO)
        {
            player.Name = playerDTO.Name;
            player.Nick = playerDTO.Nick;
            player.City = playerDTO.City;
            return player;
        }
    }
}
