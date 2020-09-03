using CustomerDashboardService.Model;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
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
            await this.container.CreateItemAsync<Customer>(entity, new PartitionKey(entity.Address.Postcode));
            return entity;
        }

        public async Task<Customer> Delete(Customer entity)
        {
            var ss = await container.DeleteItemAsync<Customer>(entity.Id, new PartitionKey(entity.Address.Postcode));
            return ss;
        }

        public async Task<Customer> Get(string id, string postcode)
        {
            var result = await container.ReadItemAsync<Customer>(id.ToString(), new PartitionKey(postcode));
            return result;
        }

        public async Task<IList<Customer>> GetAll(string postcode, string dateOfBirth)
        {
            var result = container.GetItemLinqQueryable<Customer>();
            var queryResultSetIterator = result.Where(x => x.Address.Postcode == postcode).ToFeedIterator();
            if (!string.IsNullOrEmpty(dateOfBirth))
            {
                queryResultSetIterator = result.Where(x => x.Address.Postcode == postcode || x.DateOfBirth == Convert.ToDateTime(dateOfBirth)).ToFeedIterator();
            }
            List<Customer> customers = new List<Customer>();

            while (queryResultSetIterator.HasMoreResults)
            {
                FeedResponse<Customer> currentResultSet = await queryResultSetIterator.ReadNextAsync();
                foreach (Customer customer in currentResultSet)
                {
                    customers.Add(customer);
                }
            }

            return customers;
        }

        public async Task<Customer> Update(Customer entity)
        {
            return await container.ReplaceItemAsync(entity, entity.Id.ToString(), new PartitionKey(entity.Address.Postcode));
        }
    }
}