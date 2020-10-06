using BuyABit.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BuyABit.Interfaces
{
    public interface ICacheService
    {
        Task<object> GetCountriesAsync(string key);
        Task<bool> SaveCountriesAsync(string key, object something);
    }
}