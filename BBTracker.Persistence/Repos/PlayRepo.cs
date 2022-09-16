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
            _context.Substitutions.Where(s => s.Player.Id == playerId && s.GameId == gameId)
                                    .OrderBy(s => s.Time)
                                    .LastOrDefaultAsync();

        public async Task<ICollection<Play>> GetAllPlays()
        {
            
            var plays = await _context.Plays.ToListAsync();
            return plays;
        }

        public async Task AddPlay(Play play)
        {
            var game = _context.Games.Where(g => g.Id == play.GameId).FirstOrDefault();
            var start = game.Start;
            play.GameTime = play.Time - start;
            await _context.Plays.AddAsync(play);
            if (play is FieldGoal)
            {
                if ((play as FieldGoal).Made)
                {
                    if (play.IsTeamB) game.ScoreB += (play as FieldGoal).Points;
                    if (!play.IsTeamB) game.ScoreA += (play as FieldGoal).Points;
                }

            }

            await _context.SaveChangesAsync();
        }
    }
}
