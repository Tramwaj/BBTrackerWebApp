using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBTracker.App.Services.StatsCounting
{
    public class Period
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public Period(DateTime on, DateTime off)
        {
            Start = on;
            End = off;
        }
    }
}
