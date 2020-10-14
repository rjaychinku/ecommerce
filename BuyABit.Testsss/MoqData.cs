using BuyABit.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuyABit.Testsss
{
    public class MoqData : IDisposable
    {
        public FullApplicationContext FullContext { get; private set; } = null;

        public MoqData()
        {
            var options = new DbContextOptionsBuilder<FullApplicationContext>()
           .UseInMemoryDatabase(databaseName: "buyabitDatabase")
           .Options;

            FullContext = new FullApplicationContext(options);
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
            FullContext.Products.AddRange(dummyProducts);
            FullContext.SaveChanges();
        }
            
        public void Dispose()
        {
            FullContext.Dispose();
        }
    }
}
