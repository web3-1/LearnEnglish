using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
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
        public IEnumerable<UserInfo> user { get; set; }

        public IEnumerable<Vocabulary> vocab { get; set; }

        //danh sách user mới tạo trong vòng 7 ngày gần nhất
        public IEnumerable<UserInfo> userNewInSevenDays { get; set; }
        public List<string> colors = new List<string>();
        public AdminHomePageController()
        {
            colors.Add("#BDC3C7");
            colors.Add("#9B59B6");
            colors.Add("#E74C3C");
            colors.Add("#26B99A");
            colors.Add("#3498DB");
        }

        //biến thống kê level học
        public IEnumerable<StatisticsClass.LevelStudyStatistics> _levelStudyStatistics { get; set; }
        public IEnumerable<string> UserLevel { get; set; }
        public IEnumerable<int> sumOfUserAtALevel { get; set; }
        //thống kê level từ vựng
        public IEnumerable<StatisticsClass.VocabularyLevelStatistics> _vocabLevelStatistics { get; set; }
        //thống kê feedback
        public IEnumerable<StatisticsClass.FeedbackStatistics> _feedbackStatistics { get; set; }
        public IEnumerable<string> FeedbackName { get; set; }
        public IEnumerable<int> sumFeedbackTopic { get; set; }




        // GET: Admin_HomePage
        public ActionResult adminIndex()
        {
            var model = new AdminHomePageController();
            model.topic = db.CommentTopics.ToList();
            model.cmt = db.UserComments.ToList();
            model.user = db.UserInfoes.ToList();
            model.vocab = db.Vocabularies.ToList();
            
            //lọc danh sách user mới vào 1 tuần này
            List<UserInfo> newUser = new List<UserInfo>();
            foreach (var item in db.UserInfoes.ToList())
            {
                try
                {
                    TimeSpan time = DateTime.Now - (DateTime)item.dateRegister;
                    int TongSoNgay = time.Days;
                    if (TongSoNgay <= 7)
                        newUser.Add(item);
                }
                catch{}
            }
            model.userNewInSevenDays = newUser;

            //thống kê level user
            List<StatisticsClass.LevelStudyStatistics> levelStatistics = new List<StatisticsClass.LevelStudyStatistics>();
            List<string> userLevel = new List<string>();
            List<int> sumUserLevel = new List<int>();
            string[] userLevel1 = new string[20];
            int[] sumUserLevel1 = new int[20];
            foreach (var item in db.UserInfoes.ToList())
            {
                bool isAlive = false;
                foreach (var itemUser in levelStatistics)
                {
                    if (item.levelStudy == itemUser.level)
                    {
                        itemUser.sum++;
                        isAlive = true;
                    }
                }
                if (!isAlive)
                {
                    StatisticsClass.LevelStudyStatistics temp = new StatisticsClass.LevelStudyStatistics();
                    temp.level = item.levelStudy;
                    temp.sum = 1;
                    levelStatistics.Add(temp);
                    userLevel.Add(item.levelStudy);
                    }
            }
            userLevel1 = userLevel.ToArray();
            //truyền color và percent vào cho mỗi item thống kê
            for (int i = 0; i < levelStatistics.Count(); i++)
            {
                levelStatistics[i].color = Colors.listColors.ElementAt(i);
                levelStatistics[i].percent = levelStatistics[i].sum * 100 / db.UserInfoes.Count();
                sumUserLevel.Add(levelStatistics[i].sum);
            }
            sumUserLevel1 = sumUserLevel.ToArray();
            model._levelStudyStatistics = levelStatistics;
            model.UserLevel = userLevel1;
            model.sumOfUserAtALevel = sumUserLevel1;
            //thống kê vocab level
            List<StatisticsClass.VocabularyLevelStatistics> vocabLevelStatistics = new List<StatisticsClass.VocabularyLevelStatistics>();
            foreach (var item in db.Vocabularies.ToList())
            {
                bool isAlive = false;
                foreach (var item1 in vocabLevelStatistics)
                {
                    if (item1.level == item.levelStudy)
                    {
                        item1.sum++;
                        isAlive = true;
                    }
                }
                if (!isAlive)
                {
                    StatisticsClass.VocabularyLevelStatistics temp = new StatisticsClass.VocabularyLevelStatistics();
                    temp.level = item.levelStudy;
                    temp.sum = 1;
                    vocabLevelStatistics.Add(temp);
                }
            }
            foreach(var item in vocabLevelStatistics)
            {
                item.percent = item.sum * 100 / db.Vocabularies.Count();
            }
            model._vocabLevelStatistics = vocabLevelStatistics;

            //thống kê feedback
            List<StatisticsClass.FeedbackStatistics> feedbackSta = new List<StatisticsClass.FeedbackStatistics>();
            List<string> feedbackTopic = new List<string>();
            List<int> sumFeedbackTopic = new List<int>();
            foreach (var item in db.UserComments)
            {
                bool isAlive = false;
                foreach (var item1 in feedbackSta)
                {
                    if (item1.feedbackName == item.topic)
                    {
                        item1.sum++;
                        isAlive = true;
                    }
                }
                if (!isAlive)
                {
                    StatisticsClass.FeedbackStatistics temp = new StatisticsClass.FeedbackStatistics();
                    temp.feedbackName = item.topic;
                    temp.sum = 1;
                    feedbackSta.Add(temp);
                }
            }
            foreach(var item in feedbackSta)
            {
                feedbackTopic.Add(item.feedbackName);
                sumFeedbackTopic.Add(item.sum);
            }
            model._feedbackStatistics = feedbackSta;
            model.FeedbackName = feedbackTopic;
            model.sumFeedbackTopic = sumFeedbackTopic;
            return View(model);
        }

        public ActionResult userManagement()
        {
            var model = new AdminHomePageController();
            model.user = db.UserInfoes.ToList();
            return View(model);
        }

        public ActionResult userDetail(string username)
        {   
            if (username == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserInfo userComment = db.UserInfoes.Find(username);
            if (userComment == null)
            {
                return HttpNotFound();
            }
            return View(userComment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult userDetail([Bind(Include = "username,pass,fullname,dateOfBirth,sdt,addr,levelStudy")] UserInfo userUpdated)
        {
            if (ModelState.IsValid)
            {

                var item = db.UserInfoes.SingleOrDefault(x => (x.username == userUpdated.username));
                item.pass = userUpdated.pass;
                item.fullname = userUpdated.fullname;
                item.dateOfBirth = userUpdated.dateOfBirth;
                item.sdt = userUpdated.sdt;
                item.addr = userUpdated.addr;
                item.levelStudy = userUpdated.levelStudy;

                db.SaveChanges();
                return RedirectToAction("userManagement");
            }
            return View(userUpdated);
        }

        [HttpPost]
        public ActionResult DeleteUser(string username)
        {
            var itemToRemove = db.UserInfoes.SingleOrDefault(x => x.username == username); //returns a single item.

            if (itemToRemove != null)
            {
                db.UserInfoes.Remove(itemToRemove);
                db.SaveChanges();
            }
            return RedirectToAction("userManagement");
        }

        public ActionResult vocabularyManagement()
        {
            var model = new AdminHomePageController();
            model.vocab = db.Vocabularies.ToList();
            return View(model);
        }

        public ActionResult vocabAdd(Vocabulary model)
        {
            var item = db.Vocabularies.Where(x => (x.word == model.word));
            if (item.Count() > 0)
            {
                TempData["msg"] = "<script>alert('Đã tồn tại từ vựng.');</script>";
                return View();
            }
            try
            {
                Vocabulary newWord = new Vocabulary();
                newWord.word = model.word;
                newWord.mean = model.mean;
                newWord.typeWord = model.typeWord;
                newWord.levelStudy = model.levelStudy;
                newWord.soundURL = model.soundURL;
                newWord.imgURL = model.imgURL;
                db.Vocabularies.Add(newWord);
                db.SaveChanges();
                TempData["msg"] = "<script>alert('Đã thêm từ vựng thành công.');</script>";
            }
            catch
            {

            }
            return View();
        }

        public ActionResult vocabDetail(string word)
        {
            if (word == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vocabulary vocab = db.Vocabularies.Find(word);
            if (vocab == null)
            {
                return HttpNotFound();
            }
            return View(vocab);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult vocabDetail([Bind(Include = "word,mean,typeWord,levelStudy,imgURL,soundURL")] Vocabulary vocabUpdated)
        {
            if (ModelState.IsValid)
            {

                var item = db.Vocabularies.SingleOrDefault(x => (x.word == vocabUpdated.word));
                item.word = vocabUpdated.word;
                item.mean = vocabUpdated.mean;
                item.typeWord = vocabUpdated.typeWord;
                item.levelStudy = vocabUpdated.levelStudy;
                item.imgURL = vocabUpdated.imgURL;
                item.soundURL = vocabUpdated.soundURL;

                db.SaveChanges();
                return RedirectToAction("vocabularyManagement");
            }
            return View(vocabUpdated);
        }

        [HttpPost]
        public ActionResult DeleteVocab(string word)
        {
            var itemToRemove = db.Vocabularies.SingleOrDefault(x => x.word == word); //returns a single item.

            if (itemToRemove != null)
            {
                db.Vocabularies.Remove(itemToRemove);
                db.SaveChanges();
            }
            return RedirectToAction("vocabularyManagement");
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
            catch
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

