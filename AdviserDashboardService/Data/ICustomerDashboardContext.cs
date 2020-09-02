using Microsoft.Azure.Cosmos;

namespace CustomerDashboardService.Data
{
    public interface ICustomerDashboardContext
    {
        public Container GetContainer();
    }
}