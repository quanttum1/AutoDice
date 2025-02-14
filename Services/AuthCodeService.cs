using Microsoft.Extensions.Caching.Memory;
using AutoDice.Models;
using AutoDice.Interfaces;

namespace AutoDice.Services;

public class AuthCodeService : IAuthCodeService<Player>
{
    readonly IMemoryCache _cache;
    readonly Random _random = new();
    readonly IRepository<Player> _players;

    public AuthCodeService(IMemoryCache cache, IRepository<Player> players)
    {
        _players = players;
        _cache = cache;
    }

    public string CreateCode(Player player)
    {
        int? id = _players.GetById(player.Id)?.Id;
        if (id == null)
        {
            throw new KeyNotFoundException("Given player isn't in database");
        }

        string code = _random.Next(100000, 999999).ToString();
        _cache.Set($"auth_code:{code}", id, TimeSpan.FromMinutes(5)); // Store with 5 min expiration
        return code;
    }

    public Player? Validate(string code)
    {
        if (_cache.TryGetValue($"auth_code:{code}", out int id))
        {
            _cache.Remove($"auth_code:{code}"); // Remove after use
            return _players.GetById(id);
        }
        return null;
    }
}

