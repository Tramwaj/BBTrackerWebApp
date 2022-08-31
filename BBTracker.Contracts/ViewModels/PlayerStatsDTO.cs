using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace BBTracker.Contracts.ViewModels
{
    public class PlayerStatsDTO
    {
        public string Name { get; set; }
        public string Nick { get; set; }
        public string City { get; set; }
        public double Points { get; set; }
        public int TwoPointsPercentage { get; set; }
        public int ThreePointsPercentage { get; set; }
        public double Assists { get; set; }
        public double DefReB { get; set; }
        public double OffReb { get; set; }
        public double Steals { get; set; }
        public double Turnovers { get; set; }
        public double Minutes { get; set; }
        public PlayerStatsDTO()
        {

        }

        public PlayerStatsDTO(string name, string nick, string city, double points, int twoPointsPercentage, int threePointsPercentage, double assists, double defReB, double offReb, double steals, double turnovers, double minutes)
        {
            Name = name;
            Nick = nick;
            City = city;
            Points = points;
            TwoPointsPercentage = twoPointsPercentage;
            ThreePointsPercentage = threePointsPercentage;
            Assists = assists;
            DefReB = defReB;
            OffReb = offReb;
            Steals = steals;
            Turnovers = turnovers;
            Minutes = minutes;
        }
    }
}
