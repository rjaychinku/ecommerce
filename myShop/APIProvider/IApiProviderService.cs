using System.Threading.Tasks;

namespace myShop
{
    public interface IApiProviderService
    {
        Task<object> GetAllCountriesDataAsync();
    }
}