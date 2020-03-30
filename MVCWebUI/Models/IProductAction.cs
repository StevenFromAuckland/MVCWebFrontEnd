using HillLabTestEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;

namespace MVCWebUI.Models
{
    public interface IProductAction : IEntityAction<Product>
    {
        Task<IEnumerable<Product>> GetAllProducts();
        Task<Product> GetProductById(int productId);
        Task<Uri> CreateProduct(Product product);
        Task<HttpStatusCode> UpdateProduct(Product product);
        Task<HttpStatusCode> DeleteProduct(int productId);

    }

}
