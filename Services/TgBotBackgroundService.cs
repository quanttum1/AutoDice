using System;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

using AutoDice.Interfaces;
using AutoDice.Models;

namespace AutoDice.Services;

public class TgBotBackgroundService : BackgroundService, ITgBot
{
    private readonly IServiceProvider _serviceProvider;
    private TelegramBotClient _bot = null!;
    private CancellationTokenSource _cts = new CancellationTokenSource();
    private ILogger<TgBotBackgroundService> _logger;
    private bool _isRunning;

    public bool IsRunning 
    {
        get => _isRunning;
    }

    public TgBotBackgroundService(IServiceProvider serviceProvider, ILogger<TgBotBackgroundService> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    public bool TryStart()
    {
        using var scope = _serviceProvider.CreateScope();

        if (IsRunning) return true; // Prevent multiple starts
        _logger.LogInformation("Trying to run the bot...");

        var repository = scope.ServiceProvider.GetRequiredService<IRepository<TgToken>>();
        List<TgToken> tokens = new(repository.GetAll());
        if (tokens.Count == 0)
        {
            _logger.LogError("No API key was found, the bot wasn't started");
            return false;
        }
        string apiKey = tokens.First().Token;

        _logger.LogInformation("Bot was launched");

        _bot = new TelegramBotClient(apiKey, cancellationToken: _cts.Token);
        _bot.OnMessage += OnMessage;

        _isRunning = true;
        return true;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Launching the bot...");
        TryStart(); // Try to start the bot at launch
        _logger.LogInformation("So, waiting...");
        await Task.Delay(Timeout.Infinite, stoppingToken);
    }

    private async Task OnMessage(Message msg, UpdateType type)
    {
        _logger.LogTrace($"Got a message from {msg.Chat.Id}");
        if (msg.Text != null)
        {
            _logger.LogTrace($"Text: '{msg.Text}'");
            await _bot.SendMessage(msg.Chat.Id, "Hello from ASP.NET Bot!");
        }
    }

    public override Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Stopping the bot...");
        _isRunning = false;
        return base.StopAsync(cancellationToken);
    }
}

