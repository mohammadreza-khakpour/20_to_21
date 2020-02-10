using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using Microsoft.Extensions.Logging;

using MohammadpourAspNetCoreSaturdayMondayEvening.Models;

namespace MohammadpourAspNetCoreSaturdayMondayEvening.Controllers
{
    public class HomeController : Controller
    {
        ILogger<HomeController> logger;
        public HomeController(ILogger<HomeController> _logger)
        {
            logger = _logger;
        }

        public IActionResult Index()
        {
            logger.LogInformation( "Hi,Reza: Information Log",200,300);
            logger.LogError("Hi,Reza: Error Log");
            logger.LogWarning("Hi,Reza: Warning Log");
            logger.LogDebug("Hi,Reza: Debug Log");
            logger.LogCritical("Hi,Reza: Critical Log");
            logger.LogInformation("AAAA", 10, 100);
            logger.LogInformation("BBBB", 10, 101);
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
