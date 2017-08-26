using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WaitlListMongoDB.Models
{
    public class AvgWaitSettings
    {
        [Display(Name = "Breakfast Start Time")]
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        [BsonRepresentation(BsonType.Document)]
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime BreakfastStartTime {get;set;} = DateTime.Parse("1/1/1900 09:00:00");
        [Display(Name = "Breakfast End Time")]
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        [BsonRepresentation(BsonType.Document)]
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime BreakfastEndTime {get;set;} = DateTime.Parse("1/1/1900 11:00:00");
        [Display(Name = "Lunch Start Time")]
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        [BsonRepresentation(BsonType.Document)]
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime LunchStartTime {get;set;} = DateTime.Parse("1/1/1900 11:01:00");
        [Display(Name = "Lunch End Time")]
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        [BsonRepresentation(BsonType.Document)]
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime LunchEndTime { get; set; } = DateTime.Parse("1/1/1900 15:00:00");
        [Display(Name = "Dinner Start Time")]
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        [BsonRepresentation(BsonType.Document)]
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime DinnerStartTime {get;set;} = DateTime.Parse("1/1/1900 15:01:00");
        [Display(Name = "Dinner End Time")]
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        [BsonRepresentation(BsonType.Document)]
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime DinnerEndTime { get; set; } = DateTime.Parse("1/1/1900 23:00:00");
        [Display(Name = "Include Past Lists")]
        public bool IncludePastLists { get; set; }

        public AvgWaitSettings(DateTime breakfastStartTime, DateTime breakfastEndTime, DateTime lunchStartTime, DateTime lunchEndTime, DateTime dinnerStartTime, DateTime dinnerEndTime, bool includePastLists)
        {
            BreakfastStartTime = breakfastStartTime;
            BreakfastEndTime = breakfastEndTime;
            LunchStartTime = lunchStartTime;
            LunchEndTime = lunchEndTime;
            DinnerStartTime = dinnerStartTime;
            DinnerEndTime = dinnerEndTime;
            IncludePastLists = includePastLists;
        }
    }
}