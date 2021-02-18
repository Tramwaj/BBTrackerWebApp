using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBTracker.Contracts.ViewModels
{
    public class AddPlaysToGameViewModel
    {
        public Guid GameId { get; set; }
        public ICollection<PlayDTO> playDTOs { get; set; }
    }
}
