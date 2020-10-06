using BuyABit.Interfaces;
using BuyABit.Models;
using Microsoft.EntityFrameworkCore;
using Namotion.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuyABit.Extensions
{
    public class CacheInitializer : ICacheInitializer
    {
        private FullApplicationContext dbContext;
        private IApiProviderService apiProviderService;
        private ICacheService _cacheDB;
        public CacheInitializer(IApiProviderService apiServiceInjected
                                   ,ICacheService cacheService)
        {
            apiProviderService = apiServiceInjected;
            _cacheDB = cacheService;
        }
        public async Task SeedCacheDataAsync()
        {
            var res = await apiProviderService.GetAllCountriesDataAsync();
            //bool yes = await _cacheDB.SaveCountriesAsync("1", res);
            //var result = await _cacheDB.GetCountriesAsync("1");
        }
    }
}
