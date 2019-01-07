using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDbSampleApp.App_Start.ViewModels;

namespace MongoDbSampleApp.App_Start.Models
{
    public class Rental
    {
       

        public Rental(PostRental postRental)
        {
            Description = postRental.Description;
            NumberOfRooms = postRental.NumberOfRooms;
            Price = postRental.Price;

            Address = (postRental.Address ?? string.Empty).Split('\n').ToList();
        }

        public Rental()
        {
        }

        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Description { get; set; }
        public int NumberOfRooms { get; set; }
        public List<string> Address { get; set; } = new List<string>();

        [BsonRepresentation(BsonType.Double)]
        public decimal Price { get; set; }
    }
}