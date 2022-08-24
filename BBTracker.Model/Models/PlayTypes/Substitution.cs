using System;

namespace BBTracker.Model.Models
{
    public class Substitution : Play
    {

        public bool SubbedIn { get; set; }

        public Substitution()
        {

        }

        public Substitution(Guid id, DateTime time, bool isTeamB, Guid playerId, Guid gameId, bool subbedIn)
            : base(id, time, isTeamB, playerId, gameId) => SubbedIn = subbedIn;

        public Substitution(Guid id, DateTime time, bool isTeamB, Player player, Game game, bool subbedIn)
            : base(id, time, isTeamB, player, game) => SubbedIn = subbedIn;

        public override void UpdateStats(Stats stats)
        {
            stats.Substitutions.Add(new Tuple<TimeSpan, bool>(GameTime, SubbedIn));
        }

    }

}