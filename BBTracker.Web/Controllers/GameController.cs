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
        
        public GameController(IGameService gameService)
        {
            _gameService = gameService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        /// Index
        public async Task<ActionResult<SetupGameViewModel>> Index()
        {            
            string userName = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            //var user = jwt
            return Ok(await _gameService.GetGameViewModel(User));
        }
        [HttpPost("startgame")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        ///Start New Game with Starting Players provided
        public async Task<ActionResult<NewGameViewModel>> StartGame([FromBody] GamePlayersVM playerIDs)        
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            //todo: User.Claims wysyłane a nie takie brzydactwo tutaj
            
            return Created(string.Empty, await _gameService.NewGame(playerIDs,User.Claims));
        }
        
        [HttpPost("addplayertg")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> AddPlayerToGame([FromBody] PlayerToGameVM playerDTO)
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
        public async Task<ActionResult> AddPlayersToGame([FromBody] ICollection<PlayerToGameVM> playersDTOs)
        {
            if (await _gameService.AddPlayersToGame(playersDTOs))
                return Ok();
            return BadRequest();
        }
        [HttpPost("addplays")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> AddPlays([FromBody] AddPlaysToGameViewModel playsVM)
        {
            if (await _gameService.AddPlays(playsVM))
                return Ok();
            return BadRequest();
        }
        
        [HttpPost("")]
        //[HttpPost("endgame")]
        [HttpPost("{endGameId:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<GameStatsViewModel>>  EndGame(Guid endGameId)
        {
            var _gameEnded = await _gameService.EndGame(endGameId);
            if (_gameEnded == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(_gameEnded);
            }
        }
    
}
}

