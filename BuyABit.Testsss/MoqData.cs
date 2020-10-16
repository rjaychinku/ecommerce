using BuyABit.Interfaces;
using BuyABit.Models;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuyABit.Testsss
{
    public class MoqData : IDisposable
    {
        public FullApplicationContext FullContext;
        public Mock<IDatabaseService> DatabaseServiceMoq = new Mock<IDatabaseService>();
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
                            Description = "Follow Gaither Music for updates on your favorite artists",
                            Price = "199",
                            Quantity = "9",
                            Size = "S"
                        },
                        new Product
                        {
                            Name = "Product 2",
                            Description = "Follow Gaither Music for updates on your favorite artists",
                            Price = "650",
                            Quantity = "15",
                            Size = "XL"
                        },
                        new Product
                        {
                            Name = "Product 3",
                            Description = "Follow Gaither Music for updates on your favorite artists",
                            Price = "355",
                            Quantity = "19",
                            Size = "L"
                        },
                        new Product
                        {
                            Name = "Product 4",
                            Description = "Follow Gaither Music for updates on your favorite artists",
                            Price = "1900",
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
