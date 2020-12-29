using BasketStatsModel;
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
        [HttpGet]
        public ActionResult <ICollection<Player>> Index()
        {
            return new[]
            {
                new Player(Guid.NewGuid(), "Michał", "Magic"),
                new Player(Guid.NewGuid(), "Ola", "Tolka"),
                new Player(Guid.NewGuid(), "Marcin", "Martinez"),
                new Player(Guid.NewGuid(), "Andrzej", "Ivan")
            };
        }
       // [HttpPost]
        //public ActionResult AddPlayer([FromBody] Blog blog)
        //{
        //    return Created(String.Empty, null);
        //}
    }
}
