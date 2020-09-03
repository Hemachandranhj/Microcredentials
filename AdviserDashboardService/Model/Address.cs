using Newtonsoft.Json;

namespace CustomerDashboardService.Model
{
    public class Address
    {
        public string AddressLine1 { get; set; }

        public string AddressLine2 { get; set; }

        [JsonProperty(PropertyName = "postcode")]
        public string Postcode { get; set; }

        public string City { get; set; }

        public string Country { get; set; }
    }
}
