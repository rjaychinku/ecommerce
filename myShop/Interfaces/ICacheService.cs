using myShop.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace myShop.Interfaces
{
    public interface ICacheService
    {
        Task<object> GetCountriesAsync(string key);
        Task<bool> SaveCountriesAsync(string key, object something);
    }
}