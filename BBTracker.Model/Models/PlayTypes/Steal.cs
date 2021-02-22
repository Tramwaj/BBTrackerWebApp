﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBTracker.Model.Models
{
    public class Steal : Play
    {

        public Steal()
        {

        }

        public Steal(Guid id, DateTime time, bool isTeamB, Guid playerId, Guid gameId) : base(id, time, isTeamB, playerId, gameId) { }

        public override void UpdateStats(Stats stats)
        {
            ++stats.Steals;
        }

        //public Steal(Guid id, DateTime time, bool isTeamB, Guid playerId, Guid gameId, Guid turnoverId) : base(id, time, isTeamB, playerId, gameId)
        //{
        //    TurnoverId = turnoverId;
        //}
        //public Steal(Guid id, DateTime time, bool isTeamB, Guid playerId, Guid gameId, Turnover turnover) : base(id, time, isTeamB, playerId, gameId)
        //{
        //    TurnoverId = turnover.Id;
        //    Turnover = turnover;
        //}
    }
}
