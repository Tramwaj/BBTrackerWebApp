using BBTracker.Contracts.Services;
using BBTracker.Contracts.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BBTracker.Web.Controllers
{
    
    [Route("[controller]")]
    [Authorize]
    public class GameController : ControllerBase
    {
        private readonly IGameService _gameService;
        private readonly IPlayingTimeService _substitutionService;
        public GameController(IGameService gameService, IPlayingTimeService substitutionService )
        {
            _gameService = gameService;
            _substitutionService = substitutionService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<SetupGameViewModel>> Index()
        {            
            string userName = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            //var user = jwt
            return Ok(await _gameService.GetGameViewModel(User));
        }
        [HttpPost("startgame")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<NewGameViewModel>> StartGame([FromBody] GamePlayersVM playerIDs)        
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            //todo: User.Claims wysyłane a nie takie brzydactwo tutaj
            string userName = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            return Created(string.Empty, await _gameService.NewGame(playerIDs,userName));
        }
        
        [HttpPost("addplayertg")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> AddPlayerToGame([FromBody] AddPlayerToGameVM playerDTO)
        {
            if (await _gameService.AddPlayerToGame(playerDTO))
                return Ok();
            return BadRequest();
        }
        //todo: add multiple players to game?? (this does not work yet)
        [HttpPost("addplayerstg")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> AddPlayersToGame([FromBody] ICollection<AddPlayerToGameVM> playersDTOs)
        {
            if (await _gameService.AddPlayersToGame(playersDTOs))
                return Ok();
            return BadRequest();
        }
        [HttpPost("addplays")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> AddPlays([FromBody] AddPlaysToGameViewModel playsVM)
        {
            if (await _gameService.AddPlays(playsVM))
                return Ok();
            return BadRequest();
        }
        [HttpPost("substitution")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> AddSubstitution([FromBody] AddSubstitutionViewModel subVM)
        {
            if (await _substitutionService.AddSubstitution(subVM))
                return Ok();
            return BadRequest();
        }

        [HttpPost("")]
        //[HttpPost("endgame")]
        [HttpPost("{endGameId:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult>  EndGame(Guid endGameId)
        {
            if (await _gameService.EndGame(endGameId))
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

