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

        public ISet<Play> Plays { get; set; }
        public ISet<PlayerGame> PlayerGames { get; set; }
        //public Game(){}
        public Game(Guid id, DateTime start)
        {
            Id = id;
            Start = start;
            //PlayerGames = playerGames;
        }
        public void AddPlayerGame(PlayerGame playerGame)
        {
            PlayerGames.Add(playerGame);
        }
        public void AddPlay(Play play)
        {
            Plays.Add(play);
        }
    }
}