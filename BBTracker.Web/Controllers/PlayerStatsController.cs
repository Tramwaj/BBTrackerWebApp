﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BBTrackerWebApp.Controllers
{
    [Route("[controller]")]
    [Authorize]
    public class PlayerStatsController : Controller
    {
        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}
