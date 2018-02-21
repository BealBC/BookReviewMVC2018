using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookReviewMVC2018.Models;

namespace BookReviewMVC2018.Controllers
{
    public class NewReviewController : Controller
    {
        BookReviewDbEntities db = new BookReviewDbEntities();
        // GET: NewReview
        public ActionResult Index()
        {
            if (Session["reviewerKey"] == null)
            {
                MessageClass m = new MessageClass();
                m.MessageText = "You must be logged in to add a review.";
                return RedirectToAction("Result", m);
            }
            ViewBag.BookKey = new SelectList(db.Books, "BookKey", "BookTitle");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "BookKey,ReviewerKey, ReviewDate, ReviewTitle, ReviewRating, ReviewText")] Review r)
        {
            r.ReviewerKey = (int) Session["reviewerKey"];
            r.ReviewDate = DateTime.Now;
            db.Reviews.Add(r);
            db.SaveChanges();
            MessageClass m = new MessageClass();
            m.MessageText = "Thank you for your review";
            return View("Result", m);
        }
    public ActionResult Result(MessageClass m)
    {
        return View(m);
    }
    }
}