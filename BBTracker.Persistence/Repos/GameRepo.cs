using BBTracker.Model.Models;
using Microsoft.EntityFrameworkCore;
using BBTracker.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBTracker.Persistence.Repos
{
    public class GameRepo
    {
        private BBTrackerContext _context;
        public GameRepo()
        {
            _context = new BBTrackerContext();
        }
        public async Task Add(Game game)
        {
            await _context.Games.AddAsync(game);
            await _context.SaveChangesAsync();
        }
        public async Task<Game> GetGameByIdAsync(Guid id) => await _context.Games.FirstOrDefaultAsync(g => g.Id == id);

        public async Task AddPlayerGame(Guid playerId, Guid gameId, bool isTeamB)
        {
            _context.PlayerGames.Add(new PlayerGame(
                await _context.Players.FirstOrDefaultAsync(p=>p.Id==playerId),
                await GetGameByIdAsync(gameId),
                isTeamB
                ));
            await _context.SaveChangesAsync();
        }

        public async Task UpdateEndTime(Guid gameId, DateTime end)
        {
            var game = await _context.Games.FirstAsync(g => g.Id == gameId);
            game.End = end;
            await _context.SaveChangesAsync();
        }
    }
}
