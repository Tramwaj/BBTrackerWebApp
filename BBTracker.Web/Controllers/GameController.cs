using BBTracker.Contracts.Services;
using BBTracker.Contracts.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BBTracker.Web.Controllers
{
    
    [Route("[controller]")]
    [Authorize]
    public class GameController : ControllerBase
    {
        private readonly IGameService _gameService;
        public GameController(IGameService gameService)
        {
            _gameService = gameService;
        }

        [HttpPost("startgame")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<NewGameViewModel>> StartGame([FromBody] NewGamePlayersVM playerIDs)        
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Created(string.Empty, await _gameService.NewGame(playerIDs));
        }
        
        [HttpPost("addplayertg")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> AddPlayerToGame([FromBody] AddPlayerToGameVM playerDTO)
        {
            if (await _gameService.AddPlayerToGame(playerDTO))
                return Ok();
            return BadRequest();
        }
        //this does not work yet
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> AddPlayersToGame([FromBody] ICollection<AddPlayerToGameVM> playersDTOs)
        {
            if (await _gameService.AddPlayersToGame(playersDTOs))
                return Ok();
            return BadRequest();
        }
        //[HttpPost("endgame")]
        [HttpPost("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult>  EndGame(Guid id)
        {
            if (await _gameService.EndGame(id))
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
    
}
}

