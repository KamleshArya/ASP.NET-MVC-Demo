using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcAuction.Models;
using System.Net;

namespace MvcAuction.Controllers
{
    public class AuctionsController : Controller
    {
        [AllowAnonymous]
        [OutputCache(Duration = 1)]
        public ActionResult Index()
        {
            var db=new AuctionsDataContext();
            var auctions = db.Auctions.ToArray();
            return View(auctions);
        }

        public ActionResult TempDataDemo() 
        {
            TempData["SuccessMessage"] = "The action succeeded";
            return RedirectToAction("Index");
        }

        [OutputCache(Duration = 10)]
        public ActionResult Auction(long id)
        {
            var db = new AuctionsDataContext();
            var auction = db.Auctions.Find(id);
            //ViewData["Auction"] = auction; alternate
            return View(auction);
        }

        [HttpGet]
        //[Authorize(Roles="Admin")]
        public ActionResult Create() 
        {
            var categoryList = new SelectList(new[] { "Computer Science", "Electronics", "Electrical", "Mining", "Mechanical" });
            ViewBag.CategoryList = categoryList;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Bid(Bid bid)
        {
            var db = new AuctionsDataContext();
            var auction = db.Auctions.Find(bid.AuctionId);
            if (auction == null) ModelState.AddModelError("AuctionId", "Auction not found !");
            else if (auction.CurrentPrice >= bid.Amount) ModelState.AddModelError("Amount", "Bid Amount must exceed current bid !");
            else
            {
                bid.Username = User.Identity.Name;
                auction.Bids.Add(bid);
                auction.CurrentPrice = bid.Amount;
                db.SaveChanges();
            }
            if (!Request.IsAjaxRequest())
                return RedirectToAction("Auction", new { id = bid.AuctionId });

            //return PartialView("_CurrentPrice", auction);
            return Json(new
            {
                CurrentPrice=bid.Amount.ToString("C"),
                BidCount=auction.BidCount
            });
        }

        [HttpPost]
        public ActionResult Create([Bind(Exclude="CurrentPrice")]Models.Auction auction) 
        {
            if (ModelState.IsValid)
            {
                //Saving inside database
                var db = new AuctionsDataContext();
                db.Auctions.Add(auction);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return Create();
         }
    }
}
