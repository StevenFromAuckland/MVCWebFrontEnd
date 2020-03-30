using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;

namespace MVCWebUI.Models
{
    public interface IEntityAction<T>
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(int Id);
        Task<Uri> Create(T entity);
        Task<HttpStatusCode> Update(T entity);
        Task<HttpStatusCode> Delete(int Id);

    }
 
}
