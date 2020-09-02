namespace CustomerDashboardService.Model
{
    using Newtonsoft.Json;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Customer
    {
        [JsonProperty(PropertyName = "id")]
        [Required]
        public string Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        public string PhoneNumber { get; set; }

        public Address Address { get; set; }

        public int AccountNumber { get; set; }

        public string SortCode { get; set; }

        public int PolicyNumber { get; set; }
        
        public string LastUpdatedBy { get; set; }

        public DateTime LastUpdateAt { get; set; }
    }
}
