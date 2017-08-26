using MongoDB.Bson.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WaitlListMongoDB.Models;

namespace WaitlListMongoDB
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            if (!BsonClassMap.IsClassMapRegistered(typeof(Person)))
            {
                BsonClassMap.RegisterClassMap<Person>(cm =>
                {
                    cm.AutoMap();
                    cm.MapCreator(p => new Person(p.Id, p.FirstName, p.LastName, p.PartySize, p.CheckIn, p.CheckOut, p.ContactInfo.HomePhone, p.ContactInfo.CellPhone, p.ContactInfo.WorkPhone, p.ContactInfo.Email, p.ContactInfo.Address.Line1, p.ContactInfo.Address.Line2, p.ContactInfo.Address.City, p.ContactInfo.Address.State, p.ContactInfo.Address.Zip));
                });
            }
        }
    }
}
