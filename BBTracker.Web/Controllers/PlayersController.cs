using BBTracker;
using BBTracker.Contracts.Services;
using BBTracker.Contracts.ViewModels;
using BBTracker.Model.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasketStatsWebApp.Controllers
{
    [Route("[controller]")]
    
    public class PlayersController : ControllerBase
    {
        private readonly IPlayerService _playerService;
        
        public PlayersController(IPlayerService playerService)
        {
            _playerService = playerService;
        }
        [HttpGet]
        [Authorize(Roles = "Admin,User")]
        public async Task<ActionResult<ICollection<FullPlayerDTO>>> Index()
        {
            return Ok(await _playerService.GetAllPlayersDTO()); 
        }

        [HttpGet("{id:guid}")]
        //[Authorize(Roles = "Admin,User")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<FullPlayerDTO>> GetPlayer(Guid id)
        {            
            return Ok(await _playerService.GetPlayerById(id));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        //todo: add info about "user who added, date"
        public async Task<ActionResult> AddPlayer([FromBody] CreatePlayerDTO player)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (await _playerService.AddPlayer(player))
                return Created(String.Empty,null);
            else
                return BadRequest();
        }
        
        [HttpDelete("{id:guid}")]
        [Authorize(Roles = "Admin,User")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> RemovePlayer(Guid id)
        {
            if (await _playerService.RemovePlayer(id))
                return Ok();
            else
                return BadRequest();
        }
        [HttpPut("{id:guid}")]
        [Authorize(Roles = "Admin,User")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> Editplayer(Guid id, [FromBody] FullPlayerDTO player)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (await _playerService.EditPlayer(id, player))
                return Ok();
            else
            {
                ModelState.AddModelError("Błąd", "nie znaleziono ID");
                return NotFound(ModelState);
            }
        }
    }
}
