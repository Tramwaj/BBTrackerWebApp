using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBTracker.Contracts.ViewModels
{
    public class CreateUserViewModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public Guid PlayerId { get; set; }
    }
}
