using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

using AutoDice.Models;
using AutoDice.Services;
using AutoDice.Interfaces;

namespace AutoDice.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ITgBot _bot;

    public HomeController(ILogger<HomeController> logger, ITgBot bot)
    {
        _bot = bot;
        _logger = logger;
    }

    public IActionResult Index()
    {
        if (!_bot.IsRunning)
        {
            return RedirectToAction("Index", "Bootstrap");
        }
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
