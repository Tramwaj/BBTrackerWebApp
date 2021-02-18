using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace BBTracker.Model.Models
{
    public class Game
    {
        public Guid Id { get; set; }
        public DateTime Start { get; set; }
        public DateTime? End { get; set; }
        public bool Deleted { get; set; }
        public int ScoreA { get; set; }
        public int ScoreB { get; set; }

        public ICollection<Play> Plays { get; set; }
        public ICollection<PlayerGame> PlayerGames { get; set; }
        public ICollection<Substitution> Substitutions { get; set; }

        public User Owner { get; set; }
        public Guid OwnerId { get; set; }
        
        public Game(Guid id,Guid ownerId, DateTime start)
        {
            Id = id;
            OwnerId = ownerId;
            Start = start;
            //PlayerGames = playerGames;
        }
        public void AddPlayerGame(PlayerGame playerGame)
        {
            PlayerGames.Add(playerGame);
        }

        public void AddPlay(Play play)
        {
            if (Plays == null) Plays = new List<Play>();
            Plays.Add(play);
        }
    }
}