using BBTracker.Contracts.Services;
using BBTracker.Model;
using BBTracker.Model.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasketStatsWebApp.Controllers
{
    //[Authorize]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly IGameListService _gameListService;

        public HomeController(IGameListService gameListService)
        {
            _gameListService = gameListService;
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<Game>>> Index()
        {
            return Ok(await _gameListService.GetGames(User.Claims));
        }

        [HttpGet("Get")]
        public async Task<ActionResult<string>> Get()
        {
            return Ok("bubuububub");
        }
        
        [HttpGet("GetCollection")]
        public async Task<ActionResult<ICollection<string>>> GetCollection()
        {
            return Ok(new List<string>{ "jeden","dwa","trzy"});
        }
    }
}

