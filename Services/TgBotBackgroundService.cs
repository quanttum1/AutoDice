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
using AutoDice.Exceptions;

namespace AutoDice.Services;

public class TgBotBackgroundService : BackgroundService, ITgBot
{
    private enum UserState
    {
        Listening,
        AwaitingCode
    }

    readonly IServiceProvider _serviceProvider;
    TelegramBotClient _bot = null!;
    CancellationTokenSource _cts = new CancellationTokenSource();
    ILogger<TgBotBackgroundService> _logger;
    bool _isRunning;
    Dictionary<long, UserState> _userStates = new();
    IAuthCodeService<Player> _authCodeService;

    public bool IsRunning 
    {
        get => _isRunning;
    }

    public TgBotBackgroundService(
        IServiceProvider serviceProvider,
        ILogger<TgBotBackgroundService> logger,
        IAuthCodeService<Player> authCodeService
    )
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
        _authCodeService = authCodeService;
    }

    public bool TryStart()
    {
        try 
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

            _bot = new TelegramBotClient(apiKey, cancellationToken: _cts.Token);
            _bot.OnMessage += OnMessage;

            _logger.LogInformation("Bot was launched");

            _isRunning = true;
            return true;
        } catch (Exception ex)
        {
            _logger.LogError($"Error occurred when tried to start the bot: {ex.ToString()}");
            _isRunning = false;
            return false;
        }
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await Task.Delay(Timeout.Infinite, stoppingToken);
    }

    private async Task OnMessage(Message msg, UpdateType type)
    {
        try
        {
            using var scope = _serviceProvider.CreateScope();
            var tgUsers = scope.ServiceProvider.GetRequiredService<IRepository<TgUser>>();

            _logger.LogTrace($"Got a message from {msg.Chat.Id}");
            if (msg.Text != null)
            {
                _logger.LogTrace($"Text: '{msg.Text}'");

                var user = tgUsers.GetById(msg.Chat.Id);
                var chatId = msg.Chat.Id;
                
                if (tgUsers.GetAll().Count() == 0)
                {
                    var players = scope.ServiceProvider.GetRequiredService<IRepository<Player>>();
                    var player = players.Add(new Player { Name = "Game Master", IsGameMaster = true });
                    tgUsers.Add(new TgUser { TgUserId = chatId, Player = player});
                    await _bot.SendMessage(chatId, "You are the game master now. Use /getlogincode command to get your login code");
                    return;
                }

                if (user != null)
                {
                    if (msg.Text == "/getlogincode")
                    {
                        var code = _authCodeService.CreateCode(user.Player);
                        await _bot.SendMessage(chatId, $"Your code is {code}. It's valid for 5 minutes");
                    } else
                    {
                        await _bot.SendMessage(chatId, "Invalid action. Use /getlogincode command to get your login code");
                    }
                    return;
                }

                if (!_userStates.ContainsKey(chatId))
                {
                    await _bot.SendMessage(chatId, "Hi! Enter your login key:");
                    _userStates[chatId] = UserState.AwaitingCode;
                    return;
                }
                
                if (_userStates[chatId] == UserState.AwaitingCode)
                {
                    var player = _authCodeService.Validate(msg.Text);
                    if (player == null)
                    {
                        await _bot.SendMessage(chatId, "Sorry, the code isn't valid. Try again");
                        return;
                    }

                    tgUsers.Add(new TgUser{ TgUserId = chatId, Player = (Player)player });
                    await _bot.SendMessage(chatId, "You've logged in successfully. You can now use /getlogincode command to get a new login code");
                    return;
                }
            }
        }
        catch (Exception e)
        {
            _logger.LogError($"Error in Telegram bot: {e}");
        }
    }

    public string Username {
        get
        {
            string? username = _bot.GetMe().Result?.Username;
            if (username == null)
                throw new FailedToGetUsernameException(
                    "Failed to get the username of the bot. Is it running?");
            return username;
        }
    }

    public override Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Stopping the bot...");
        _isRunning = false;
        return base.StopAsync(cancellationToken);
    }
}

