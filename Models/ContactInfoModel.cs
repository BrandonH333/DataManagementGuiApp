using System.Collections.Generic;

namespace DataManagementGuiApp.Models
{
    public class ContactInfoModel
    {
        public ContactModel ContactModel { get; set; } = new ContactModel();
        public AddressModel AddressModel { get; set; } = new AddressModel();
        public PhoneModel PhoneModel { get; set; } = new PhoneModel();
    }
}
