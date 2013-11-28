using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcAuction.Models;

namespace MvcAuction.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        [OutputCache(Duration = 3)]
        public ActionResult Index()
        {
            ViewBag.Message = "This page was rendered at "+DateTime.Now;
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [OutputCache(Duration = 3600)]
        public ActionResult CategoryNavigation()
        {
            var db = new AuctionsDataContext();
            var categories = db.Auctions.Select(x => x.Category).Distinct();
            ViewBag.Categories = categories.ToArray();
            return PartialView();
        }
    }
}
