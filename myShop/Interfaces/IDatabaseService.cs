using myShop.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace myShop.Interfaces
{
    public interface IDatabaseService
    {
        Task<bool> CreateBillingAddressAsync(BillingAddress Addresss);
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<IEnumerable<ProductColour>> GetProductColoursAsync();
        Task<IEnumerable<ProductSize>> GetProductSizesAsync();
        Task<bool> SaveShippingAddressAsync(BaseAddress address);
        Task<bool> CreateShippingAddressAsync(ShippingAddress shippingAddress);
    }
}