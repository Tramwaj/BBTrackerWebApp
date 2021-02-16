using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBTracker.Contracts.ViewModels
{
    public class SetupGameViewModel
    {
        public ICollection<FullPlayerDTO> Players { get; set; }
        //TODO: add locations
    }

}
