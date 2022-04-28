using BBTracker.App.Mappers;
using BBTracker.Contracts.ViewModels;
using BBTracker.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBTracker.App.Services.StatsCounting
{
    internal static class GameStatsCounter
    {
        public static GameStatsViewModel CreateGameStats(Game game, ICollection<Play> plays)
        {
            var teamAStats = plays
                            .Where(p => !p.IsTeamB)
                            .Select(p => p.PlayerId)
                            .Distinct()
                            .Select(p => new Stats(p))
                            .ToList();

            var teamBStats = plays
                .Where(p => p.IsTeamB)
                .Select(p => p.PlayerId)
                .Distinct()
                .Select(p => new Stats(p))
                .ToList();
            foreach (Play play in game.Plays.Where(p => !p.IsTeamB))
            {
                teamAStats.First(s => s.Id == play.PlayerId).ResolvePlay(play);
            }
            foreach (Play play in game.Plays.Where(p => p.IsTeamB))
            {
                teamBStats.First(s => s.Id == play.PlayerId).ResolvePlay(play);
            }
            var teamAStatsDTO = teamAStats.Select(ts => Mapper.DTOFromStats(ts)).ToList();
            var teamBStatsDTO = teamBStats.Select(ts => Mapper.DTOFromStats(ts)).ToList();

            return new GameStatsViewModel(
                game.Id,
                game.Start,
                (DateTime)game.End,
                new StatsDTO(teamAStatsDTO),
                new StatsDTO(teamBStatsDTO),
                teamAStatsDTO,
                teamBStatsDTO
                );
        }
    }
}
