using BBTracker.Model.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBTracker.Persistence.Repos
{
    public class SubstitutionRepo
    {
        private BBTrackerContext _context;
        public SubstitutionRepo() => _context = new BBTrackerContext();
        
        public async Task AddSubstitution(Substitution sub)
        {
            await _context.Substitutions.AddAsync(sub);
        }
        
        public async Task<Substitution> GetLastSubByPlayerGame(Guid playerId, Guid gameId) => await
            _context.Substitutions.Where(s => s.Player.Id == playerId&& s.GameId==gameId).LastOrDefaultAsync();
        
    }
}
