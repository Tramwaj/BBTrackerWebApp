using System;
using System.Collections;
using System.Collections.Generic;

namespace BasketStatsModel
{
    public class Player
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Nick { get; set; }

        public Player(Guid id, string name, string nick)
        {
            Id = id;
            Name = name;
            Nick = nick;
        }
        public virtual ICollection<Play> Plays { get; set; }
    }

}
