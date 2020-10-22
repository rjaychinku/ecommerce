using BuyABit.Extensions;
using BuyABit.Models;
using BuyABit.Testsss;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using System.Threading.Tasks;

namespace BuyABit.Tests.RepositoryTests
{
    public class ProductTests : IClassFixture<MoqData>
    {
        private readonly MoqData _fixture;
        public ProductTests(MoqData fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task GetAll_ShouldReturnAllProductsAsync()
        {
            //Arrange
            //Done in MoqData class

            //Act
            var repositoryUnderTest = new DatabaseService(_fixture.FullContext);
            IEnumerable<Product> allReturnedProducts = await repositoryUnderTest.GetAllProductsAsync();

            //Assert
            Assert.Equal(_fixture.FullContext.Products.Count(), allReturnedProducts.Count());
        }
    }
}
