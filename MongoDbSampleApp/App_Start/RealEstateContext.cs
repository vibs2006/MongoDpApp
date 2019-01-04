using MongoDB.Driver;
using MongoDbSampleApp.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MongoDbSampleApp.App_Start
{
    public class RealEstateContext
    {
        public MongoDatabase Database;
        public RealEstateContext()
        {
            var client = new MongoClient(Settings.Default.RealEstateConnectionString);
            var server = client.GetServer();
            Database = server.GetDatabase(Settings.Default.RealEstateDatabase);
        }
    }
}