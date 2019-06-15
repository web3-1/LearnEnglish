using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TurtleEnglish.Models;
namespace TurtleEnglish.Controllers
{
    public class AdminHomePageController : Controller
    {
        private LearnEnglishEntities db = new LearnEnglishEntities();
        public IEnumerable<CommentTopic> topic { get; set; }
        public IEnumerable<UserComment> cmt { get; set; }
        // GET: Admin_HomePage
        public ActionResult adminIndex()
        {
            var model = new AdminHomePageController();
            model.topic = db.CommentTopics.ToList();
            model.cmt = db.UserComments.ToList();
            return View(model);
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
            var model = new AdminHomePageController();
            model.topic = db.CommentTopics.ToList();
            model.cmt = db.UserComments.ToList();

            return View(model);

        }
        [HttpPost]
        public ActionResult DeleteTopic(int stt)
        {
            var itemToRemove = db.CommentTopics.SingleOrDefault(x => x.stt == stt); //returns a single item.

            if (itemToRemove != null)
            {
                var itemUserCmt = db.UserComments.Where(x => x.stt == stt).ToList();
                for (int i = 0; i < itemUserCmt.Count(); i++)
                    itemUserCmt.ElementAt(i).topic = null;

                db.CommentTopics.Remove(itemToRemove);
                db.SaveChanges();
            }
            return RedirectToAction("feedbackManagement");
        }

        public ActionResult feedbackAdd(CommentTopic model)
        {
            var item = db.CommentTopics.Where(x => (x.topic == model.topic));
            if (item.Count() > 0)
            {
                TempData["msg"] = "<script>alert('Đã tồn tại topic.');</script>";
                return View();
            }
            try
            {
                CommentTopic newCmt = new CommentTopic();
                newCmt.topic = model.topic;

                db.CommentTopics.Add(newCmt);
                db.SaveChanges();
                TempData["msg"] = "<script>alert('Đã thêm topic thành công.');</script>";
            }
            catch (Exception ex)
            {

            }
            return View();
        }

        // GET: AdminHomePage/feedbackEdit/5
        public ActionResult feedbackEdit(int? stt)
        {
            if (stt == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CommentTopic cmt = db.CommentTopics.Where(x => (x.stt == stt)).FirstOrDefault();
            if (cmt == null)
            {
                return HttpNotFound();
            }
            return View(cmt);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult feedbackEdit([Bind(Include = "stt,topic")] CommentTopic commentTopic)
        {
            if (ModelState.IsValid)
            {

                var item = db.CommentTopics.Single(x => (x.stt == commentTopic.stt));
                string oldTopic = item.topic;
                db.CommentTopics.Remove(item);
                CommentTopic newCmt = new CommentTopic();
                newCmt.topic = commentTopic.topic;
                db.CommentTopics.Add(newCmt);

                var itemUserCmt = db.UserComments.Where(x => x.topic == oldTopic).ToList();
                for (int i = 0; i < itemUserCmt.Count(); i++)
                    itemUserCmt.ElementAt(i).topic = commentTopic.topic;

                db.SaveChanges();
                //var itemUserCmt1 = db.UserComments.Where(x => x.topic == null).ToList();
                //for (int i = 0; i < itemUserCmt1.Count(); i++)
                //    itemUserCmt1.ElementAt(i).topic = commentTopic.topic;
                //db.SaveChanges();
                return RedirectToAction("feedbackManagement");
            }
            return View(commentTopic);
        }

        [HttpPost]
        public ActionResult DeleteComment(int stt)
        {
            var itemToRemove = db.UserComments.SingleOrDefault(x => x.stt == stt); //returns a single item.

            if (itemToRemove != null)
            {
                db.UserComments.Remove(itemToRemove);
                db.SaveChanges();
            }
            return RedirectToAction("feedbackManagement");
        }

        public ActionResult commentEdit(int? stt)
        {
            if (stt == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserComment userComment = db.UserComments.Find(stt);
            if (userComment == null)
            {
                return HttpNotFound();
            }
            ViewBag.topic = new SelectList(db.CommentTopics, "topic", "topic", userComment.topic);
            return View(userComment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult commentEdit([Bind(Include = "stt,email,topic,comment,dateComment,statusCmt")] UserComment userComment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userComment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("feedbackManagement");
            }
            ViewBag.topic = new SelectList(db.CommentTopics, "topic", "topic", userComment.topic);
            return View(userComment);
        }
    }
}

