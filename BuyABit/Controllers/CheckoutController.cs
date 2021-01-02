using System.Threading.Tasks;
using BuyABit.Interfaces;
using BuyABit.Models.AddressFactory;
using Microsoft.AspNetCore.Mvc;

namespace BuyABit.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CheckoutController : ControllerBase
    {
        private AddressFactory _addressFactory;
        private IDatabaseService _databaseService;
        private ICacheService __cacheDB;
        public CheckoutController(AddressFactory addressFactory, ICacheService cacheDB, IDatabaseService databaseService)
        {
            _addressFactory = addressFactory;
            _databaseService = databaseService;
            __cacheDB = cacheDB;
        }

        // GET: /Checkout/CreateAddress
        [HttpPost]
        [Route(nameof(CreateAddress))]
        public async Task<bool> CreateAddress(BaseAddress address)
        {
           bool result = await _databaseService.SaveShippingAddressAsync(address);
           return result;
        }

        // GET: /Checkout/GetCountries
        [HttpGet]
        [Route(nameof(GetCountries))]
        public async Task<object> GetCountries()
        {
            object result = await __cacheDB.GetCountriesAsync("1");
            return result;
        }
    }
}
