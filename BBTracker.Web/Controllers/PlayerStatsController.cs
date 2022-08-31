using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BBTracker.Contracts.Services;

namespace BBTrackerWebApp.Controllers
{
    [Route("[controller]")]
    [Authorize]
    public class PlayerStatsController : ControllerBase
    {
        private readonly IPlayerStatsService _playerStatsService;

        public PlayerStatsController(IPlayerStatsService playerStatsService)
        {
            _playerStatsService = playerStatsService;
        }

        public async Task<IActionResult> Index(bool per30mins = false)
        {
            return Ok(await _playerStatsService.GetPlayersStats(per30mins));
        }
    }
}
