using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

using AutoDice.Models;
using AutoDice.Services;
using AutoDice.Interfaces;

namespace AutoDice.Controllers;

public class BootstrapController : Controller
{
    public class InputModel
    {
        public string Value { get; set; } = string.Empty;
    }

    private readonly ILogger<HomeController> _logger;
    private readonly ITgBot _bot;
    private readonly IRepository<TgToken> _tokens;

    public BootstrapController(ILogger<HomeController> logger, ITgBot bot, IRepository<TgToken> tokens)
    {
        _bot = bot;
        _logger = logger;
        _tokens = tokens;
    }

    public IActionResult Index()
    {
        _logger.LogInformation("Bootstrap was opened");
        return View();
    }

    [HttpPost("/Submit")]
    public IActionResult Submit([FromBody] InputModel model)
    {
        if (string.IsNullOrWhiteSpace(model.Value))
        {
            return BadRequest("Input cannot be empty.");
        }

        if (_bot.IsRunning)
        {
            return BadRequest("Bot is already running");
        }

        var allTokens = _tokens.GetAll();
        foreach (var i in allTokens)
        {
            _tokens.Delete(i);
        }
        _tokens.Add(new TgToken { Token = model.Value });

        if (_bot.TryStart())
        {
            return Ok();
        } else
        {
            return BadRequest("Failed to start the bot");
        }
    }
}

