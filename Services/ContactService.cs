using CsvHelper;
using DataManagementGuiApp.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DataManagementGuiApp.Services
{
    public class ContactService
    {
        public List<ContactInfoModel> GetContactListData()
        {
            var contactRecords = new List<ContactModel>();
            var addressRecords = new List<AddressModel>();
            var phoneRecords = new List<PhoneModel>();

            // Read the records from csv files
            using (var reader = new StreamReader(@"..\DataManagementGuiApp\Data\Contact.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                contactRecords = csv.GetRecords<ContactModel>().ToList();
            }

            using (var reader = new StreamReader(@"..\DataManagementGuiApp\Data\Address.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                addressRecords = csv.GetRecords<AddressModel>().ToList();
            }

            using (var reader = new StreamReader(@"..\DataManagementGuiApp\Data\Phone.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                phoneRecords = csv.GetRecords<PhoneModel>().ToList();
            }

            //Create contact info model list
            var contactInfoModelList = new List<ContactInfoModel>();    
            for(int i = 0; i < contactRecords.Count; i++)
            {
                contactInfoModelList.Add(new ContactInfoModel
                {
                    ContactModel = contactRecords[i],
                    AddressModel = addressRecords[i],
                    PhoneModel = phoneRecords[i]
                });
            }

            return contactInfoModelList;
        }

        public ContactInfoModel GetContactById(Guid contactId)
        {
            var contactInfoModelList = GetContactListData();

            return contactInfoModelList.Find(x => x.ContactModel.Id == contactId);
        }

        public bool SaveContact(ContactInfoModel contactInfo)
        {
            var contactInfoModelList = GetContactListData();
            var contact = contactInfoModelList.Find(x => x.ContactModel.Id == contactInfo.ContactModel.Id);

            if (contact == null)
            {
                // Set new contact id
                contactInfo.ContactModel.Id = Guid.NewGuid();
                contactInfo.AddressModel.ContactId = contactInfo.ContactModel.Id;
                contactInfo.PhoneModel.ContactId = contactInfo.ContactModel.Id;

                contactInfoModelList.Add(contactInfo);
            }
            else
            {
                // Replace existing contact with update
                contactInfoModelList.Remove(contact);
                contactInfoModelList.Add(contactInfo);
            }

            WriteRecords(contactInfoModelList);

            return true;
        }

        public void DeleteContact(Guid? contactId)
        {
            var contactInfoModelList = GetContactListData();

            // Find the record to delete
            var contactToBeDeleted = contactInfoModelList.Find(x => x.ContactModel.Id == contactId);

            // Remove the record to delete
            contactInfoModelList.Remove(contactToBeDeleted);

            WriteRecords(contactInfoModelList);
        }

        private void WriteRecords(List<ContactInfoModel> contactInfoModelList)
        {
            var contacts = contactInfoModelList.Select(x => x.ContactModel);
            var addresses = contactInfoModelList.Select(x => x.AddressModel);
            var phones = contactInfoModelList.Select(x => x.PhoneModel);

            // Write the records to the csv file
            using (var writer = new StreamWriter(@"..\DataManagementGuiApp\Data\Contact.csv"))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(contacts);
            }

            using (var writer = new StreamWriter(@"..\DataManagementGuiApp\Data\Address.csv"))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(addresses);
            }

            using (var writer = new StreamWriter(@"..\DataManagementGuiApp\Data\Phone.csv"))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(phones);
            }
        }
    }
}
