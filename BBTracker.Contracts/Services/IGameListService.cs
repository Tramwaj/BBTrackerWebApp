using BBTracker.Model.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BBTracker.Contracts.Services
{
    public interface IGameListService
    {
        Task<ICollection<Game>> GetGames(IEnumerable<Claim> claims);
    }
}
