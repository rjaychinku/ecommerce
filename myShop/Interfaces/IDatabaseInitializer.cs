using myShop.Models;
using System.Threading.Tasks;

namespace myShop.Extensions
{
    public interface IDatabaseInitializer
    {
        Task SeedCacheDataAsync();
        void SeedDatabaseDataAsync();
    }
}