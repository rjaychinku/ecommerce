using myShop.Interfaces;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace myShop.Extensions
{
    public class CacheService : ICacheService
    {
        private readonly IDatabase _cacheDB;
        public CacheService(IConnectionMultiplexer cacheDB)
        {
            _cacheDB = cacheDB.GetDatabase();
        }

        public async Task<bool> SaveCountriesAsync(string key, object something)
        {
            return await _cacheDB.StringSetAsync(key, something.ToString());
        }

        public async Task<object> GetCountriesAsync(string key)
        {
            return await _cacheDB.StringGetAsync(key);
        }
    }
}
