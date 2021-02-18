using System;
using System.Collections;
using System.Collections.Generic;

namespace BBTracker.Model.Models
{
    public class Player
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Nick { get; set; }
        public string City { get; set; }
        public bool Deleted { get; set; }
        public ICollection<PlayerGame> PlayerGames { get; set; }
        public ICollection<Play> Plays { get; set; }
        public ICollection<Substitution> Substitutions { get; set; }

        public Player(Guid id, string name, string nick)
        {
            Id = id;
            Name = name;
            Nick = nick;
            City = "BB";
            PlayerGames = new HashSet<PlayerGame>();
        }

        public Player(Guid id, string name, string nick, string city) : this(id, name, nick)
        {
            City = city;
        }
        public void AddPlayerGame(PlayerGame playerGame)
        {
            PlayerGames.Add(playerGame);
        }


    }

}
