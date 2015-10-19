using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JqueyDataTables.Controllers
{
    public class HomeController : Controller
    {
        private northwindEntities db = new northwindEntities();
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
        public ActionResult Customers()
        {
            var customers = db.Customers.ToList();
            return View(customers);
        }
    }
}