using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBTracker.Model.Models.PlayTypes
{
    public class Steal : Play
    {
        public Player StolenFrom { get; set; }
    }
}
