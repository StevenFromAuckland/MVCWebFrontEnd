using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace MVCWebUI.Models
{
    public abstract class EntityAction<T> : IEntityAction<T> where T : class
    {
        public HttpClient ClientAPI { get; set; }
        protected string EntityPath { get; set; }

        protected IProductHttpClientWrapper ParentWrapper { get; set; }

        public EntityAction(HttpClient clientAPI, string entityPath, IProductHttpClientWrapper parentWrapper)
        {
            ClientAPI = clientAPI;
            EntityPath = entityPath;
            ParentWrapper = parentWrapper;
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            List<T> objEntities = new List<T>();
            HttpResponseMessage response = await ClientAPI.GetAsync(EntityPath);
            if (response.IsSuccessStatusCode)
            {
                objEntities = await response.Content.ReadAsAsync<List<T>>();
            }
            return objEntities;
        }

        public async Task<T> GetById(int Id)
        {

            T objEntity = default(T);
            HttpResponseMessage response = await ClientAPI.GetAsync(string.Format("{0}/{1}", EntityPath, Id));
            if (response.IsSuccessStatusCode)
            {
                objEntity = await response.Content.ReadAsAsync<T>();
            }
            return objEntity;
        }

        public async Task<Uri> Create(T entity)
        {
            HttpResponseMessage response = await ClientAPI.PostAsJsonAsync(EntityPath, entity);
            response.EnsureSuccessStatusCode();

            // return URI of the created resource.
            return response.Headers.Location;
        }

        public async Task<HttpStatusCode> Update(T entity)
        {
            HttpResponseMessage response = await ClientAPI.PutAsJsonAsync(EntityPath, entity);
            response.EnsureSuccessStatusCode();

            return response.StatusCode;
        }

        public async Task<HttpStatusCode> Delete(int Id)
        {
            HttpResponseMessage response = await ClientAPI.DeleteAsync(string.Format("{0}/{1}", EntityPath, Id));
            return response.StatusCode;
        }


    }

}
