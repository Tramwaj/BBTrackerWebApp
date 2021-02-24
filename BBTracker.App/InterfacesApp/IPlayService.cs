using BBTracker.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBTracker.App.Interfaces
{
    public interface IPlayService
    {
        public Task<bool> PlayIsPossible(Play play);
    }
}
