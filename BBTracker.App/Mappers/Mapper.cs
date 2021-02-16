using BBTracker.Contracts.ViewModels;
using BBTracker.Model.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BBTracker.App.Mappers
{
    class Mapper
    {
        internal static FullPlayerDTO CreateFullPlayerDTO(Player player)
        {
            return new FullPlayerDTO(player.Id, player.Name, player.Nick, player.City);
        }
        internal static Player UpdatePlayerFromDTO(Player player, FullPlayerDTO playerDTO)
        {
            player.Name = playerDTO.Name;
            player.Nick = playerDTO.Nick;
            player.City = playerDTO.City;
            return player;
        }
      
    }
}
