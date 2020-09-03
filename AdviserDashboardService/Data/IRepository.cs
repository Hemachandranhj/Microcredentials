using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CustomerDashboardService.Data
{
    public interface IRepository<T> where T : class
    {
        Task<T> Get(string id, string postcode);

        Task<IList<T>> GetAll(string postcode, string dateOfBirth);

        Task<T> Add(T entity);

        Task<T> Update(T entity);

        Task<T> Delete(T entity);
    }
}
