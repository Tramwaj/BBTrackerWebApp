using System;
using System.Security.Policy;

namespace BBTracker.Model.Models
{    
    abstract public class Play
    {
        public Guid Id { get; set; }
        public DateTime Time { get;set; }
        public Guid GameId { get;set; }
        public Game Game { get; set; }
        public Guid PlayerId { get;set; }
        public Player Player { get; set; }
        public TimeSpan GameTime { get;set; }
        public bool IsTeamB { get;set; }

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

        protected Play(Guid id, DateTime time, bool isTeamB, Player player, Game game)
        {
            Id = id;
            Time = time;
            Game = game;
            Player = player;
            IsTeamB = isTeamB;
            PlayerId = player.Id;
            GameId = game.Id;
        }

        public abstract void UpdateStats(Stats stats);
        

        
    }
}