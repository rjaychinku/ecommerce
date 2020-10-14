using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuyABit.Interfaces;
using BuyABit.Models;
using BuyABit.Models.AddressFactory;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        // GET: api/CreateAddress
        [HttpPost]
        [Route(nameof(CreateAddress))]
        public async Task<bool> CreateAddress(BaseAddress address)
        {
           bool result = await _databaseService.SaveShippingAddressAsync(address);
           return result;
        }

        // GET: api/GetCountries
        [HttpGet]
        [Route(nameof(GetCountries))]
        public async Task<object> GetCountries()
        {
            var result = await __cacheDB.GetCountriesAsync("1");
            return result;
        }
    }
}
