using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBTracker.Contracts.ViewModels.Stats
{
    public class TopPlayersViewModel
    {
        public ICollection<TopStatsDTO> PointLeaders { get; set; }
        public ICollection<TopStatsDTO> AssistLeaders { get; set; }
        public ICollection<TopStatsDTO> ReboundsLeaders { get; set; }
        public ICollection<TopStatsDTO> OffReboundsLeaders { get; set; }
        public ICollection<TopStatsDTO> DefReboundsLeaders { get; set; }
        public ICollection<TopStatsDTO> StealsLeaders { get; set; }
        public ICollection<TopStatsDTO> FG2pPercentageLeaders { get; set; }
        public ICollection<TopStatsDTO> FG3pPercentageLeaders { get; set; }
        public ICollection<TopStatsDTO> BlocksLeaders { get; set; }
        public ICollection<TopStatsDTO> FoulsLeaders { get; set; }
        public ICollection<TopStatsDTO> FouledLeaders { get; set; }

        //pkt
        //rebs (def/off)
        //asst
        //stl
        //fg %
        //3p fg %
        //fouled
        //blocks
        //fouls
        //wsio na 100 poss.

        //+/- ??
        //adj. +/-
    }
    public class TopStatsDTO
    {
        public Guid PlayerId { get; set; }
        public string PlayerFullName { get; set; }
        public double StatValue { get; set; }
    } 
}
