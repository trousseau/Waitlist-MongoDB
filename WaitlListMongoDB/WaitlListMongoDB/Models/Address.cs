using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WaitlListMongoDB.Models
{
    public class Address
    {
        [Display(Name = "Address Line 1")]
        [BsonDefaultValue("")]
        [StringLength(50)]
        public string Line1 { get; set; } = string.Empty;
        [Display(Name = "Address Line 2")]
        [BsonDefaultValue("")]
        [StringLength(50)]
        public string Line2 { get; set; } = string.Empty;

        [BsonDefaultValue("")]
        [StringLength(50)]
        public string City { get; set; } = string.Empty;

        [BsonDefaultValue("")]
        [StringLength(20)]
        public string State { get; set; } = string.Empty;

        [BsonDefaultValue("00000")]

        public int Zip { get; set; } = 00000;

        public Address()
        {

        }

        public Address(string line1, string line2, string city, string state, int zip)
        {
            Line1 = line1;
            Line2 = line2;
            City = city;
            State = state;
            Zip = zip;
        }
    }
}