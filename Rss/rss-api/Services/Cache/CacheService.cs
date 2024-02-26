using Newtonsoft.Json;
using StackExchange.Redis;

namespace rss_api.Services.Cache;

public class CacheService : ICacheService
{
    private IDatabase _db;
    public CacheService()
    {
        ConfigureRedis();
    }
    private void ConfigureRedis()
    {
        var redis = ConnectionMultiplexer.Connect("localhost:6379");
        _db = redis.GetDatabase();
    }
    public T GetData<T>(string key)
    {
        var value = _db.StringGet(key);
        if (!string.IsNullOrEmpty(value))
        {
            return JsonConvert.DeserializeObject<T>(value);
        }
        return default;
    }
    public bool SetData<T>(string key, T value, DateTimeOffset expirationTime)
    {
        var expiryTime = expirationTime.DateTime.Subtract(DateTime.Now);
        var isSet = _db.StringSet(key, JsonConvert.SerializeObject(value), expiryTime);
        return isSet;
    }
}