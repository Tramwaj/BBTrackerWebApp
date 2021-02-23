using BBTracker.Contracts.ViewModels;
using BBTracker.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBTracker.Contracts.Services
{
    public interface IUserService
    {
        public Task<bool> CreateUser(CreateUserViewModel createUserVM);
        public Task<bool> Login(LoginViewModel loginVM);
        public Task<User> GetUser(string userName);
        public Task<bool> DeleteUser(Guid id);
    }
}
