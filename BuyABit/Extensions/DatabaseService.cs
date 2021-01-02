using System.Collections.Generic;
using System.Linq;
using BuyABit.Interfaces;
using BuyABit.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BuyABit.Extensions
{
    public class DatabaseService : IDatabaseService
    {
        private readonly FullApplicationContext _dbcontext;
        public DatabaseService(FullApplicationContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _dbcontext.Products.ToListAsync();
        }

        public async Task<IEnumerable<ProductSize>> GetProductSizesAsync()
        {
            return await _dbcontext.ProductSizes.OrderBy(c => c.DropDownOrder).ToListAsync();
        }

        public async Task<IEnumerable<ProductColour>> GetProductColoursAsync()
        {
            return await _dbcontext.ProductColours.OrderBy(c => c.DropDownOrder).ToListAsync();
        }

        public async Task<bool> CreateBillingAddressAsync(BillingAddress Addresss)
        {
            await _dbcontext.BillingAddresses.AddAsync(Addresss);
            return await _dbcontext.SaveChangesAsync() > 0;
        }

        public async Task<bool> SaveShippingAddressAsync(BaseAddress address)
        {
            await _dbcontext.AddAsync(address);
            return await _dbcontext.SaveChangesAsync() > 0;
        }

        public Task<bool> CreateShippingAddressAsync(ShippingAddress shippingAddress)
        {
            throw new System.NotImplementedException();
        }

        public bool UpdateUser(ApplicationUser user)
        {
            _dbcontext.Update(user);
            return _dbcontext.SaveChanges() > 0;
        }
    }
}
