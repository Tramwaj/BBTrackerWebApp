using BBTracker.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace BBTracker.App.Services.StatsCounting
{
    public static class PlaysFilter
    {
        //kolejny gracz - bez subina a z suboutem
        public static IEnumerable<Play> FilterPlayerOnCourt(this IEnumerable<Play> plays, Player player)
        {
            if (plays is null)
                return null;
            IEnumerable<Substitution> playerSubs = GetPlayerSubstitutions(plays, player);
            ICollection<Period> periods = playerSubs.Where(x => x.SubbedIn == true)
                                                               .Zip(playerSubs.Where(x => x.SubbedIn == false), (x, y) =>new Period(x.Time, y.Time))
                                                               .ToList();            
            List<Play> output = new();
            foreach (var period in periods)
            {
                output.AddRange(plays.Where(x => x.Time > period.Start && x.Time < period.End));
            }
            return output;
        }

        private static IEnumerable<Substitution> GetPlayerSubstitutions(IEnumerable<Play> plays, Player player)
        {
            return plays.Where(x => x.Player == player)
                                       .Where(x => x is Substitution)
                                       .Select(x => (Substitution)x);
        }
        public static IEnumerable<Period> GetOnCourtPeriods(this IEnumerable<Substitution> subs, Player player = null)
        {
            return subs.Where(x => x.SubbedIn == true)
                                                               .Zip(subs.Where(x => x.SubbedIn == false), (x, y) => new Period(x.Time, y.Time))
                                                               .ToList();
        }



        //public PlaysFilter(Player player, ICollection<Play> plays)
        //{
        //    IEnumerable<Substitution> playerSubs = plays.Where(x => x.Player == player)
        //                               .Where(x => x.GetType() == typeof(Substitution))
        //                               .Select(x => (Substitution)x);
        //    IEnumerable<TimeSpan> onFloorTimespans = playerSubs.Where(x => x.SubbedIn == true)
        //                                                       .Zip(playerSubs.Where(x=>x.SubbedIn==false),(x,y)=>new TimeSpan()                                       
        //}
    }
}
