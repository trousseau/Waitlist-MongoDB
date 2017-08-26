using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WaitlListMongoDB.Models
{
    public class UserSettings
    {
        [BsonId]
        public ObjectId _id { get; set; }
        public string UserId { get; set; }
        public AvgWaitSettings AvgWaitSettings { get; set; }

        public UserSettings()
        {

        }

        public UserSettings(string userId, AvgWaitSettings avgWaitSettings)
        {
            UserId = userId;
            AvgWaitSettings = avgWaitSettings;
        }

        public UserSettings(string userId, DateTime breakfastStartTime, DateTime breakfastEndTime, DateTime lunchStartTime, DateTime lunchEndTime, DateTime dinnerStartTime, DateTime dinnerEndTime, bool includePastLists)
        {
            UserId = userId;
            AvgWaitSettings = new AvgWaitSettings(breakfastStartTime, breakfastEndTime, lunchStartTime, lunchEndTime, dinnerStartTime, dinnerEndTime, includePastLists);
        }

        public UserSettings(ObjectId id, string userId, DateTime breakfastStartTime, DateTime breakfastEndTime, DateTime lunchStartTime, DateTime lunchEndTime, DateTime dinnerStartTime, DateTime dinnerEndTime, bool includePastLists)
        {
            _id = id;
            UserId = userId;
            AvgWaitSettings = new AvgWaitSettings(breakfastStartTime, breakfastEndTime, lunchStartTime, lunchEndTime, dinnerStartTime, dinnerEndTime, includePastLists);
        }
    }
}