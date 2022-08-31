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
        private readonly BBTrackerContext _context;
        public GameRepo() => _context = new BBTrackerContext();

        public async Task NewGameAsync(Game game)
        {
            await _context.Games.AddAsync(game);
            await _context.SaveChangesAsync();
        }
        public async Task<ICollection<Game>> GetAllGamesAsync(Guid userId) => await _context.Games.Where(g => g.OwnerId == userId).AsQueryable().ToListAsync();

        public async Task<Game> GetGameByIdAsync(Guid id) => await _context.Games.FirstOrDefaultAsync(g => g.Id == id);
        public async Task<Game> GetFullGameByIdAsync(Guid id) => await _context.Games.Include(g => g.Plays)
                                                                                     .Include(g => g.PlayerGames)
                                                                                        .ThenInclude(pg => pg.Player)
                                                                                     .FirstOrDefaultAsync(g => g.Id == id);

        public async Task AddPlayerGame(PlayerGame pg)
        {
            await _context.PlayerGames.AddAsync(pg);
            await _context.SaveChangesAsync();
        }

        public async Task<ICollection<PlayerGame>> GetPlayerGamesByGameId(Guid id) => await _context.PlayerGames.Where(pg => pg.GameId == id).ToListAsync();

        public async Task UpdateEndTime(Guid gameId, DateTime end)
        {
            var game = await GetGameByIdAsync(gameId);
            game.End = end;
            await _context.SaveChangesAsync();
        }
        //todo: to own repo - with substitutions as a Play

        public async Task<ICollection<Play>> GetPlaysByGameId(Guid id) => await _context.Plays.Where(p => p.GameId == id).ToListAsync();
    }
}
