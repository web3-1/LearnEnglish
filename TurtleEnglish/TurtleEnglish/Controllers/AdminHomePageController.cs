using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TurtleEnglish.Controllers
{
    public class AdminHomePageController : Controller
    {
        // GET: Admin_HomePage
        public ActionResult adminIndex()
        {
            return View();
        }

        public ActionResult userManagement()
        {
            return View();
        }

        public ActionResult vocabularyManagement()
        {
            return View();
        }

        public ActionResult feedbackManagement()
        {
            return View();
            
        }

        public ActionResult feedbackAdd()
        {
            return View();
        }
    }
}