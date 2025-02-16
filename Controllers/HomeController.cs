using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

using AutoDice.Models;
using AutoDice.Services;
using AutoDice.Interfaces;

namespace AutoDice.Controllers;

public class HomeController : Controller
{
    readonly ILogger<HomeController> _logger;
    readonly ITgBot _bot;
    readonly IRepository<WebUserSession> _webUserSessions;

    public HomeController(ILogger<HomeController> logger, ITgBot bot, IRepository<WebUserSession> wus)
    {
        _bot = bot;
        _logger = logger;
        _webUserSessions = wus;
    }

    public IActionResult Index()
    {
        if (!_bot.IsRunning)
        {
            _logger.LogInformation($"Redirecting to bootstrap");
            return RedirectToAction("Index", "Bootstrap");
        }

        var sessionCookie = Request.Cookies["Session"];

        if (sessionCookie == null)
        {
            return RedirectToAction("Index", "Login");
        }

        var session = _webUserSessions.GetById(Guid.Parse(sessionCookie));

        if (session == null)
        {
            return RedirectToAction("Index", "Login");
        }

        return View(session.Player);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
