using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WaitlListMongoDB.Models
{
    public class Person
    {
        public enum Status
        {
            Seated,
            Deleted,
            Waiting
        };

        [BsonId]
        public ObjectId Id { get; set; }
        [Display(Name = "First")]
        [BsonDefaultValue("")]
        [StringLength(30)]
        public string FirstName { get; set; }
        [Display(Name = "Last")]
        [BsonDefaultValue("")]
        [StringLength(50)]
        public string LastName { get; set; }
        [Display(Name = "Party Size")]
        [BsonDefaultValue("")]
        [Range(1,500)]
        public int PartySize { get; set; } = 1;
        [Display(Name = "Check In Time")]
        [BsonRepresentation(BsonType.Document)]
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime CheckIn { get; set; } = DateTime.Now;
        [BsonRepresentation(BsonType.Document)]
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime Seated { get; set; } = DateTime.MaxValue;
        [BsonRepresentation(BsonType.Document)]
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime CheckOut { get; set; } = DateTime.MaxValue;
        public Status PersonStatus { get; set; } = Status.Waiting;

        public ContactInfo ContactInfo { get; set; }

        public Person()
        {
            ContactInfo = new ContactInfo();
        }

        public Person(ObjectId id, string firstName, string lastName, int partySize, DateTime checkIn, DateTime checkOut, ContactInfo contactInfo)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            PartySize = partySize;
            CheckIn = checkIn;
            CheckOut = checkOut;
            ContactInfo = contactInfo;
        }

        public Person(ObjectId id, string firstName, string lastName, int partySize, DateTime checkIn, DateTime checkOut, string homePhone, string cellPhone, string workPhone, string email, string line1, string line2, string city, string state, int zip)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            PartySize = partySize;
            CheckIn = checkIn;
            CheckOut = checkOut;

            Address a1 = new Address(line1, line2, city, state, zip);
            ContactInfo c1 = new ContactInfo(homePhone, cellPhone, workPhone, email, a1);

            ContactInfo = c1;
        }

        public string ObjIdToString()
        {
            return Id.ToString();
        }

        public string GetTimeWaited()
        {
            TimeSpan result = new TimeSpan();
            if (Seated == DateTime.MaxValue)
            {
                result = DateTime.Now - CheckIn;
            }
            else
            {
                result = Seated - CheckIn;
            }


            return string.Format($"Hrs: {result.Hours} Mins: {result.Minutes}");
        }

        public TimeSpan GetTimeWaitedSpan()
        {
            TimeSpan result = new TimeSpan();
            if (Seated == DateTime.MaxValue)
            {
                result = DateTime.Now - CheckIn;
            }
            else
            {
                result = Seated - CheckIn;
            }
            return result;
        }

        public string GetCellPhoneNum()
        {
            return string.Format($"{ContactInfo.CellPhone:(###)###-####}");
        }

        public string GetCheckIn()
        {
            if (CheckIn == DateTime.MaxValue)
            {
                return string.Empty;
            }
            else
            {
                return CheckIn.ToString();
            }
        }

        public string GetCheckOut()
        {
            if (CheckOut == DateTime.MaxValue)
            {
                return string.Empty;
            }
            else
            {
                return CheckOut.ToString();
            }
        }

        public string GetSeated()
        {
            if (Seated == DateTime.MaxValue)
            {
                return string.Empty;
            }
            else
            {
                return Seated.ToString();
            }
        }
    }
}