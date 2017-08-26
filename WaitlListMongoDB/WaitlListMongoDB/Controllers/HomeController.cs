using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WaitlListMongoDB.Models;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace WaitlListMongoDB.Controllers
{
    public class HomeController : Controller
    {
        protected static IMongoClient _client;
        protected static IMongoDatabase _database;

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Waitlist()
        {
            return View();
        }

        public ActionResult PastList()
        {
            return View();
        }

        public ActionResult Create()
        {
            return PartialView();
        }

        public ActionResult Edit(string id)
        {
            _client = new MongoClient();
            _database = _client.GetDatabase("Waitlist");
            var collection = _database.GetCollection<Person>("People");

            var objId = new ObjectId();
            ObjectId.TryParse(id, out objId);

            var filter = Builders<Person>.Filter.Eq("_id", objId);

            var p1 = collection.Find(filter).First();

            return PartialView(p1);
        }

        public ActionResult EditDetails(string id)
        {
            _client = new MongoClient();
            _database = _client.GetDatabase("Waitlist");
            var collection = _database.GetCollection<Person>("People");

            var objId = new ObjectId();
            ObjectId.TryParse(id, out objId);

            var filter = Builders<Person>.Filter.Eq("_id", objId);

            var p1 = collection.Find(filter).First();

            return View(p1);
        }

        public ActionResult Export()
        {
            return PartialView();
        }

        [HttpGet]
        public ActionResult AvgWaitSettings()
        {
            return RedirectToAction("AvgWaitSettingsPartial");
        }

        [Authorize]
        public ActionResult AvgWaitSettingsPartial()
        {
            var userId = User.Identity.GetUserId();

            _client = new MongoClient();
            _database = _client.GetDatabase("Waitlist");
            var collection = _database.GetCollection<UserSettings>("UserSettings");

            UserSettings us1 = new UserSettings();

            us1 = collection.Find(user => user.UserId == userId).First();

            long count = collection.Find(user => user.UserId == userId).Count();

            if (count > 0)
            {
                return PartialView("AvgWaitSettingsPartial", us1);
            }
            else
            {
                return PartialView("AvgWaitSettingsPartial");
            }
        }

        public ActionResult Settings()
        {
            return View();
        }

        public async Task<ActionResult> ListViewAsync()
        {
            _client = new MongoClient();
            _database = _client.GetDatabase("Waitlist");

            List<Person> people = new List<Person>();

            var collection = _database.GetCollection<BsonDocument>("People");
            var collection2 = _database.GetCollection<Person>("People");

            var filter = "{ PersonStatus: 2 }";
            var count = 0;

            using (var cursor = await collection.FindAsync(filter))
            {
                while (await cursor.MoveNextAsync())
                {
                    var batch = cursor.Current;
                    foreach (var document in batch)
                    {
                        var p1 = BsonSerializer.Deserialize<Person>(document);
                        people.Add(p1);
                        count++;
                    }
                }
            }

            var docs = collection.ToJson();

            return PartialView("ListPartial", people);
        }

        public async Task<ActionResult> PastListViewAsync()
        {
            _client = new MongoClient();
            _database = _client.GetDatabase("Waitlist");

            List<Person> people = new List<Person>();

            var collection = _database.GetCollection<BsonDocument>("People");
            var collection2 = _database.GetCollection<Person>("People");

            var filterBuilder = Builders<BsonDocument>.Filter;
            var filter = filterBuilder.Eq("PersonStatus", 0) | filterBuilder.Eq("PersonStatus", 1);

            var count = 0;

            using (var cursor = await collection.FindAsync(filter))
            {
                while (await cursor.MoveNextAsync())
                {
                    var batch = cursor.Current;
                    foreach (var document in batch)
                    {
                        var p1 = BsonSerializer.Deserialize<Person>(document);
                        people.Add(p1);
                        count++;
                    }
                }
            }

            var docs = collection.ToJson();

            return PartialView("PastListPartial", people);
        }

        public ActionResult NewPerson(Person person)
        {
            _client = new MongoClient();
            _database = _client.GetDatabase("Waitlist");

            var collection = _database.GetCollection<BsonDocument>("People");

            var document = person.ToBsonDocument();

            collection.InsertOne(document);

            return RedirectToAction("Waitlist");
        }

        public ActionResult SavePerson(Person person, string id)
        {
            ObjectId oId = new ObjectId();
            ObjectId.TryParse(id, out oId);
            person.Id = oId;

            _client = new MongoClient();
            _database = _client.GetDatabase("Waitlist");
            var collection = _database.GetCollection<Person>("People");

            var filter = Builders<Person>.Filter.Eq("_id", person.Id);

            var p1 = collection.ReplaceOne(filter,person);

            return RedirectToAction("Waitlist");
        }

        public ActionResult Details(string id)
        {
            _client = new MongoClient();
            _database = _client.GetDatabase("Waitlist");
            var collection = _database.GetCollection<BsonDocument>("People");

            var objId = new ObjectId();
            ObjectId.TryParse(id, out objId);
            var filter = Builders<BsonDocument>.Filter.Eq("_id", objId);

            var result = collection.Find(filter).First();

            var p1 = BsonSerializer.Deserialize<Person>(result);

            return View(p1);
        }

        public async Task<ActionResult> Seat(string id)
        {
            _client = new MongoClient();
            _database = _client.GetDatabase("Waitlist");
            var collection = _database.GetCollection<BsonDocument>("People");

            var objId = new ObjectId();
            ObjectId.TryParse(id, out objId);

            var filter = Builders<BsonDocument>.Filter.Eq("_id", objId);
            var update = Builders<BsonDocument>.Update.Set("PersonStatus", Person.Status.Seated).Set("Seated", DateTime.Now);
            var result = await collection.UpdateOneAsync(filter, update);

            return RedirectToAction("Waitlist");
        }

        public async Task<ActionResult> Delete(string id)
        {
            _client = new MongoClient();
            _database = _client.GetDatabase("Waitlist");
            var collection = _database.GetCollection<BsonDocument>("People");

            var objId = new ObjectId();
            ObjectId.TryParse(id, out objId);

            var filter = Builders<BsonDocument>.Filter.Eq("_id", objId);
            var update = Builders<BsonDocument>.Update.Set("PersonStatus", Person.Status.Deleted);
            var result = await collection.UpdateOneAsync(filter, update);

            return RedirectToAction("Waitlist");
        }

        [HttpPost]
        public async Task<ActionResult> ExportList()
        {
            string fileType = Request["Include"].ToString();
            string fileFormat = Request["FileFormat"].ToString();
            var startDate = Request["StartDate"].ToString();
            var endDate = Request["EndDate"].ToString();


            _client = new MongoClient();
            _database = _client.GetDatabase("Waitlist");

            List<Person> people = new List<Person>();

            var collection = _database.GetCollection<BsonDocument>("People");
            var collection2 = _database.GetCollection<Person>("People");

            var filterBuilder = Builders<BsonDocument>.Filter;
            var filter = filterBuilder.Eq("PersonStatus", 0) | filterBuilder.Eq("PersonStatus", 1);

            var count = 0;

            using (var cursor = await collection.FindAsync(filter))
            {
                while (await cursor.MoveNextAsync())
                {
                    var batch = cursor.Current;
                    foreach (var document in batch)
                    {
                        var p1 = BsonSerializer.Deserialize<Person>(document);
                        people.Add(p1);
                        count++;
                    }
                }
            }

            var docs = collection.ToJson();

            return PartialView("PastListPartial", people);
        }

        public ActionResult SaveAvgWaitTimeSettings()
        {
            var userId = User.Identity.GetUserId();

            ObjectId oId = new ObjectId();

            DateTime bfastStart = new DateTime();
            DateTime bfastEnd = new DateTime();
            DateTime lunchStart = new DateTime();
            DateTime lunchEnd = new DateTime();
            DateTime dinnerStart = new DateTime();
            DateTime dinnerEnd = new DateTime();
            bool includePastList = false;

            DateTime.TryParse(Request["AvgWaitSettings.BreakfastStartTime"], out bfastStart);
            DateTime.TryParse(Request["AvgWaitSettings.BreakfastEndTime"], out bfastEnd);
            DateTime.TryParse(Request["AvgWaitSettings.LunchStartTime"], out lunchStart);
            DateTime.TryParse(Request["AvgWaitSettings.LunchEndTime"], out lunchEnd);
            DateTime.TryParse(Request["AvgWaitSettings.DinnerStartTime"], out dinnerStart);
            DateTime.TryParse(Request["AvgWaitSettings.DinnerEndTime"], out dinnerEnd);

            bool.TryParse(Request["AvgWaitSettings.IncludePastList"],out includePastList);

            _client = new MongoClient();
            _database = _client.GetDatabase("Waitlist");
            var collection = _database.GetCollection<UserSettings>("UserSettings");

            var filter = Builders<UserSettings>.Filter.Eq("UserId", userId);

            var results = collection.Find(filter);
            long count = results.Count();

            UserSettings us1 = new UserSettings();

            us1 = collection.Find(user => user.UserId == userId).First();

            ObjectId.TryParse(us1._id.ToString(), out oId);

            if (count == 0)
            {
                try
                {
                    UserSettings us = new UserSettings(oId, userId, bfastStart, bfastEnd, lunchStart, lunchEnd, dinnerStart, dinnerEnd, includePastList);
                    collection.InsertOneAsync(us);
                }
                catch (Exception e)
                {
                    TempData["notice"] = "There was an error updating the record. Please try again.";
                }
            }
            else
            {
                try
                {
                    UserSettings us = new UserSettings(oId, userId, bfastStart, bfastEnd, lunchStart, lunchEnd, dinnerStart, dinnerEnd, includePastList);
                    collection.ReplaceOne(filter, us);
                }
                catch (Exception e)
                {
                    TempData["notice"] = "There was an error updating the record. Please try again.";
                }
            }
            return RedirectToAction("Settings");
        }
    }
}