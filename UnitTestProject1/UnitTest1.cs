using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDbSampleApp.App_Start;
using MongoDbSampleApp.App_Start.Models;
using MongoDB.Bson;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void ToDocument_RentalWithAnId_IdIsRepresentedAsAnObjectID()
        {
            var rental = new Rental();
            rental.Id = ObjectId.GenerateNewId().ToString();

            var bsonDocument = rental.ToBsonDocument();

            Assert.AreEqual(bsonDocument["_id"].BsonType, BsonType.ObjectId);
        }
    }
}
