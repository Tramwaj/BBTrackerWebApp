using BBTracker.Persistence;
using BBTracker.Contracts.ViewModels;
using BBTracker.Model.Models;
using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBTracker.App
{
    public class UserService
    {
        private BBTrackerContext _context;
        public UserService()
        {
            _context = new BBTrackerContext();
        }
        public async Task<bool> CreateUser(CreateUserViewModel userVM)
        {
            if (await _context.Users.AnyAsync(x => x.UserName == userVM.UserName))
                return false;
            else
            {
                var hashedPass = BCrypt.Net.BCrypt.HashPassword(userVM.Password);
                await _context.Users.AddAsync(new User(
                    Guid.NewGuid(),
                    userVM.UserName,
                    hashedPass
                    ));
                await _context.SaveChangesAsync();
                return true;
            }
        }

        public async Task<bool> Login(LoginViewModel loginVM)
        {
            var user = await GetUser(loginVM.UserName);
            if (user == null) 
                return false;
            if (!BCrypt.Net.BCrypt.Verify(loginVM.Password, user.Password))
                return false;
            else
                return true;
        }

        public async Task<User> GetUser(string userName) => await _context
            .Users
            .FirstOrDefaultAsync(x => x.UserName == userName);
    }
}
