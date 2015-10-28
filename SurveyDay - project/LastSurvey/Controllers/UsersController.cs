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
    public class UsersController : Controller
    {
        private ModelSurvey db = new ModelSurvey();

        // GET: Users
        public ActionResult Index()
        {
            return View();
        }

        // GET: Amdin
        public ActionResult Admin()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult Login(User user)
        {
            if (ModelState.IsValid)
            {
                using (ModelSurvey db = new ModelSurvey())
                { 
                    var u = db.Users.Where(e => e.UserNumber.Equals(user.UserNumber) && e.Password.Equals(user.Password)).FirstOrDefault();
                    if (u != null)
                    {
                        Session["LoggedUser"] = user.UserID.ToString();
                        Session["LoggedUserNumber"] = user.UserNumber.ToString();
                        if (u.IsAdmin)
                        {
                            return RedirectToAction("Admin", "Users");
                        }
                        else
                        {
                            return RedirectToAction("Index", "Users");
                        }
                        
                    }
                    else
                    {
                        Session["LoggedUser"] = null;
                        Session["LoggedUserNumber"] = null;
                        ModelState.AddModelError("", "Login Incorrect");
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            return RedirectToAction("Index", "Home");
        }
        

        public ActionResult Logout()
        {
            Session["LoggedUser"] = null;
            Session["LoggedUserNumber"] = null;
            return RedirectToAction("Index","Home");
        }

        public ActionResult IndexAdmin() 
        {
            return View(db.Users.ToList());
        }

        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserID,UserNumber,Password,Email,IsAdmin,PasswordSalt")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("IndexAdmin");
            }

            return View(user);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
           
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserID,UserNumber,Password,Email,IsAdmin,PasswordSalt")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("IndexAdmin");
            }
            return View(user);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            List<UserAnswer> uanswers = new List<UserAnswer>();
            List<Answer> answers = new List<Answer>();
            uanswers = db.UserAnswers.Where(ua => ua.UserID == id).ToList();
            List<UserAnswer> uanswersDistinct = new List<UserAnswer>();
            uanswersDistinct = uanswers.Distinct().ToList();
            foreach (UserAnswer ua in uanswersDistinct)
            {
                answers.Add(db.Answers.Find(ua.AnswerID));
            }
            List<Answer> answersDistict = new List<Answer>();
            answersDistict = answers.Distinct().ToList();
            foreach (UserAnswer ua in uanswersDistinct)
            {
                if(ua != null)
                {
                    db.UserAnswers.Remove(ua);
                    db.SaveChanges();
                }
                
            }
            foreach (Answer a in answersDistict)
            {
                if (a != null)
                {
                    db.Answers.Remove(a);
                    db.SaveChanges();
                }
                
            }
            User user = db.Users.Find(id);
            if(user.IsAdmin || user == null)
            {
                return RedirectToAction("IndexAdmin");
            }
            else
            {
                db.Users.Remove(user);
                db.SaveChanges();
                return RedirectToAction("IndexAdmin");
            }
            
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
