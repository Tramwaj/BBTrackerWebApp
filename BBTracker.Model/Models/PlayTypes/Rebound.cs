using System;

namespace BBTracker.Model.Models
{
    public class Rebound : Play
    {
        public bool IsOffensive { get; }
        public FieldGoal FieldGoalRebounded { get; set; }
        public Guid FieldGoalReboundedId { get; set; }


        public Rebound()
        {

        }

        public Rebound(Guid id, DateTime time, bool isTeamB, Guid playerId, Guid gameId, bool isOffensive, Guid fieldGoalReboundedId) : base(id, time, isTeamB, playerId, gameId)
        {
            FieldGoalReboundedId = fieldGoalReboundedId;
            IsOffensive = isOffensive;
        }
        public Rebound(Guid id, DateTime time, bool isTeamB, Guid playerId, Guid gameId, bool isOffensive, FieldGoal fieldGoalRebounded) : base(id, time, isTeamB, playerId, gameId)
        {
            IsOffensive = isOffensive;
            FieldGoalRebounded = fieldGoalRebounded;
            FieldGoalReboundedId = fieldGoalRebounded.Id;
        }

        public override void UpdateStats(Stats stats)
        {
            if (IsOffensive) ++stats.OffensiveRebounds;
            else ++stats.DefensiveRebounds;
        }

    }
}