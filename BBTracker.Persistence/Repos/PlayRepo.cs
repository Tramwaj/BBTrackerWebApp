using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

namespace BBTracker.Persistence.Repos
{
    public class PlayRepo
    {
        private BBTrackerContext _context;
        public PlayRepo()
        {
            _context = new BBTrackerContext();
        }

    }
}
