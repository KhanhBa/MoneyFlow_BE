using System.Text.Json;
using StackExchange.Redis;

namespace MoneyFlow.Services.Redis;

public class RedisService
{
    private readonly IDatabase _db;
    
    public RedisService(IConnectionMultiplexer connection)
    {
        _db = connection.GetDatabase();
    }
    // Set string
    public async Task SetStringAsync(string key, string value, TimeSpan? expiry = null)
    {
        await _db.StringSetAsync(key, value, expiry);
    }

    // Get string
    public async Task<string?> GetStringAsync(string key)
    {
        return await _db.StringGetAsync(key);
    }

    // Set object
    public async Task SetObjectAsync<T>(string key, T obj, TimeSpan? expiry = null)
    {
        var json = JsonSerializer.Serialize(obj);
        await _db.StringSetAsync(key, json, expiry);
    }

    // Get object
    public async Task<T?> GetObjectAsync<T>(string key)
    {
        var json = await _db.StringGetAsync(key);
        if (json.IsNullOrEmpty) return default;
        return JsonSerializer.Deserialize<T>(json!);
    }

    // Delete key
    public async Task<bool> DeleteKeyAsync(string key)
    {
        return await _db.KeyDeleteAsync(key);
    }
}
