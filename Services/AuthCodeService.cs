using Microsoft.Extensions.Caching.Memory;

public class AuthCodeService
{
    private readonly IMemoryCache _cache;
    private readonly Random _random = new();

    public AuthCodeService(IMemoryCache cache)
    {
        _cache = cache;
    }

    public string CreateCode(int id)
    {
        string code = _random.Next(100000, 999999).ToString();
        _cache.Set($"auth_code:{code}", id, TimeSpan.FromMinutes(5)); // Store with 5 min expiration
        return code;
    }

    public int? Validate(string code)
    {
        if (_cache.TryGetValue($"auth_code:{code}", out int id))
        {
            _cache.Remove($"auth_code:{code}"); // Remove after use
            return id;
        }
        return null;
    }
}

