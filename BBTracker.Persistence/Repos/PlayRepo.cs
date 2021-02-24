using BBTracker.Model.Models;
using Microsoft.EntityFrameworkCore;
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
        public PlayRepo() => _context = new BBTrackerContext();

        public async Task AddSubstitution(Substitution sub)
        {
            sub.Player = await _context.Players.FirstAsync(p => p.Id == sub.PlayerId);
            await _context.Substitutions.AddAsync(sub);
            await _context.SaveChangesAsync();
        }
        public async Task<ICollection<Substitution>> GetPlayerSubstitutions(Guid playerId) => await _context.Substitutions
            .Where(s => s.PlayerId == playerId)
            .ToListAsync();

        public async Task<ICollection<Substitution>> GetPlayerGameSubstitutions(Guid playerId, Guid gameId) => await _context.Substitutions
            .Where(s => s.PlayerId == playerId && s.GameId == gameId)
            .ToListAsync();


        public async Task<Substitution> GetLastSubByPlayerGame(Guid playerId, Guid gameId) => await
            _context.Substitutions.Where(s => s.Player.Id == playerId && s.GameId == gameId).LastOrDefaultAsync();
        
        public async Task AddPlay(Play play)
        {
            await _context.Plays.AddAsync(play);

            await _context.SaveChangesAsync();
        }
    }
}
