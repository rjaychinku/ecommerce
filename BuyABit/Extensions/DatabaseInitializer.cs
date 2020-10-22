using BuyABit.Interfaces;
using BuyABit.Models;
using Microsoft.EntityFrameworkCore;
using Namotion.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuyABit.Extensions
{
    public class DatabaseInitializer : IDatabaseInitializer
    {
        private FullApplicationContext dbContext;
        private IApiProviderService apiProviderService;
        private ICacheService _cacheDB;
        public DatabaseInitializer(FullApplicationContext dbContexInjected, IApiProviderService apiServiceInjected
                                   , ICacheService cacheService)
        {
            dbContext = dbContexInjected;
            apiProviderService = apiServiceInjected;
            _cacheDB = cacheService;
        }

        public void SeedDatabaseDataAsync()
        {
          //  dbContext.Database.EnsureCreated();
            dbContext.Database.Migrate();
            if (!dbContext.Products.Any())
            {
                List<Product> dummyProducts = new List<Product>
                    {
                        new Product
                        {
                            Name = "Product 1",
                            Description = "rite you necessary to code here to insert the User to database and save",
                            Price = "450",
                            Quantity = "5",
                            Size = "S"
                        },
                        new Product
                        {
                            Name = "Product 2",
                            Description = "rite you necessary to code here to insert the User to database and save",
                            Price = "599",
                            Quantity = "50",
                            Size = "XL"
                        },
                        new Product
                        {
                            Name = "Product 3",
                            Description = "rite you necessary to code here to insert the User to database and save",
                            Price = "849",
                            Quantity = "2",
                            Size = "L"
                        },
                        new Product
                        {
                            Name = "Product 4",
                            Description = "rite you necessary to code here to insert the User to database and save",
                            Price = "359",
                            Quantity = "6",
                            Size = "M"
                        }
                    };
                dbContext.Products.AddRange(dummyProducts);
                dbContext.SaveChanges();
            }
            if (!dbContext.ProductSizes.Any())
            {
                List<ProductSize> dummySizes = new List<ProductSize>
                {
                    new ProductSize
                    {
                        Name = "Size",
                        DropDownOrder = 0
                        },
                           new ProductSize
                    {
                        Name = "XS",
                             DropDownOrder = 1
                        },
                                  new ProductSize
                    {
                        Name = "S",
                             DropDownOrder = 2
                        },
                                         new ProductSize
                    {
                        Name = "M",
                             DropDownOrder = 3
                        },
                               new ProductSize
                    {
                        Name = "L",
                             DropDownOrder = 4
                        }
                };


                dbContext.ProductSizes.AddRange(dummySizes);
                dbContext.SaveChanges();
            }
            if (!dbContext.ProductColours.Any())
            {
                List<ProductColour> dummyColours = new List<ProductColour>
                {
                     new ProductColour
                    {
                        Name = "Colour",
                             DropDownOrder = 0
                        },
                    new ProductColour
                    {
                        Name = "White",
                             DropDownOrder = 1
                        },
                    new ProductColour
                    {
                        Name = "Black",
                             DropDownOrder = 2
                     },
                    new ProductColour
                    {
                        Name = "Blue",
                             DropDownOrder = 3
                        },
                     new ProductColour
                    {
                        Name = "Red",
                             DropDownOrder = 4
                        },
                   new ProductColour
                    {
                        Name = "Grey",
                             DropDownOrder = 5
                     }
                };

                dbContext.ProductColours.AddRange(dummyColours);
                dbContext.SaveChanges();
            }
        }
        public async Task SeedCacheDataAsync()
        {
            var res = await apiProviderService.GetAllCountriesDataAsync();
            bool yes = await _cacheDB.SaveCountriesAsync("1", res);
            var result = await _cacheDB.GetCountriesAsync("1");
        }
    }
}
