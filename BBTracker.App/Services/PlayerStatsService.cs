using BBTracker.App.Services.StatsCounting;
using BBTracker.Contracts.Services;
using BBTracker.Contracts.ViewModels;
using BBTracker.Model.Models;
using BBTracker.Persistence.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBTracker.App.Services
{
    public class PlayerStatsService : IPlayerStatsService
    {
        private readonly PlayRepo _repo;
        private readonly PlayerRepo _playerRepo;

        public PlayerStatsService(PlayRepo repo, PlayerRepo playerRepo)
        {
            _repo = repo;
            _playerRepo = playerRepo;
        }

        public async Task<ICollection<PlayerStatsDTO>> GetPlayersStats(bool per30mins)
        {
            var plays = await _repo.GetAllPlays();

            var stats = GeneralStatsCounter.GetPlayerStats(plays);
            ICollection<Player> players = await _playerRepo.GetMultiplePlayersByIdsAsync(stats.Select(s => s.Id));

            ICollection<PlayerStatsDTO> result = new List<PlayerStatsDTO>();
            foreach (var stat in stats)
            {
                if (!per30mins)
                {
                    result.Add(new PlayerStatsDTO
                    {
                        Name = players.FirstOrDefault(p => p.Id == stat.Id).Name,
                        Nick = players.FirstOrDefault(p => p.Id == stat.Id).Nick,
                        City = players.FirstOrDefault(p => p.Id == stat.Id).City,
                        Points = stat.Points,
                        Eff2p = stat.TwoPointsPercentage,
                        Made2p = stat.FieldGoalsMade2p,
                        Attempted2p = stat.Attempted2p,
                        Eff3p = stat.ThreePointsPercentage,
                        Made3p = stat.FieldGoalsMade3p,
                        Attempted3p = stat.Attempted3p,
                        Assists = stat.Assists,
                        DefReb = stat.DefensiveRebounds,
                        OffReb = stat.OffensiveRebounds,
                        Steals = stat.Steals,
                        Turnovers = stat.Turnovers,
                        Minutes = stat.GetPlayingTime().TotalMinutes
                    });
                }
                else
                {
                    double factor = stat.GetPlayingTime().TotalMinutes / 30;
                    result.Add(new PlayerStatsDTO
                    {
                        Name = players.FirstOrDefault(p => p.Id == stat.Id).Name,
                        Nick = players.FirstOrDefault(p => p.Id == stat.Id).Nick,
                        City = players.FirstOrDefault(p => p.Id == stat.Id).City,
                        Points = stat.Points * factor,
                        Eff2p = stat.TwoPointsPercentage * factor,
                        Eff3p = stat.ThreePointsPercentage * factor,
                        Assists = stat.Assists * factor,
                        DefReb = stat.DefensiveRebounds * factor,
                        OffReb = stat.OffensiveRebounds * factor,
                        Steals = stat.Steals * factor,
                        Turnovers = stat.Turnovers * factor,
                        Minutes = stat.GetPlayingTime().TotalMinutes
                    });
                }
            }
            return result.ToList();
        }
    }
}
