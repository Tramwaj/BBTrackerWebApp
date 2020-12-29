using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasketStatsWebApp.Controllers
{
    [Route("[controller]")]
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            //return RedirectToAction("Index", "Players");
            return Redirect("~/Players");
        }
    }
}
