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
        internal static StatsDTO DTOFromStats(Stats stats)
        {
            var dto = new StatsDTO();
            dto.PlayerId = stats.Id;
            dto.Points = stats.Points;
            dto.FieldGoalsMade2p = stats.FieldGoalsMade2p;
            dto.FieldGoalsMissed2p = stats.FieldGoalsMissed2p;
            dto.FieldGoalsMade3p = stats.FieldGoalsMade3p;
            dto.FieldGoalsMissed3p = stats.FieldGoalsMissed3p;
            dto.Assists = stats.Assists;
            dto.Rebounds = stats.TotalRebounds;
            dto.DefensiveRebounds = stats.DefensiveRebounds;
            dto.OffensiveRebounds = stats.OffensiveRebounds;
            dto.Steals = stats.Steals;
            dto.Blocks = stats.Blocks;
            dto.Turnovers = stats.Turnovers;
            return dto;
    }
      
    }
}
