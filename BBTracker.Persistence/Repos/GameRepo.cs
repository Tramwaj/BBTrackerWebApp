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

        public async Task StartGameAsync(Game game)
        {
            await _context.Games.AddAsync(game);
            await _context.SaveChangesAsync();
        }        

        public async Task<Game> GetGameByIdAsync(Guid id) => await _context.Games.FirstOrDefaultAsync(g => g.Id == id);

        public async Task AddPlayerGame(PlayerGame pg)
        {
            await _context.PlayerGames.AddAsync(pg);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateEndTime(Guid gameId, DateTime end)
        {
            var game = await GetGameByIdAsync(gameId);
            game.End = end;
            await _context.SaveChangesAsync();
        }
        //todo: to own repo?
        public async Task AddPlay(Play play)
        {   
            await _context.Plays.AddAsync(play);
            
            await _context.SaveChangesAsync();
        }        
    }
}
