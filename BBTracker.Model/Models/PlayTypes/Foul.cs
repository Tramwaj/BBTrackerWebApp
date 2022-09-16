using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBTracker.Model.Models
{
    public class Foul : Play
    {
        //public Player FouledPlayer { get; set; }
        public Foul()
        {

        }

        public Foul(Guid id, DateTime time, bool isTeamB, Guid playerId, Guid gameId, Player fouledPlayer) : base(id, time, isTeamB, playerId, gameId)
        {
            //FouledPlayer = fouledPlayer;
        }

        public override void UpdateStats(Stats stats)
        {
        }
    }
}
