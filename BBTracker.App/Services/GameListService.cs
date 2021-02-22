using BBTracker.Contracts.Services;
using BBTracker.Model.Models;
using BBTracker.Persistence.Repos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BBTracker.App.Services
{
    public class GameListService : IGameListService
    {
        private readonly GameRepo _gameRepo;
        private readonly IUserService _userRepo;
        public GameListService(GameRepo gameRepo, IUserService userRepo)
        {
            _gameRepo = gameRepo;
            _userRepo = userRepo;
        }
        public async Task<ICollection<Game>> GetGames(IEnumerable<Claim> userClaims)
        {
            string userName = userClaims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var _user = await _userRepo.GetUser(userName);
            return await _gameRepo.GetAllGamesAsync(_user.Id);
        }
    }
}
