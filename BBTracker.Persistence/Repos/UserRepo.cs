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
            user.Player = await _context.Players.FirstOrDefaultAsync(x=>x.Id==user.PlayerId);
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
    }
}
