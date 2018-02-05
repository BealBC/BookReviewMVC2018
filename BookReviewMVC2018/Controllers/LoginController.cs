using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookReviewMVC2018.Models;

namespace BookReviewMVC2018.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "UserName, Password")]LoginClass lc)
        {
            BookReviewDbEntities db = new BookReviewDbEntities();
            int loginResult = db.usp_ReviewerLogin(lc.UserName, lc.Password);
            if (loginResult != -1)
            {
                var uid = (from r in db.Reviewers
                           where r.ReviewerUserName.Equals(lc.UserName)
                           select r.ReviewerKey).FirstOrDefault();
                int rKey = (int)uid;
                Session["reviewerKey"] = rKey;

                MessageClass msg = new MessageClass();
                msg.MessageText = "Thank You, " + lc.UserName + " for logging in. you can now donate or apply fo assistance";
                return RedirectToAction("Result", msg);
            }
            MessageClass message = new MessageClass();
            message.MessageText = "Invalid Login";
            return View("Result", message);
        }
        public ActionResult Result(MessageClass m)
        {
            return View(m);
        }
    }
}