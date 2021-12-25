using EvrakYonetimSistemi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using EvrakYonetimSistemi.Data;
using Microsoft.AspNetCore.Authorization;

namespace EvrakYonetimSistemi.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext c;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext c)
        {
            _logger = logger;
            this.c = c;
        }

        public IActionResult Index()
        {

            return View();
        }

    }
}
