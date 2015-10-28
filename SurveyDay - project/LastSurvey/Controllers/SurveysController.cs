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
    public class SurveysController : Controller
    {
        private ModelSurvey db = new ModelSurvey();

        // GET: Surveys
        public ActionResult Index()
        {
            return View(db.Surveys.ToList());
        }

        public ActionResult IndexAdmin()
        {
            return View(db.Surveys.ToList());
        }

        // GET: Surveys/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Survey survey = db.Surveys.Find(id);
            if (survey == null)
            {
                return HttpNotFound();
            }
            return View(survey);
        }

        // GET: Surveys/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Surveys/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SurveyID,Title,StartDate,EndDate")] Survey survey)
        {
            if (ModelState.IsValid)
            {
                db.Surveys.Add(survey);
                db.SaveChanges();
                return RedirectToAction("IndexAdmin");
            }

            return View(survey);
        }

        // GET: Surveys/Edit/5
        public ActionResult Start(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Survey survey = db.Surveys.Find(id);
            if (survey == null)
            {
                return HttpNotFound();
            }
            return RedirectToAction("Index", "Questions", new { id = survey.SurveyID });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Start([Bind(Include = "SurveyID,Title,StartDate,EndDate")] Survey survey)
        {
            if (ModelState.IsValid)
            {
                db.Entry(survey).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(survey);
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Survey survey = db.Surveys.Find(id);
            if (survey == null)
            {
                return HttpNotFound();
            }
            return View(survey);
        }

        // POST: Surveys/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SurveyID,Title,StartDate,EndDate")] Survey survey)
        {
            if (ModelState.IsValid)
            {
                db.Entry(survey).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("IndexAdmin");
            }
            return View(survey);
        }
        // GET: Surveys/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Survey survey = db.Surveys.Find(id);
            if (survey == null)
            {
                return HttpNotFound();
            }
            return View(survey);
        }

        // POST: Surveys/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            List<Question> questions = new List<Question>();
            List<Answer> answers = new List<Answer>();
            List<UserAnswer> uanswers = new List<UserAnswer>();
            questions = db.Questions.Where(q => q.SurveyID == id).ToList();
            foreach (Question q in questions)
            {
                answers.AddRange(db.Answers.Where(a => a.QuestionID == q.QuestionID)); 
            }
            foreach (Answer a in answers)
            {
                uanswers.AddRange(db.UserAnswers.Where(ua => ua.AnswerID == a.AnswerID));
            }
            foreach (UserAnswer ua in uanswers.Distinct())
            {
                if (ua != null)
                {
                    db.UserAnswers.Remove(ua);
                    db.SaveChanges();
                } 
            }
            foreach (Answer a in answers.Distinct())
            {
                if(a != null)
                {
                    db.Answers.Remove(a);
                    db.SaveChanges();
                } 
            }
            foreach (Question q in questions) 
            {
                if( q != null)
                {
                    db.Questions.Remove(q);
                    db.SaveChanges();
                } 
            }

            Survey survey = db.Surveys.Find(id);
            if (survey != null)
            {
                db.Surveys.Remove(survey);
                db.SaveChanges();
            }
  
            return RedirectToAction("IndexAdmin");
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
