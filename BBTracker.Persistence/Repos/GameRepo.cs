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
            var game = await GetFullGameByIdAsync(gameId);
            game.End = end;
            await SwitchOutPlayers(game);
            _context.Games.Update(game);
            await _context.SaveChangesAsync();
        }
        private async Task SwitchOutPlayers(Game game)
        {
            //var plays = await GetPlaysByGameId(game.Id);
            //game.Plays = plays;

            var uniquePlayerIds = game.Plays.Select(p => p.PlayerId).Distinct().ToList();

            foreach (var playerId in uniquePlayerIds)
            {
                var player = await _context.Players.FirstOrDefaultAsync(p => p.Id == playerId);
                var lastSubstitution = game.Plays.Where((p) => p.Player == player && p is Substitution)
                              .OrderBy(p => p.GameTime)
                              .Last() as Substitution;
                if (lastSubstitution.SubbedIn == true)
                {
                    var subOutAdded = new Substitution(DateTime.Now, lastSubstitution.IsTeamB, player, game, false);
                    subOutAdded.GameTime = (game.End - game.Start)??TimeSpan.Zero;
                    game.Plays.Add(subOutAdded);
                }
            }

        }

        //todo: to own repo - with substitutions as a Play

        public async Task<ICollection<Play>> GetPlaysByGameId(Guid id) => await _context.Plays.Where(p => p.GameId == id).ToListAsync();

        //public async Task<int> EndInactiveGames()
        //{
        //    var gamesNotEnded = await _context.Games.Where(g => g.End == null).ToListAsync();
        //    int count = 0;
        //    foreach (var game in gamesNotEnded)
        //    {
        //        var lastPlay = (await GetPlaysByGameId(game.Id)).OrderBy(p => p.GameTime).Last();
        //        if (game.Start.Add(lastPlay.GameTime).AddMinutes(30) < DateTime.Now)
        //        {
        //            game.End = game.Start.Add(lastPlay.GameTime);
        //            _context.Update(game);
        //            count++;
        //        }
        //    }
        //    return count;
        //}
    }
}
