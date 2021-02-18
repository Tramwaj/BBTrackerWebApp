using BBTracker.Contracts.ViewModels;
using BBTracker.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBTracker.App.Services
{
    public interface IPlayParser
    {
        public ICollection<Play> ReadPlaysBundle(ICollection<PlayDTO> playsBundle, Guid gameId);
    }
}
