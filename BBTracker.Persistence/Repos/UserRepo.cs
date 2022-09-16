using BBTracker.Model.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBTracker.Persistence.Repos
{
    public class UserRepo
    {
        private BBTrackerContext _context;
        public UserRepo()
        {
            _context = new BBTrackerContext();
        }
        public async Task<User> GetUserByIdAsync(Guid id) => await _context.Users.FirstOrDefaultAsync(p => p.Id == id);
        public async Task<User> GetUserByNameAsync(string userName) => await _context.Users.FirstOrDefaultAsync(p => p.UserName == userName);


        public async Task<ICollection<User>> GetAllUsersAsync() => await _context.Users.AsQueryable().ToListAsync();

        public async Task AddAsync(User user)
        {
            user.Player = await _context.Players.FirstOrDefaultAsync(x => x.Id == user.PlayerId);
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(User user)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(User user)
        {
            var entity = await _context
                .Users
                .FirstOrDefaultAsync(p => p.Id == user.Id);
            entity.Password = user.Password;
            await _context.SaveChangesAsync();
        }
        public async Task<int> EndInactiveGames()
        {
            var gamesNotEnded = await _context.Games.Include(g => g.Plays).Where(g => g.End == null).ToListAsync();
            int count = 0;
            var gameIds = gamesNotEnded.Select(g => g.Id);
            foreach (var gameId in gameIds)
            {
                var game = gamesNotEnded.First(g => g.Id == gameId);
                if (game.Plays.Count() < 1) continue;
                var lastPlay = _context.Plays.Where(p => p.GameId == game.Id).OrderBy(p => p.GameTime).Last();
                if (lastPlay.Time.AddMinutes(30) < DateTime.Now)
                {
                    game.End = game.Start.Add(lastPlay.GameTime);
                    var uniquePlayerIds = game.Plays.Select(p => p.PlayerId).Distinct().ToList();
                    foreach (var playerId in uniquePlayerIds)
                    {
                        var player = await _context.Players.FirstOrDefaultAsync(p => p.Id == playerId);
                        var lastSubstitution = game.Plays.Where((p) => p.Player == player && p is Substitution)
                                      .OrderBy(p => p.GameTime)
                                      .Last() as Substitution;
                        if (lastSubstitution.SubbedIn == true)
                        {
                            game.Plays.Add(new Substitution(Guid.NewGuid(), lastPlay.Time, lastSubstitution.IsTeamB, player, game, false));
                        }
                    }

                    _context.Update(game);
                    count++;
                }
            }
            await _context.SaveChangesAsync();
            return count;
        }
    }
}
