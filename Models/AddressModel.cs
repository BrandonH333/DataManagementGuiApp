using System;

namespace DataManagementGuiApp.Models
{
    public class AddressModel
    {
        public Guid ContactId { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Apt { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
    }
}
