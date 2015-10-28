using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LastSurvey.Models;

namespace LastSurvey.Controllers
{
    public class QuestionsController : Controller
    {
        private ModelSurvey db = new ModelSurvey();

        // GET: Questions
        public ActionResult IndexAdmin(int? id)
        {
                 Survey survey = db.Surveys.Where(s=>s.SurveyID == id).FirstOrDefault();
                 if (survey != null)
                 {
                     var questions = survey.Questions.ToList();
                     if (questions != null)
                     {
                         @ViewBag.Surv = survey;
                         return View(questions);
                     }
                     else return RedirectToAction("IndexAdmin", "Surveys");
                 }
                 else return RedirectToAction("IndexAdmin", "Surveys");
        }
        // GET: Questions
        public ActionResult Index(int? id)
        {
            //.Include(q => q.QuestionType).Include(q => q.Survey)

            Survey survey = db.Surveys.Where(s => s.SurveyID == id).FirstOrDefault();
            if (survey != null)
            {
                var questions = survey.Questions.ToList();

                if (questions != null)
                {
                    ViewBag.Surv = survey;
                    return View(questions);
                }
                     else return RedirectToAction("Index", "Surveys");
                 }
                 else return RedirectToAction("Index", "Home");
        }

        //public ActionResult IndexAdmin()
        //{
        //    return View(db.Questions.ToList());
        //}

        [HttpPost]
        public ActionResult Submitted(int? id)
        {
            if (id != null)
            {
                Survey survey = db.Surveys.Where(s => s.SurveyID == id).FirstOrDefault();
                var questions = survey.Questions.ToList();
                String userI = Session["LoggedUserNumber"].ToString() ;
                User user = db.Users.Where(u => u.UserNumber == userI).FirstOrDefault();
                if (questions != null && user != null)
                {
                    foreach (Question q in questions)
                    {
                        //var value = Request.Form[q.QuestionID.ToString()].ToString();
                        var value = Request.Form[q.QuestionID.ToString()].ToString();
                        Answer answer = new Answer();
                        UserAnswer userAnswer = new UserAnswer();
                        answer.Answer1 = value;
                        answer.QuestionID = q.QuestionID;
                        userAnswer.AnswerID = answer.AnswerID;
                        userAnswer.UserID = user.UserID;
                        userAnswer.SubmitDate = DateTime.Now;
                        answer.UserAnswers.Add(userAnswer);
                        db.Answers.Add(answer);
                        db.SaveChanges();
                    }

                    return RedirectToAction("Store", "Answers", new {id = id});
                }
                else return RedirectToAction("Index", "Surveys");
            }
            else
            {
                return RedirectToAction("Index","Users");
            }
        }

        // GET: Questions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question question = db.Questions.Find(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            return View(question);
        }

        // GET: Questions/Create
        public ActionResult Create()
        {
            ViewBag.QuestionTypeID = new SelectList(db.QuestionTypes, "QuestionTypeID", "QuestionType1");
            ViewBag.SurveyID = new SelectList(db.Surveys, "SurveyID", "Title");
            return View();
        }

        // POST: Questions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "QuestionID,SurveyID,QuestionTypeID,Question1,Category,MaxValue,MinValue")] Question question)
        {
            var Id = question.SurveyID; 
            if (ModelState.IsValid)
            {
                db.Questions.Add(question);
                db.SaveChanges();
                return RedirectToAction("IndexAdmin", new {id = Id });
            }

            ViewBag.QuestionTypeID = new SelectList(db.QuestionTypes, "QuestionTypeID", "QuestionType1", question.QuestionTypeID);
            ViewBag.SurveyID = new SelectList(db.Surveys, "SurveyID", "Title", question.SurveyID);
            return View(question);
        }

        // GET: Questions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question question = db.Questions.Find(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            ViewBag.QuestionTypeID = new SelectList(db.QuestionTypes, "QuestionTypeID", "QuestionType1", question.QuestionTypeID);
            ViewBag.SurveyID = new SelectList(db.Surveys, "SurveyID", "Title", question.SurveyID);
            return View(question);
        }

        // POST: Questions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "QuestionID,SurveyID,QuestionTypeID,Question1,Category,MaxValue,MinValue")] Question question)
        {
            
            if (ModelState.IsValid)
            {
                db.Entry(question).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("IndexAdmin",new { id = question.SurveyID});
            }
            ViewBag.QuestionTypeID = new SelectList(db.QuestionTypes, "QuestionTypeID", "QuestionType1", question.QuestionTypeID);
            ViewBag.SurveyID = new SelectList(db.Surveys, "SurveyID", "Title", question.SurveyID);
            return View(question);
        }

        // GET: Questions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question question = db.Questions.Find(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            return View(question);
        }

        // POST: Questions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            List<Answer> answers = new List<Answer>();
            List<UserAnswer> uanswers = new List<UserAnswer>();
            answers = db.Answers.Where(a => a.QuestionID == id).ToList();
            foreach (Answer a in answers)
            {
                uanswers = db.UserAnswers.Where(ua => ua.AnswerID == a.AnswerID).ToList();
            }
            List<UserAnswer> uanswersDistinct = new List<UserAnswer>();
            uanswersDistinct = uanswers.Distinct().ToList();
            foreach (UserAnswer ua in uanswersDistinct)
            {
                if (ua != null)
                {
                    db.UserAnswers.Remove(ua);
                    db.SaveChanges();
                }
                
            }
            foreach (Answer a in answers)
            {
                if (a != null)
                {
                    db.Answers.Remove(a);
                    db.SaveChanges();
                }
            }
            Question question = db.Questions.Find(id);
            var idsurvey = question.SurveyID;
            if(question != null)
            {
                db.Questions.Remove(question);
                db.SaveChanges();
            }
            return RedirectToAction("IndexAdmin", new { id = idsurvey });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
