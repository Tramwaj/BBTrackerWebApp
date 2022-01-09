using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBTracker.Model.Models
{
    public record OnCourtPeriod
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public bool IsTeamB { get; set; }        
    }
}
