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
using BBTracker.Contracts.Services;
using BBTracker.Persistence.Repos;

namespace BBTracker.App.Services
{
    public class UserService : IUserService
    {
        private UserRepo _userRepo;
        public UserService()
        {
            _userRepo = new UserRepo();
        }
        public async Task<bool> CreateUser(CreateUserViewModel userVM)
        {
            var allUsers = await _userRepo.GetAllUsersAsync();
            if (allUsers.Any(x => x.UserName == userVM.UserName))
                return false;
            else
            {
                var hashedPass = BCrypt.Net.BCrypt.HashPassword(userVM.Password);
                await _userRepo.AddAsync(new User(
                    Guid.NewGuid(),
                    userVM.UserName,
                    hashedPass
                    ));
                return true;
            }
        }

        public async Task<bool> Login(LoginViewModel loginVM)
        {
            var user = await GetUser(loginVM.UserName);
            if (user == null)
                return false;
            return VerifyPassword(loginVM.Password, user.Password);
        }

        private bool VerifyPassword(string inputPassword, string dbPassword)
        {
            if (BCrypt.Net.BCrypt.Verify(inputPassword, dbPassword))
                return true;
            return false;
        }


        public async Task<User> GetUser(string userName) => await _userRepo.GetUserByNameAsync(userName);

        public async Task<bool> DeleteUser(Guid id)
        {
            var _user = await _userRepo.GetUserByIdAsync(id);
            if (_user == null)
                return false;
            await _userRepo.DeleteAsync(_user);
            return true;
        }
    }
}
