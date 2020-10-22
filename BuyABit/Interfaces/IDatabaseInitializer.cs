using BuyABit.Models;
using System.Threading.Tasks;

namespace BuyABit.Extensions
{
    public interface IDatabaseInitializer
    {
        void SeedDatabaseDataAsync();
        Task SeedCacheDataAsync();
    }
}