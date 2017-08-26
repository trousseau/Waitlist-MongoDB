using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WaitlListMongoDB.Models
{
    public class ContactInfo
    {
        [Phone]
        [Display(Name = "Home Phone")]
        [BsonDefaultValue("")]
        public string HomePhone { get; set; } = string.Empty;
        [Phone]
        [Display(Name = "Cell Phone")]
        [BsonDefaultValue("")]
        public string CellPhone { get; set; } = string.Empty;
        [Phone]
        [Display(Name = "Work Phone")]
        [BsonDefaultValue("")]
        public string WorkPhone { get; set; } = string.Empty;
        [Phone]
        [BsonDefaultValue("")]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        public Address Address { get; set; }

        public ContactInfo()
        {
            Address = new Address();
        }

        public ContactInfo(string homePhone, string cellPhone, string workPhone, string email, Address address)
        {
            HomePhone = homePhone;
            CellPhone = cellPhone;
            WorkPhone = workPhone;
            Email = email;
            Address = address;
        }

        public ContactInfo(string homePhone, string cellPhone, string workPhone, string email, string line1, string line2, string city, string state, int zip)
        {
            HomePhone = homePhone;
            CellPhone = cellPhone;
            WorkPhone = workPhone;
            Email = email;

            Address a1 = new Address(line1, line2, city, state, zip);

            Address = a1;
        }
    }
}