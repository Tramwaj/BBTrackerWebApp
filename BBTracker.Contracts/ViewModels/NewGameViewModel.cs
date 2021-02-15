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

        public NewGameViewModel(Guid id, DateTime start)
        {
            Id = id;
            Start = start;
        }
    }
}
