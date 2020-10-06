using System.Threading.Tasks;

namespace BuyABit.Extensions
{
    public interface ICacheInitializer
    {
        Task SeedCacheDataAsync();
    }
}