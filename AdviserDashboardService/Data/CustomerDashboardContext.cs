using Microsoft.Azure.Cosmos;

namespace CustomerDashboardService.Data
{
    public class CustomerDashboardContext : CosmosClient, ICustomerDashboardContext
    {
        private Container container;

        public CustomerDashboardContext(string endpointUri, string primaryKey, string databaseName, string containerName)
            : base(endpointUri, primaryKey, new CosmosClientOptions { ApplicationName = "CustomerService" })
        {
            this.CreateDatabaseAsync(databaseName, containerName);
        }

        public Container GetContainer()
        {
            return this.container;
        }

        private async void CreateDatabaseAsync(string databaseName, string containerName)
        {
            var response = await this.CreateDatabaseIfNotExistsAsync(databaseName);

            var database = response.Database;

            this.container = await database.CreateContainerIfNotExistsAsync(containerName, "/id", 400);
        }
    }
}
