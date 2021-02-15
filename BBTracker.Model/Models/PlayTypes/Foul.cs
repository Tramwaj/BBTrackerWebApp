using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBTracker.Model.Models
{
    public class Foul : Play
    {
        public Player? FouledPlayer { get; set; }
    }
}
