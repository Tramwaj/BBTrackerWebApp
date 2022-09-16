using BBTracker.Contracts.ViewModels;
using BBTracker.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBTracker.App.Services.StatsCounting
{
    internal static class GeneralStatsCounter
    {
        public static ICollection<Stats> GetPlayerStats(ICollection<Play> plays)
        {
            ICollection<Stats> stats = new List<Stats>();
            foreach (var play in plays)
            {
                if (!stats.Any(x => x.Id == play.PlayerId))
                    stats.Add(new Stats(play.PlayerId));
                stats.First(s => s.Id == play.PlayerId).ResolvePlay(play);
            }
            return stats;
        }
    }
}
