﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MongoDbSampleApp.App_Start.ViewModels
{
    public class PostRental
    {
        public string Description { get; set; }
        public int NumberOfRooms { get; set; }

        public decimal Price { get; set; }

        public string Address { get; set; }
    }
}