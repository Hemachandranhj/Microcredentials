using System;
using System.Threading.Tasks;

namespace CustomerDashboardService.Data
{
    public interface IRepository<T> where T : class
    {
        Task<T> Get(string id);

        Task<T> Add(T entity);

        Task<T> Update(T entity);

        Task<T> Delete(string id);
    }
}
