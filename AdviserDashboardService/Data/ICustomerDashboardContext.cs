using Microsoft.Azure.Cosmos;

namespace CustomerDashboardService.Data
{
    public interface ICustomerDashboardContext
    {
        public Container CustomerContainer { get; }
        public Container SearchContainer { get; }
    }
}