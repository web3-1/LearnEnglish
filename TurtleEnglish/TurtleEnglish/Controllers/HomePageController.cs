using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TurtleEnglish.Models;

namespace TurtleEnglish.Controllers
{
    public class HomePageController : Controller
    {
        private LearnEnglishEntities db = new LearnEnglishEntities();

        public IEnumerable<UserInfo> user { get; set; }
        public IEnumerable<CommentTopic> commentTopic { get; set; }
        // GET: HomePage
        public ActionResult HomePage()
        {
            return View();
        }

        public ActionResult AboutUs()
        {
            return View();
        }
        public ActionResult Contact()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            var model = new HomePageController();
            model.commentTopic = db.CommentTopics.ToList();
            return View(model);
        }
        [HttpPost]
        public ActionResult Addcommemt()
        {
            var comment = Request.Form["comment"];
            var topic = Request.Form["topic"];
            UserComment cm = new UserComment();
            cm.email = Session["username"].ToString();
            cm.comment = comment;
            cm.topic = topic;
            cm.dateComment = DateTime.Now;
            cm.statusCmt = false;
            db.UserComments.Add(cm);
            db.SaveChanges();
            return RedirectToAction("Contact", "HomePage");
        }
        public ActionResult Profile(string username)
        {
            if (username == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            UserInfo result = db.UserInfoes.Where(x => x.username == username).FirstOrDefault();

            if (result == null)
            {
                return HttpNotFound();
            }
            return View(result);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Profile([Bind(Include = "username,pass,fullname,dateOfBirth,sdt,addr,levelStudy")] UserInfo user)
        {
            if (ModelState.IsValid)
            {
                string username = Session["username"].ToString();
                var item = db.UserInfoes.SingleOrDefault(x => x.username == username);
                if (Request.Form["pass"] != "")
                    item.pass = Request.Form["pass"];
                var dateOfBirth = Request.Form["dateOfBirth"];
                try
                {
                    item.dateOfBirth = DateTime.Parse(dateOfBirth);
                }
                catch {
                    item.dateOfBirth = null;
                };
                item.sdt = Int32.Parse(Request.Form["sdt"]);
                item.fullname = Request.Form["fullname"];
                item.addr = Request.Form["addr"];
                db.SaveChanges();
                return View(user);
            }
            return RedirectToAction("HomePage");
        }
    }
}