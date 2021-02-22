using System;

namespace BBTracker.Model.Models
{
    public class Assist : Play
    {
        public FieldGoal FieldGoal { get; set; }
        public Guid FieldGoalId { get; set; }

        public Assist()
        {

        }
        public Assist(Guid id, DateTime time, bool isTeamB, Guid playerId, Guid gameId, Guid fieldGoalId) 
            : base(id, time, isTeamB, playerId, gameId) => FieldGoalId = fieldGoalId;

        public Assist(Guid id, DateTime time, bool isTeamB, Guid playerId, Guid gameId, FieldGoal fieldGoal) 
            : base(id, time, isTeamB, playerId, gameId)
        {
            FieldGoal = fieldGoal;
            FieldGoalId = fieldGoal.Id;
        }
        public override void UpdateStats(Stats stats)
        {
            stats.Assists++;
        }        
    }
}