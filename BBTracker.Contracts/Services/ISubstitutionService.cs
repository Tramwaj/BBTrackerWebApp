using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBTracker.Contracts.Services
{
    public interface ISubstitutionService
    {
        Task<bool> AddSubstitution(AddSubstitutionViewModel subvm);
    }
}
