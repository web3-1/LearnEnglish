using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TurtleEnglish.Models;
namespace TurtleEnglish.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        private LearnEnglishEntities db = new LearnEnglishEntities();
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CheckLogin()
        {
            var username = Request.Form["username"];
            var pass = Request.Form["pass"];
            if (username == "admin@turtleenglish.com" && pass == "123")
                return RedirectToAction("adminIndex", "AdminHomePage");
            var result = db.UserInfoes.FirstOrDefault(x => (x.username == username && x.pass == pass));
            if (result != null)
            {
                Session["username"] = result.username;
                Session["level"] = result.levelStudy;
                return RedirectToAction("HomePage", "HomePage");
            }
            return RedirectToAction("Login");
        }
        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("HomePage", "HomePage");
        }
        public ActionResult Register(string error)
        {
            ViewBag.Message = error;
            return View();
        }
        [HttpPost]
        public ActionResult CheckRegister()
        {
            var username = Request.Form["username"];
            var pass = Request.Form["pass"];
            var passconfirm = Request.Form["passconfirm"];
            if (pass != passconfirm)
            {
                ViewBag.Message = "Pass confirm is not correct, please try again";
                return RedirectToAction("Register", "Login", new { error = ViewBag.Message });
            }
            var result = db.UserInfoes.SingleOrDefault(x => (x.username == username));
            if (result != null)
            {
                ViewBag.Message = "User name already exist, Please try another User name";
                return RedirectToAction("Register", "Login", new { error = ViewBag.Message });
            }
            UserInfo user = new UserInfo();
            user.username = username;
            user.pass = pass;
            user.levelStudy = "beginer";
            user.dateRegister = DateTime.Now;
            db.UserInfoes.Add(user);
            db.SaveChanges();
            var list = db.UserInfoes.ToList();
            Session["username"] = username;
            Session["level"] = "beginer";
            return RedirectToAction("HomePage", "HomePage");
        }
    }
}