using System;
using System.Collections.Generic;
using System.Text;

namespace BBTracker.Model
{
    public class PlayerGame
    {
        public Guid PlayerId { get; set; }
        public Guid GameId { get; set; }
        public Player Player { get; set; }
        public Game Game { get; set; }
        public bool TeamB { get; set; }

        public PlayerGame()
        {

        }
        public PlayerGame(Player player, Game game, bool teamB)
        {            
            Player = player;
            Game = game;
            TeamB = teamB;
        }
    }
}
