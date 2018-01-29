using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookReviewMVC2018.Models;

namespace BookReviewMVC2018.Controllers
{
    public class RegistrationController : Controller
    {
        BookReviewDbEntities db = new BookReviewDbEntities();
        // GET: Registration
        public ActionResult Index()
        {
            return View(db.Reviewers.ToList());
        }
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "ReviewerUserName, ReviewerFirstName, ReviewerLastName, ReviewerEmail, ReviewPlainPassword")]Reviewer r)
        {
            int result = db.usp_NewReviewer(r.ReviewerUserName, r.ReviewerFirstName, r.ReviewerLastName, r.ReviewerEmail, r.ReviewPlainPassword);

            if(result !=-1)
            {
                return RedirectToAction("Index");
            }

            return View();
        }
    }
}