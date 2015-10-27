using JqueyDataTables.Models;
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
            return View();
        }
        public ActionResult GetDataCustomers(jQueryDataTableParams param)
        {
            //Traer registros
            IQueryable<Customers> memberCol = db.Customers.AsQueryable();

            //Manejador de filtros
            int totalCount = memberCol.Count();
            IEnumerable<Customers> filteredMembers = memberCol;
            if (!string.IsNullOrEmpty(param.sSearch))
            {
                filteredMembers = memberCol
                        .Where(m => m.CompanyName.Contains(param.sSearch) ||
                           m.ContactName.Contains(param.sSearch) ||
                           m.ContactTitle.Contains(param.sSearch) ||
                           m.Address.Contains(param.sSearch) ||
                           m.City.Contains(param.sSearch) ||
                           m.Region.Contains(param.sSearch) ||
                           m.PostalCode.Contains(param.sSearch) ||
                           m.Country.Contains(param.sSearch) ||
                           m.Phone.Contains(param.sSearch) ||
                           m.Fax.Contains(param.sSearch));
            }
            //Manejador de orden
            var sortIdx = Convert.ToInt32(Request["iSortCol_0"]);
            Func<Customers, string> orderingFunction =
                                (
                                m => sortIdx == 0 ? m.CompanyName :
                                  sortIdx == 1 ? m.ContactName :
                                  sortIdx == 2 ? m.ContactTitle :
                                  sortIdx == 3 ? m.Address :
                                  sortIdx == 4 ? m.City :
                                  sortIdx == 5 ? m.Region :
                                  sortIdx == 6 ? m.PostalCode :
                                  sortIdx == 7 ? m.Country :
                                  sortIdx == 8 ? m.Phone :
                                  sortIdx == 9 ? m.Fax :
                                  m.CustomerID
                                 );
            var sortDirection = Request["sSortDir_0"]; // asc or desc  
            if (sortDirection == "asc")
                filteredMembers = filteredMembers.OrderBy(orderingFunction);
            else
                filteredMembers = filteredMembers.OrderByDescending(orderingFunction);
            var displayedMembers = filteredMembers
                     .Skip(param.iDisplayStart)
                     .Take(param.iDisplayLength);

            //Manejardo de resultados
            var result = from a in displayedMembers
                         select new
                         {
                             a.CompanyName,
                             a.ContactName,
                             a.ContactTitle,
                             a.Address,
                             a.City,
                             a.Region,
                             a.PostalCode,
                             a.Country,
                             a.Phone,
                             a.Fax


                         };
            //Se devuelven los resultados por json
            return Json(new
            {
                sEcho = param.sEcho,
                iTotalRecords = totalCount,
                iTotalDisplayRecords = filteredMembers.Count(),
                aaData = result
            },
               JsonRequestBehavior.AllowGet);
        }
    }
}