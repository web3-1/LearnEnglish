using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TurtleEnglish.Models;

namespace TurtleEnglish.Controllers
{
    public class VocabularyController : Controller
    {
        private LearnEnglishEntities db = new LearnEnglishEntities();
        public IEnumerable<Vocabulary> vocabList { get; set; }


        // GET: Vocabulary
        public ActionResult Index(string diem)
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            var model = new VocabularyController();
            string level = Session["level"].ToString();
            model.vocabList = db.Vocabularies.Where(x => x.levelStudy == level).ToList();
            int num = 1;
            foreach (var item in model.vocabList)
            {
                item.id = num;
                num++;
            }
            ViewBag.Message = diem;
            return View(model);
        }
        public ActionResult Test()
        {
            var model = new VocabularyController();
            string level = Session["level"].ToString();
            model.vocabList = db.Vocabularies.Where(x=>x.levelStudy==level).ToList();
            int num = 1;
            foreach (var item in model.vocabList)
            {
                item.id = num;
                num++;

            }

            return View(model);
        }
        [HttpPost]
        public ActionResult Checktest()
        {
            var result1 = Request.Form["1"];
            var result2 = Request.Form["2"];
            var result3 = Request.Form["3"];
            var result4 = Request.Form["4"];
            var result5 = Request.Form["5"];
            var result6 = Request.Form["6"];
            var result7 = Request.Form["7"];
            var result8 = Request.Form["8"];
            var model = new VocabularyController();
            string level = Session["level"].ToString();
            model.vocabList = db.Vocabularies.Where(x => x.levelStudy == level).ToList();
            List<string> a = new List<string>();
            a.Add(result1);
            a.Add(result2);
            a.Add(result3);
            a.Add(result4);
            a.Add(result5);
            a.Add(result6);
            a.Add(result7);
            a.Add(result8);
            int score = 0;
            int i = 0;
            foreach (var item in model.vocabList)
            {
                if (item.mean == a[i])
                {
                    score++;
                }
                i++;
                if (i == 8)
                {
                    break;
                }
            }
            if (score == 8)
            {
                string username = Session["username"].ToString();
                var user = db.UserInfoes.Where(x => x.username == username).FirstOrDefault();
                switch (user.levelStudy)
                {
                    case "Beginer":
                        user.levelStudy = "Intermediate";
                        Session["level"] = "Intermediate";
                        break;
                    case "Intermediate":
                        user.levelStudy = "Expert";
                        Session["level"] = "Expert";
                        break;
                }
                db.SaveChanges();
            }
            ViewData["score"] = score;
            return RedirectToAction("Index", "Vocabulary", new { diem = ViewData["score"] });

        }
    }
}