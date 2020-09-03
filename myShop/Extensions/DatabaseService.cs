using System.Collections.Generic;
using System.Linq;
using myShop.Interfaces;
using myShop.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace myShop.Extensions
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
            return (IEnumerable<ProductSize>)await _dbcontext.ProductSizes.OrderBy(c => c.DropDownOrder).ToListAsync();
        }

        public async Task<IEnumerable<ProductColour>> GetProductColoursAsync()
        {
            return (IEnumerable<ProductColour>)await _dbcontext.ProductColours.OrderBy(c => c.DropDownOrder).ToListAsync();
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
    }
}
