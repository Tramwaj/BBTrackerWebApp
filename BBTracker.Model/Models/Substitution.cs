using System;

namespace BBTracker.Model.Models
{
    public class Substitution
    {
        public Guid Id { get; }
        public DateTime Time { get; }
        public TimeSpan GameTime { get; }
        public Guid PlayerId { get; }
        public Guid GameId { get; }

        public Player Player { get; set; }
        public Game Game { get; set; }
        public bool SubbedIn { get; }

        public Substitution()
        {

        }

        public Substitution(Guid id, DateTime time, TimeSpan gameTime, Guid playerId, Guid gameId, bool subbedOut)
        {
            Id = id;
            Time = time;
            GameTime = gameTime;
            PlayerId = playerId;
            GameId = gameId;
            SubbedIn = subbedOut;
        }
    }

}