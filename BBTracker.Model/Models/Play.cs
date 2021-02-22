using BBTracker.Model.Interfaces;
using System;
using System.Security.Policy;

namespace BBTracker.Model.Models
{    
    abstract public class Play
    {
        public Guid Id { get; }
        public DateTime Time { get; }
        public Game Game { get; set; }
        public TimeSpan GameTime { get; }
        public bool IsTeamB { get; }
        public Guid PlayerId { get; }
        public Guid GameId { get; }
        public Player Player { get; set; }

        public Play()
        {
        }

        public Play(Guid id, DateTime time, bool isTeamB, Guid playerId, Guid gameId)
        {
            Id = id;
            Time = time;
            //GameTime = gameTime;
            IsTeamB = isTeamB;
            PlayerId = playerId;
            GameId = gameId;
        }
        public abstract void UpdateStats(Stats stats);
        

        
    }
}