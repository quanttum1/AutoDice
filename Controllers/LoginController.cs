using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

using AutoDice.Models;
using AutoDice.Services;
using AutoDice.Interfaces;

namespace AutoDice.Controllers;

public class LoginController : Controller
{
    public class InputModel
    {
        public string Value { get; set; } = string.Empty;
    }

    readonly ILogger<LoginController> _logger;
    readonly ITgBot _bot;
    readonly IRepository<WebUserSession> _sessions;
    readonly IAuthCodeService<Player> _authCodeService;

    public LoginController(
        ILogger<LoginController> logger,
        ITgBot bot,
        IRepository<WebUserSession> sessions,
        IAuthCodeService<Player> authCodeService
    )
    {
        _bot = bot;
        _logger = logger;
        _sessions = sessions;
        _authCodeService = authCodeService;
    }

    public IActionResult Index()
    {
        _logger.LogInformation("Login was opened");
        ViewData["BotLink"] = $"https://t.me/{_bot.Username}";
        return View();
    }

    [HttpPost("/Login/Submit")]
    public IActionResult Submit([FromBody] InputModel model)
    {
        if (Request.Cookies["Session"] != null)
        {
            return BadRequest("You've already logged in");
        }

        var player = _authCodeService.Validate(model.Value);

        if (player == null)
        {
            return BadRequest("Invalid or expired login code");
        }

        var session = new WebUserSession
        {
            Player = player
        };

        _sessions.Add(session);

        Response.Cookies.Append("Session", session.Id.ToString());
        return Ok();
    }
}
