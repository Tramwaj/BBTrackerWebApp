using BBTracker.Model.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace BBTracker.Persistence.Repos
{
    public class PlayerRepo
    {
        private BBTrackerContext _context;
        public PlayerRepo() => _context = new BBTrackerContext();

        public async Task<Player> GetPlayerAsync(Guid id) => await _context.Players.FirstOrDefaultAsync(p => p.Id == id);

        public async Task<ICollection<Player>> GetPlayersAsync() => await _context.Players.AsQueryable().ToListAsync();

        public async Task<ICollection<Player>> GetPlayersFromGame(Guid gameId) => await _context.Players
            .Include(p => p.PlayerGames)
            .Where(p => p.PlayerGames
            .Any(pg => pg.GameId == gameId))
            .AsQueryable()
            .ToListAsync();

        public async Task Create(Player player)
        {
            await _context.Players.AddAsync(player);
            await _context.SaveChangesAsync();
        }
        public async Task Delete(Player player)
        {
            _context.Players.Remove(player);
            await _context.SaveChangesAsync();
        }    
        public async Task Update(Player player)
        {
            var entity = await _context
                .Players
                .FirstOrDefaultAsync(p => p.Id == player.Id);
            entity.Name = player.Name;
            entity.Nick = player.Nick;
            entity.City = player.City;
            await _context.SaveChangesAsync();
        }
    }
}
