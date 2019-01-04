using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MongoDB.Driver;
using MongoDbSampleApp.Properties;
using MongoDbSampleApp.App_Start;

namespace MongoDbSampleApp.Controllers
{
    public class HomeController : Controller
    {
        public RealEstateContext context;
        public HomeController()
        {
            context = new RealEstateContext();
        }
        public ActionResult Index()
        {
            //return View();
            context.Database.GetStats();
            return Json(context.Database.Server, JsonRequestBehavior.AllowGet);
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
    }
}