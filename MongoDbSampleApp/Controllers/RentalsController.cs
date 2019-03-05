using MongoDbSampleApp.App_Start.Models;
using MongoDbSampleApp.App_Start.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MongoDbSampleApp.App_Start;
using MongoDB.Bson;
using System.Data;
using System.Data.SqlClient;
using System.Data.Sql;
using Dapper;
using System.Diagnostics;
using System.IO;
using MongoDbSampleApp.Properties;

namespace MongoDbSampleApp.Controllers
{
    public class RentalsController : Controller
    {
        // GET: Rentals

        public readonly RealEstateContext context = new RealEstateContext();
        string directoryPath = AppDomain.CurrentDomain.BaseDirectory;

        string sqlServerTableName = Settings.Default.SqlServerTable;
        string sqlServerTableNameColumn = Settings.Default.SqlServerColumnName;
        string sqlServerTableNameId = Settings.Default.SqlServerColumnId;

        string testString = "";

        /// <summary>
        /// Enter name of file WITHOUT .txt extension
        /// </summary>
        /// <param name="textFile"></param>
        /// <returns></returns>
        private string readStringFromTextFile(string textFile)
        {
            var file = Path.Combine(directoryPath, textFile + ".txt");
            return System.IO.File.ReadAllText(file);
        }

        public ActionResult Index()
        {
            var rentals = context.Rentals.FindAll();
            return View(rentals);
        }

        public ActionResult AdjustPrice(string id)
        {
            var rental = context.Rentals.FindOneById(new ObjectId(id));
            return View(rental);
        }

        [HttpPost]
        public ActionResult AdjustPrice(string id, AdjustPrice price)
        {

            var rental = context.Rentals.FindOneById(new ObjectId(id));
            rental.AdjustPrice(price);
            context.Rentals.Save(rental);

            return RedirectToAction("Index");
        }

        public ActionResult Post()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Post(PostRental postRental)
        {
            //for (int i = 0; i <= 1000; i++)
            //{
            postRental.Price = postRental.Price + 1;
            var rental = new Rental(postRental);
            context.Rentals.Insert(rental);
            //}
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult TestPerformance()
        {
            return View();
        }

        [HttpPost]
        public ActionResult TestPerformance(string dropdown, int numberOfIterations)
        {
            var timer = new Stopwatch();
            timer.Start();
            string s = readStringFromTextFile("TextFile");
            if (dropdown == "mongodb")
            {
                bulkInsertMongoDb(s, numberOfIterations);
            }
            else if (dropdown == "sqlserver")
            {
                bulkInsertSqlServer(s, numberOfIterations);
            }
            else
            {
                return Content("Invalid type of database seleted");
            }

            timer.Stop();

            var timerValueInSecond = timer.ElapsedMilliseconds / 1000;

            return Content($"<p>Database Type: {dropdown}</p><p>Elapsed Time: {timerValueInSecond} Seconds</p>");
        }

        private void bulkInsertMongoDb(string insertString, int numberOfIterations)
        {
            for (int i = 1; i <= numberOfIterations; i++)
            {
                Rental postRental = new Rental
                {
                    //$"Test Address {i + 1}"
                    Price = i + 1,
                    Address = new List<string> { $"Test Address {i + 1}" },
                    Description = insertString ,
                    NumberOfRooms = new Random().Next(0,10)
                };
                context.Rentals.Insert(postRental);
            }
        }

        private void bulkInsertSqlServer(string insertString, int numberOfIterations)
        {
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["parcelheroconnect"].ConnectionString))
            {
                for (int i = 1; i <= numberOfIterations; i++)
                {
                    conn.Execute($"insert into {sqlServerTableName}({sqlServerTableNameId},{sqlServerTableNameColumn}) VALUES ('{i}','{insertString}')", null, null, 600, CommandType.Text);

                    //PostRental postRental = new PostRental
                    //{
                    //    Price = i + 1,
                    //    Address = insertString
                    //};
                    //context.Rentals.Insert(postRental);
                }
            }
        }

    }
}