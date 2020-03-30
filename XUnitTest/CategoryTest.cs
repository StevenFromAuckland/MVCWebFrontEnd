using HillLabTestEntities;
using Moq;
using MVCWebUI.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace XUnitTest
{
    public class CategoryTest
    {
        [Fact]
        public async Task GetAllCategories_ReturnsListOfCategories_WithSingleCategory()
        {
            var mockEntity = new Mock<ICategoryAction>();
            mockEntity.Setup(x => (x.GetAllCategories())).Returns(GetAllCategories());
            var mockHttpClient = new Mock<IProductHttpClientWrapper>();
            mockHttpClient.SetupGet(x => x.Category).Returns(mockEntity.Object);

            var result = await mockHttpClient.Object.Category.GetAllCategories();

            Assert.IsType<List<Category>>(result);
            Assert.Single(result);
        }
        [Fact]
        public async Task GetCategoryById_ReturnsOneCategory_WithId1()
        {
            const int categoryId = 1;
            var mockEntity = new Mock<ICategoryAction>();
            mockEntity.Setup(x => (x.GetCategoryById(It.IsAny<int>()))).Returns(GetCategoryById(categoryId));
            var mockHttpClient = new Mock<IProductHttpClientWrapper>();
            mockHttpClient.SetupGet(x => x.Category).Returns(mockEntity.Object);


            var result = await mockHttpClient.Object.Category.GetCategoryById(It.IsAny<int>());

            Assert.IsType<Category>(result);
            Assert.Equal(categoryId, result.CategoryId);
        }
        [Fact]
        public void CreateCategory_ReturnNothing_VerifyCall()
        {
            var mockEntity = new Mock<ICategoryAction>();
            var mockHttpClient = new Mock<IProductHttpClientWrapper>();
            mockHttpClient.SetupGet(x => x.Category).Returns(mockEntity.Object);

            mockHttpClient.Object.Category.CreateCategory(It.IsAny<Category>());


            mockEntity.Verify(x => x.CreateCategory(It.IsAny<Category>()), Times.Once);
        }
        [Fact]
        public void CreateCategory_ReturnNothing_WithoutException()
        {
            var mockEntity = new Mock<ICategoryAction>();
            mockEntity.Setup(x => x.CreateCategory(It.IsAny<Category>())).Throws<Exception>();
            var mockHttpClient = new Mock<IProductHttpClientWrapper>();
            mockHttpClient.SetupGet(x => x.Category).Returns(mockEntity.Object);


            Assert.ThrowsAnyAsync<Exception>(async () => await mockHttpClient.Object.Category.CreateCategory(It.IsAny<Category>()));
        }
        [Fact]
        public void UpdateCategory_ReturnNothing_VerifyCall()
        {
            var mockEntity = new Mock<ICategoryAction>();
            var mockHttpClient = new Mock<IProductHttpClientWrapper>();
            mockHttpClient.SetupGet(x => x.Category).Returns(mockEntity.Object);

            mockHttpClient.Object.Category.UpdateCategory(It.IsAny<Category>());


            mockEntity.Verify(x => x.UpdateCategory(It.IsAny<Category>()), Times.Once);
        }
        [Fact]
        public void UpdateCategory_ReturnNothing_WithoutException()
        {
            var mockEntity = new Mock<ICategoryAction>();
            mockEntity.Setup(x => x.UpdateCategory(It.IsAny<Category>())).Throws<Exception>();
            var mockHttpClient = new Mock<IProductHttpClientWrapper>();
            mockHttpClient.SetupGet(x => x.Category).Returns(mockEntity.Object);


            Assert.ThrowsAsync<Exception>(() => mockHttpClient.Object.Category.UpdateCategory(It.IsAny<Category>()));
        }
        [Fact]
        public void DeleteCategory_ReturnNothing_VerifyCall()
        {
            var mockEntity = new Mock<ICategoryAction>();
            var mockHttpClient = new Mock<IProductHttpClientWrapper>();
            mockHttpClient.SetupGet(x => x.Category).Returns(mockEntity.Object);

            mockHttpClient.Object.Category.DeleteCategory(It.IsAny<int>());


            mockEntity.Verify(x => x.DeleteCategory(It.IsAny<int>()), Times.Once);
        }
        [Fact]
        public void DeleteCategory_ReturnNothing_WithoutException()
        {
            var mockEntity = new Mock<ICategoryAction>();
            mockEntity.Setup(x => x.DeleteCategory(It.IsAny<int>())).Throws<Exception>();
            var mockHttpClient = new Mock<IProductHttpClientWrapper>();
            mockHttpClient.SetupGet(x => x.Category).Returns(mockEntity.Object);


            Assert.ThrowsAsync<Exception>(() => mockHttpClient.Object.Category.DeleteCategory(It.IsAny<int>()));
        }

        public async Task<IEnumerable<Category>> GetAllCategories()
        {
            await Task.Delay(1);
            return new List<Category>
            {
                new Category
                {
                    CategoryId = 1,
                    CategoryName = "Food"
                }
            };
        }

        public async Task<Category> GetCategoryById(int categoryId)
        {
            await Task.Delay(1);
            return
                new Category
                {
                    CategoryId = categoryId,
                    CategoryName = "Food"
                };
        }
    }
}
