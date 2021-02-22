using System;

namespace BBTracker.Model.Models
{
    public class Turnover : Play
    {
        public Turnover()
        {

        }

        public Turnover(Guid id, DateTime time, bool isTeamB, Guid playerId, Guid gameId) : base(id, time, isTeamB, playerId, gameId){}

        public override void UpdateStats(Stats stats)
        {
            ++stats.Turnovers;
        }
    }

}