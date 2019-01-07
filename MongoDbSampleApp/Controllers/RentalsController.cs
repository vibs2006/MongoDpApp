using MongoDbSampleApp.App_Start.Models;
using MongoDbSampleApp.App_Start.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MongoDbSampleApp.App_Start;

namespace MongoDbSampleApp.Controllers
{
    public class RentalsController : Controller
    {
        // GET: Rentals

        public readonly RealEstateContext context = new RealEstateContext();

        public ActionResult Index()
        {
            var rentals = context.Rentals.FindAll();
            return View(rentals);
        }
        
        public ActionResult Post()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Post(PostRental postRental)
        {
            var rental = new Rental(postRental);
            context.Rentals.Insert(rental);
            return RedirectToAction("Index");
        }


    }
}