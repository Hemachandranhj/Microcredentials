using CustomerDashboardService.Model;
using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace CustomerDashboardService.Data
{
    public class CustomerDashboardContext : CosmosClient, ICustomerDashboardContext
    {
        public Container CustomerContainer { get; private set; }

        public Container SearchContainer { get; private set; }

        private Container leaseContainer;

        public CustomerDashboardContext(string endpointUri, string primaryKey, string databaseName, string containerName)
            : base(endpointUri, primaryKey, new CosmosClientOptions { ApplicationName = "CustomerService" })
        {
            this.CreateDatabaseAsync(databaseName, containerName);            
        }

        private async void CreateDatabaseAsync(string databaseName, string containerName)
        {
            var response = await this.CreateDatabaseIfNotExistsAsync(databaseName);

            var database = response.Database;

            this.CustomerContainer = await database.CreateContainerIfNotExistsAsync(containerName, "/id", 400);
            
            this.leaseContainer = await database.CreateContainerIfNotExistsAsync(containerName + "lease", "/id", 400);

            this.SearchContainer = await database.CreateContainerIfNotExistsAsync(containerName + "search", "/address/postcode", 400);

            this.StartChangeFeed();
        }

        private async void StartChangeFeed()
        {
            var processor = this.CustomerContainer.GetChangeFeedProcessorBuilder<Customer>(processorName: "changeFeedSample", OnChangesAsync)
                            .WithLeaseContainer(this.leaseContainer)                            
                            .WithInstanceName("consoleHost")
                            .Build();

            await processor.StartAsync();
        }

        private async Task OnChangesAsync(IReadOnlyCollection<Customer> changes, CancellationToken cancellationToken)
        {
            foreach (Customer customer in changes)
            {
                try
                {
                    _ = await this.SearchContainer.ReadItemAsync<Customer>(customer.Id, new PartitionKey(customer.Address.Postcode));

                    _ = await this.SearchContainer.ReplaceItemAsync<Customer>(customer, customer.Id, new PartitionKey(customer.Address.Postcode));
                }
                catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
                {
                    _ = await this.SearchContainer.CreateItemAsync<Customer>(customer, new PartitionKey(customer.Address.Postcode));
                }
            }                                           
        }
    }
}
