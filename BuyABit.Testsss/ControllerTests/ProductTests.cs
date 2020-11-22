using BuyABit.Testsss;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using System.Threading.Tasks;
using Moq;
using BuyABit.Controllers;

namespace BuyABit.Tests.ControllerTests
{
    public class ProductTests : IClassFixture<MoqData>
    {
        private readonly MoqData _fixture;
        private ProductController _controllerUnderTest;
        public ProductTests(MoqData fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task GetAll_ShouldReturnAllProductsAsync()
        {
            //Arrange
            List<Product> allProducts = _fixture.FullContext.Products.ToList();
            _fixture.DatabaseServiceMoq.Setup(x => x.GetAllProductsAsync())
                                        .ReturnsAsync(allProducts);

            //Act
            _controllerUnderTest = new ProductController(_fixture.DatabaseServiceMoq.Object);
            IEnumerable<Product> result = await _controllerUnderTest.GetAll();

            //Assert
            Assert.Equal(result.Count(), allProducts.Count);
        }
    }
}
