using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBTracker.Contracts.ViewModels
{
    public class AddSubstitutionViewModel
    {
        public Guid GameId { get; set; }
        public Guid PlayerId { get; set; }
        public bool MyProperty { get; set; }
    }
}
