using CustomerDashboardService.Model;
using Microsoft.Azure.Cosmos;
using System;
using System.Threading.Tasks;

namespace CustomerDashboardService.Data.Repositories
{
    public class CustomerRepository : IRepository<Customer>
    {
        private readonly Container container;

        public CustomerRepository(ICustomerDashboardContext context)
        {
            this.container = context.GetContainer();
        }

        public async Task<Customer> Add(Customer entity)
        {
            await this.container.CreateItemAsync<Customer>(entity, new PartitionKey(entity.Id));
            return entity;
        }

        public async Task<Customer> Delete(string id)
        {
            return await container.DeleteItemAsync<Customer>(id, new PartitionKey(id));
        }

        public async Task<Customer> Get(string id)
        {
            var result = await container.ReadItemAsync<Customer>(id.ToString(), new PartitionKey(id));
            return result;
        }

        public async Task<Customer> Update(Customer entity)
        {
            return await container.ReplaceItemAsync(entity, entity.Id.ToString(), new PartitionKey(entity.Id));
        }
    }
}
