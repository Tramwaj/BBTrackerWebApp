using System;

namespace BBTracker.Model.Models
{    
    public class FieldGoal : Play
    {
        public int Points { get; }
        public bool Made { get; }
        public bool WasBlocked { get; }
        public bool WasAssisted { get; }

        public FieldGoal()
        {

        }

        public FieldGoal(Guid id, DateTime time, bool isTeamB, Guid playerId, Guid gameId, int points, bool made, bool wasBlocked, bool wasAssisted) : base(id, time, isTeamB, playerId, gameId)
        {
            Points = points;
            Made = made;
            WasBlocked = wasBlocked;
            WasAssisted = wasAssisted;
        }

        public override void UpdateStats(Stats stats)
        {
            if (Points == 2 && Made) ++stats.FieldGoalsMade2p;
            if (Points == 2 && !Made) ++stats.FieldGoalsMissed2p;
            if (Points == 3 && Made) ++stats.FieldGoalsMade3p;
            if (Points == 3 && !Made) ++stats.FieldGoalsMissed3p;
        }
    }


}