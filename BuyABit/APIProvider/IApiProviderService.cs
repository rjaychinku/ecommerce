using System.Threading.Tasks;

namespace BuyABit
{
    public interface IApiProviderService
    {
        Task<object> GetAllCountriesDataAsync();
    }
}