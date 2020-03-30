using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using HillLabTestEntities;

namespace MVCWebUI.Models
{
    public class CategoryAction : EntityAction<Category>, ICategoryAction
    {
        public CategoryAction(HttpClient clientAPI, IProductHttpClientWrapper parentWrapper)
            : base(clientAPI, "Categories", parentWrapper)
        {
        }
        public async Task<IEnumerable<Category>> GetAllCategories()
        {
            IEnumerable<Category> categories = await GetAll();
            if (categories != null)
                return categories.OrderBy(x => x.CategoryName);

            return null;
        }
        public async Task<Category> GetCategoryById(int categoryId)
        {
            return await GetById(categoryId);
        }
        public async Task<Uri> CreateCategory(Category category)
        {
            return await Create(category);
        }
        public async Task<HttpStatusCode> UpdateCategory(Category category)
        {
            return await Update(category);
        }
        public async Task<HttpStatusCode> DeleteCategory(int categoryId)
        {
            return await Delete(categoryId);
        }

    }
}