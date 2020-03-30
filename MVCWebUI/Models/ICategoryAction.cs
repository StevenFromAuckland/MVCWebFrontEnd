using HillLabTestEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;

namespace MVCWebUI.Models
{
    public interface ICategoryAction : IEntityAction<Category>
    {
        Task<IEnumerable<Category>> GetAllCategories();
        Task<Category> GetCategoryById(int categoryId);
        Task<Uri> CreateCategory(Category category);
        Task<HttpStatusCode> UpdateCategory(Category category);
        Task<HttpStatusCode> DeleteCategory(int categoryId);

    }

}
