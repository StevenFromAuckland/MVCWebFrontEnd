using HillLabTestEntities;
using Moq;
using MVCWebUI.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace XUnitTest
{
    public class ProductTest
    {
        [Fact]
        public async Task GetAllProducts_ReturnsListOfProducts_WithSingleProduct()
        {
            var mockEntity = new Mock<IProductAction>();
            mockEntity.Setup(x => (x.GetAllProducts())).Returns(GetAllProducts());
            var mockHttpClient = new Mock<IProductHttpClientWrapper>();
            mockHttpClient.SetupGet(x => x.Product).Returns(mockEntity.Object);

            var result = await mockHttpClient.Object.Product.GetAllProducts();

            Assert.IsType<List<Product>>(result);
            Assert.Single(result);
        }
        [Fact]
        public async Task GetProductById_ReturnsOneProduct_WithId1()
        {
            const int productId = 1;
            var mockEntity = new Mock<IProductAction>();
            mockEntity.Setup(x => (x.GetProductById(It.IsAny<int>()))).Returns(GetProductById(productId));
            var mockHttpClient = new Mock<IProductHttpClientWrapper>();
            mockHttpClient.SetupGet(x => x.Product).Returns(mockEntity.Object);


            var result = await mockHttpClient.Object.Product.GetProductById(It.IsAny<int>());

            Assert.IsType<Product>(result);
            Assert.Equal(productId, result.ProductId);
        }
        [Fact]
        public void CreateProduct_ReturnNothing_VerifyCall()
        {
            var mockEntity = new Mock<IProductAction>();
            var mockHttpClient = new Mock<IProductHttpClientWrapper>();
            mockHttpClient.SetupGet(x => x.Product).Returns(mockEntity.Object);

            mockHttpClient.Object.Product.CreateProduct(It.IsAny<Product>());


            mockEntity.Verify(x => x.CreateProduct(It.IsAny<Product>()), Times.Once);
        }
        [Fact]
        public void CreateProduct_ReturnNothing_WithoutException()
        {
            var mockEntity = new Mock<IProductAction>();
            mockEntity.Setup(x => x.CreateProduct(It.IsAny<Product>())).Throws<Exception>();
            var mockHttpClient = new Mock<IProductHttpClientWrapper>();
            mockHttpClient.SetupGet(x => x.Product).Returns(mockEntity.Object);


            Assert.ThrowsAnyAsync<Exception>(async () => await mockHttpClient.Object.Product.CreateProduct(It.IsAny<Product>()));
        }
        [Fact]
        public void UpdateProduct_ReturnNothing_VerifyCall()
        {
            var mockEntity = new Mock<IProductAction>();
            var mockHttpClient = new Mock<IProductHttpClientWrapper>();
            mockHttpClient.SetupGet(x => x.Product).Returns(mockEntity.Object);

            mockHttpClient.Object.Product.UpdateProduct(It.IsAny<Product>());


            mockEntity.Verify(x => x.UpdateProduct(It.IsAny<Product>()), Times.Once);
        }
        [Fact]
        public void UpdateProduct_ReturnNothing_WithoutException()
        {
            var mockEntity = new Mock<IProductAction>();
            mockEntity.Setup(x => x.UpdateProduct(It.IsAny<Product>())).Throws<Exception>();
            var mockHttpClient = new Mock<IProductHttpClientWrapper>();
            mockHttpClient.SetupGet(x => x.Product).Returns(mockEntity.Object);


            Assert.ThrowsAsync<Exception>(() => mockHttpClient.Object.Product.UpdateProduct(It.IsAny<Product>()));
        }
        [Fact]
        public void DeleteProduct_ReturnNothing_VerifyCall()
        {
            var mockEntity = new Mock<IProductAction>();
            var mockHttpClient = new Mock<IProductHttpClientWrapper>();
            mockHttpClient.SetupGet(x => x.Product).Returns(mockEntity.Object);

            mockHttpClient.Object.Product.DeleteProduct(It.IsAny<int>());


            mockEntity.Verify(x => x.DeleteProduct(It.IsAny<int>()), Times.Once);
        }
        [Fact]
        public void DeleteProduct_ReturnNothing_WithoutException()
        {
            var mockEntity = new Mock<IProductAction>();
            mockEntity.Setup(x => x.DeleteProduct(It.IsAny<int>())).Throws<Exception>();
            var mockHttpClient = new Mock<IProductHttpClientWrapper>();
            mockHttpClient.SetupGet(x => x.Product).Returns(mockEntity.Object);


            Assert.ThrowsAsync<Exception>(() => mockHttpClient.Object.Product.DeleteProduct(It.IsAny<int>()));
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            await Task.Delay(1);
            return new List<Product>
            {
                new Product
                {
                    ProductId = 1,
                    ProductName = "Bread",
                    CategoryId = 2
                }
            };
        }

        public async Task<Product> GetProductById(int productId)
        {
            await Task.Delay(1);
            return
                new Product
                {
                    ProductId = productId,
                    ProductName = "Bread",
                    CategoryId = 2
                };
        }
    }
}
