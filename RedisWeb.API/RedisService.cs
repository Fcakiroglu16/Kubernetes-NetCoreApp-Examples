using StackExchange.Redis;

namespace RedisWeb.API;

public class RedisService
{
    private readonly string? _redisHost;
    private readonly string? _redisPort;
    private readonly string? _redisPassword;
    private ConnectionMultiplexer? _redis;

    public RedisService(IConfiguration configuration)
    {
        _redisHost = configuration["Redis:Host"];
        _redisPort = configuration["Redis:Port"];
        _redisPassword = configuration["Redis:Password"];
    }

    public void Connect()
    {
        var configString = $"{_redisHost}:{_redisPort},password={_redisPassword}";

        _redis = ConnectionMultiplexer.Connect(configString);
    }

    public IDatabase GetDb(int db)
    {
        return _redis!.GetDatabase(db);
    }
}