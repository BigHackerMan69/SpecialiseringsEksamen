using StackExchange.Redis;
using System;
using System.Threading.Tasks;

namespace SpecialiseringsEksamen.Services
{
    internal class RedisService
    {
        private static readonly Lazy<ConnectionMultiplexer> lazyConnection = new Lazy<ConnectionMultiplexer>(() =>
        {
            return ConnectionMultiplexer.Connect("redis-10149.c55.eu-central-1-1.ec2.redns.redis-cloud.com:10149,password=D5VENZXKv5u2PVTMhhRpCbxdvP4dJUTm");
        });

        private static ConnectionMultiplexer Connection => lazyConnection.Value;

        public async Task<string> GetValueAsync(string key)
        {
            var db = Connection.GetDatabase();
            return await db.StringGetAsync(key);
        }

        public async Task SetValueAsync(string key, string value)
        {
            var db = Connection.GetDatabase();
            await db.StringSetAsync(key, value);
        }

        public async Task<bool> RegisterUserAsync(string username, string password)
        {
            var db = Connection.GetDatabase();
            var userKey = $"user:{username}";

            if(await db.KeyExistsAsync(userKey))
            {
                return false;
            }

            await db.StringSetAsync(userKey,password);
            return true;
        }

        public async Task<bool> LoginUserAsync(string username, string password)
        {
            var db = Connection.GetDatabase();
            var userKey = $"user:{username}";

            var storedPassword = await db.StringGetAsync(userKey);
            return storedPassword == password;
        }
    }
}
