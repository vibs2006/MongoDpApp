using MongoDB.Driver;
using MongoDbSampleApp.App_Start.Models;
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
            //Refer - https://stackoverflow.com/questions/29457098/c-sharp-mongodb-driver-getserver-is-gone-what-now
            var server = client.GetServer();
            Database = server.GetDatabase(Settings.Default.RealEstateDatabase);
        }

        public MongoCollection<Rental> Rentals
        {
            get
            {
                return Database.GetCollection<Rental>("rentals");
            }
        }
    }
}