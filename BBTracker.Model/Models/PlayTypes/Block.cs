using System;

namespace BBTracker.Model.Models
{
    public class Block : Play
    {
        public FieldGoal FieldGoalBlocked { get; set; }
        public Guid FieldGoalBlockedId { get; set; }

        public Block()
        {

        }

        public Block(Guid id, DateTime time, bool isTeamB, Guid playerId, Guid gameId,Guid fieldGoalBlockedId) : base(id, time, isTeamB, playerId, gameId)
        {
            FieldGoalBlockedId = fieldGoalBlockedId;
        }

        public Block(Guid id, DateTime time, bool isTeamB, Guid playerId, Guid gameId, FieldGoal fieldGoalBlocked) : base(id, time, isTeamB, playerId, gameId)
        {
            FieldGoalBlocked = fieldGoalBlocked;
            FieldGoalBlockedId = fieldGoalBlocked.Id;
        }
    }

}