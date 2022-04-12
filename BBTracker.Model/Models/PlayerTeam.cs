using System;

namespace BBTracker.Model.Models
{
    public class PlayerTeam
    {
        public Player Player { get; set; }
        public Team Team { get; set; }
        public Guid PlayerId { get; set; }
        public Guid TeamId { get; set; }

    }
}
