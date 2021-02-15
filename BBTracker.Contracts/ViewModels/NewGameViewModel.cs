using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBTracker.Contracts.ViewModels
{
    public class NewGameViewModel
    {
        public Guid Id { get; set; }
        public DateTime Start { get; set; }
        public ICollection<Guid> Players { get; set; }

        public NewGameViewModel(Guid id, DateTime start, ICollection<Guid> players=null)
        {
            Id = id;
            Start = start;
            Players = players;
        }
    }
}
